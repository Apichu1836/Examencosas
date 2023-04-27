using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;

    private int _id;
    public int id
    {
        get { return _id; }
    }


    private SceneController controller;

    private void Start()
    {
        controller = GameObject.Find("SceneController").GetComponent<SceneController>();
    }


    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    private void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.canReveal)
        {
            cardBack.SetActive(false);
            controller.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        cardBack.SetActive(true);
    }
}
