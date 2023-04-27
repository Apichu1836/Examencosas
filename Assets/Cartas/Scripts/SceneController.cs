using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneController : MonoBehaviour
{
    [SerializeField] private MemoryCard templateCard;
    [SerializeField] private Sprite[] images;

    public int gridRows = 2;
    public int gridCols = 4;
    public float header = 2f;
    public float margin = 1f;

    private MemoryCard _firstRevealed, _secondRevealed;

    private int cardsFound = 0;
    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }



    public void CardRevealed(MemoryCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        } else
        {
            _secondRevealed = card;
            //Debug.Log(_firstRevealed.id == _secondRevealed.id ? "PAREJA" : "INTENTALO OTRA VEZ");
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.id == _secondRevealed.id)
        {
            GameController.Instance.AddScore(1);
            cardsFound += 2;
            if (cardsFound == gridCols * gridRows)
            {
                GameController.Instance.NextScene();
            }
        } else
        {
            yield return new WaitForSeconds(0.5f);
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }
        _firstRevealed = _secondRevealed = null;
    }

    void Start()
    {
        //int id = Random.Range(0, images.Length);
        //var card = Instantiate<MemoryCard>(templateCard);
        //card.SetCard(id, images[id]);
        float totalheight = Camera.main.orthographicSize * 2;
        float totalwidht = totalheight * Camera.main.aspect;
        Deal(templateCard, totalwidht, totalheight, header, margin, gridRows, gridCols);

    }

    private void Deal(MemoryCard prefab, float totalwidth, float totalheight,
    float header, float margin, int gridRows, int gridCols)
    {
        float spacewidth = (totalwidth - 2 * margin) / gridCols;
        float spaceheight = (totalheight - header - margin) / gridRows;

        int[] ids = ShufflePairs(gridCols * gridRows);
        int index = 0;
        for (int j = 0; j < gridRows; j++)
        {
            for (int i = 0; i < gridCols; i++)
            {
                var card = Instantiate<MemoryCard>(prefab);
                //int id = Random.Range(0, images.Length);
                int id = ids[index++];
                card.SetCard(id, images[id]);
                float posX = (i + 0.5f) * spacewidth - totalwidth / 2 + margin;
                float posY = (j + 0.5f) * spaceheight - totalheight / 2 + margin;
                card.transform.position = new Vector3(posX, posY, prefab.transform.position.z);
            }
        }
    }

    private int[] ShufflePairs(int numCards)
    {
        int[] ids = new int[numCards];
        for (int i = 0; i < numCards; i++)
        {
            ids[i] = i / 2;
        }
        for (int i = 0; i < numCards - 1; i++)
        {
            int r = Random.Range(i, numCards);
            int tmp = ids[i];
            ids[i] = ids[r];
            ids[r] = tmp;
        }
        return ids;
    }

    public void Restart()
    {
        GameController.Instance.ResetGame();
    }

}
