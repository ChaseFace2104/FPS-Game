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
    public Camera worldCam;
    public Transform spawn;

    public float startTimerAmount;
    private float startTimer;

    public float targetActivateTimerAmount;
    private float targetActivateTimer;

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
        startTimer -= Time.deltaTime;

        messageText.text = "Get Ready " + (int)(startTimer + 1);

        if (startTimer < 0) {
            messageText.text = "";
            gameState = GameState.Playing;
            gameTimer = gameTimerAmount;
            startTimer = startTimerAmount;
            score = 0;

            player.SetActive(true);
            worldCam.gameObject.SetActive(false);
        }
    }

    void GameStatePlaying()
    {
        gameTimer -= Time.deltaTime;
        int sec = Mathf.RoundToInt(gameTimer);
        timerText.text = string.Format("Time: {0:D2}:{1:D2}", (sec / 60), (sec % 60));

        if (gameTimer <= 0) {
            Debug.Log("GAME OVER, SCORE: " + score);
            gameState = GameState.GameOver;
            player.SetActive(false);
            worldCam.gameObject.SetActive(true);
            for (int i = 0; i < targets.Length; i++) {
                targets[i].gameObject.SetActive(false);
            }

            highScores.AddScore(score);
            highScores.SaveScoresToFile();
        }

        targetActivateTimer -= Time.deltaTime;
        if (targetActivateTimer <= 0) {
            ActivateRandomTarget();
            targetActivateTimer = targetActivateTimerAmount;
        }
    }

    void GameStateOver()
    {
        player.transform.position = spawn.position;
        messageText.text = "press enter to start";
        if (Input.GetKeyUp(KeyCode.Return)) {
            gameState = GameState.Start;
            timerText.text = "";
            scoreText.text = "";
        }
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    private void Awake()
    {
        gameState = GameState.GameOver;
    }

    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(false);
        worldCam.gameObject.SetActive(true);
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].GameManager = this;
            targets[i].gameObject.SetActive(false);
        }
        startTimer = startTimerAmount;
        messageText.text = "press enter to start";
        timerText.text = "";
        scoreText.text = "";
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

    public void ActivateRandomTarget() {
        int randIndex = Random.Range(0, targets.Length);
        targets[randIndex].gameObject.SetActive(true);
    }
}
