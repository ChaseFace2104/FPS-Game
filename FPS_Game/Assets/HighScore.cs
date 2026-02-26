using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public int[] scores = new int[10];

    string dir;

    public string scoreFile = "highscore.txt";

    // Start is called before the first frame update
    void Start()
    {
        dir = Application.dataPath;
        Debug.Log("current directory is " + dir);

        LoadScoresFromFile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScoresFromFile()
    {
        bool isFile = File.Exists(dir + "\\" + scoreFile);
        if (isFile)
        {
            Debug.Log("Found file, " + scoreFile);
        } else
        {
            Debug.Log("File " + scoreFile + " doesnt exist", this);
            return;
        }

        scores = new int[scores.Length];

        StreamReader fileReader = new StreamReader(dir + "\\" + scoreFile);

        int scoreCount = 0;

        while(fileReader.Peek() != 0 && scoreCount < scores.Length)
        {
            string fileLine = fileReader.ReadLine();
            int readScore = -1;
            bool didParse = int.TryParse(fileLine, out readScore);
            if (didParse)
            {
                scores[scoreCount] = readScore;
            } else
            {
                Debug.Log("invalid line at " + scoreCount, this);
                scores[scoreCount] = 0;
            }
            scoreCount++;
        }

        fileReader.Close();
        Debug.Log("high scores read from " + scoreFile);
    }

    public void SaveScoresToFile()
    {
        StreamWriter fileWriter = new StreamWriter(dir + "\\" + scoreFile);

        for (int i = 0; i < scores.Length; i++)
        {
            fileWriter.WriteLine(scores[i]);
        }

        fileWriter.Close();

        Debug.Log("scores written to " + scoreFile);
    }

    public void AddScore(int newScore)
    {
        int desiredIndex = -1;
        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] < newScore || scores[i] == 0)
            {
                desiredIndex = i;
                break;
            }
        }

        if (desiredIndex < 0)
        {
            Debug.Log("score of " + newScore + " not high enough for score list", this);
            return;
        }

        for (int i = scores.Length - 1; i > desiredIndex; i--)
        {
            scores[i] = scores[i - 1];
        }

        scores[desiredIndex] = newScore;
        Debug.Log("score of " + newScore + " put into high scores at " + desiredIndex, this);
    }
}
