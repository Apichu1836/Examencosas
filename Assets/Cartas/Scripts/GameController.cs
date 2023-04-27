using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : SingletonCartas<GameController>
{
    private int _score = 0;
    [SerializeField] private TextMesh scoreText;
    [SerializeField] private TextMesh mainLabel;
    public float levelCompleteDuration = 3.0f;

    private void Start()
    {
        UpdateMainLabel("");
        UpdateScoreLabel();
    }
    protected void UpdateScoreLabel()
    {
        scoreText.text = "Score: " + _score;
    }

    protected void UpdateMainLabel(string text)
    {
        mainLabel.text = text;
    }

    public void AddScore(int score)
    {
        _score += score;
        UpdateScoreLabel();
    }

    public void NextScene()
    {
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            GameCompleted();
        }
        else
        {
            StartCoroutine(showMessageAndGotoNextScreen("Nivel completado", levelCompleteDuration));
        }
    }

    private IEnumerator showMessageAndGotoNextScreen(string msg, float waitSeconds)
    {
        UpdateMainLabel(msg);
        yield return new WaitForSeconds(waitSeconds);
        UpdateMainLabel("");
        SceneManager.LoadScene("Level2");
        if(SceneManager.GetActiveScene().name == "Level1"){
            SceneManager.LoadScene("Level2");
        }else if(SceneManager.GetActiveScene().name == "Level2"){
            SceneManager.LoadScene("Level3");
        }
    }

    protected void GameCompleted()
    {
        mainLabel.text = "�Felicidades!";
    }

    public void ResetGame()
    {
        _score = 0;
        UpdateMainLabel("");
        UpdateScoreLabel();
        SceneManager.LoadScene(0);
    }
}
