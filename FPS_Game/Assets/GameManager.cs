using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HighScore highScores;

    public TextMeshProUGUI messageText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    public target[] targets;
    public GameObject player;

    public float startTimerAmount;
    private float startTimer;

    public float targetActivateTimerAmount;
    public float targetActivateTimer;

    public float gameTimerAmount = 60;
    private float gameTimer;

    private int score = 0;

    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };

    private GameState gameState;

    public GameState State { get { return gameState;  } }

    void GameStateStart()
    {

    }

    void GameStatePlaying()
    {
        
    }

    void GameStateOver()
    {

    }

    public void AddScore(int points)
    {
        score += points;
    }

    private void Awake()
    {
        gameState = GameState.GameOver;
    }

    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(false);
        Camera.main.gameObject.SetActive(true);
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].GameManager = this;
            targets[i].gameObject.SetActive(false);
        }
        startTimer = startTimerAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

        switch (gameState)
        {
            case GameState.Start:
                GameStateStart();
                break;
            case GameState.Playing:
                GameStatePlaying();
                break;
            case GameState.GameOver:
                GameStateOver();
                break;
        }
    }
}
