using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManagerScript : MonoBehaviour
{
    #region Fields

    public bool isRacing, raceFinished;
    public Text timerText, pointsText, lapText, countdownText;
    private float currentTime, finalTime, countdownTime;
    public int currentPoints;
    private int finalPoints, currentLap;

    public GameObject[] pointsBooster;

    private float currentScore;
    public float[] highScores = new float[3];

    #endregion

    #region Methods
    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        highScores[0] = PlayerPrefs.GetFloat("FirstPlaceScore", 0);
        highScores[1] = PlayerPrefs.GetFloat("SecondPlaceScore", 0);
        highScores[2] =PlayerPrefs.GetFloat("ThirdPlaceScore", 0);

        currentTime = 0;
        countdownTime = 3.5f;
        currentPoints = 0;
        currentLap = 1;
    }

    // Update is called once per frame
    void Update()
    {  
        if (isRacing)
        {
            countdownTime = 3.5f;
            countdownText.gameObject.SetActive(false);

            RunTimer();
            DisplayPointsAndLaps();
        }
        else if(!raceFinished)
        {
            StartCountdown();
        }
    }
    #endregion

    void RunTimer()
    {
        //Run the timer up
        currentTime += Time.deltaTime;

        //Turn time into strings
        string minutes = ((int) currentTime/60).ToString();
        string seconds = (currentTime % 60).ToString("f2");

        //Display the time
        timerText.text = "Time: " + minutes + "." + seconds;
    }

    void DisplayPointsAndLaps()
    {
        pointsText.text = "Points: " + currentPoints;
        lapText.text = "Lap: " +  currentLap+ "/3";
    }

    void StartCountdown()
    {
        countdownText.gameObject.SetActive(true);

        if (countdownTime >0)
        {
            countdownTime -= Time.deltaTime;

            string countdownSeconds = (countdownTime % 60).ToString("f0");

            countdownText.text = countdownSeconds;
        }

        if (countdownTime < 0)
        {
            StartRace();
        }
    }

    void StartRace()
    {
        if (!isRacing)
        {
            currentLap = 1;
            currentTime = 0;
            currentPoints = 0;
            isRacing = true;
        }
     }

    public void UpdateLapCount()
    {
        currentLap++;
        if (currentLap > 3)
        {
            FinishRace();
        }
    } 

    void FinishRace()
    {
        if (currentTime != 0)
        {
            isRacing = false;
            raceFinished = true;

            //Record final time
            finalTime = currentTime;
            //Record final points
            finalPoints = currentPoints;

            UpdateHighScores();

            //Reset current time
            currentTime = 0;
            //Reset points
            currentPoints = 0;
        }
    }

    void UpdateHighScores()
    {
        currentScore = currentTime - currentPoints;

        //If current score is better than lowest previous highscore
        //Add it to the scoreboard
        if (currentScore < highScores[highScores.Length-1])
        {
            highScores[highScores.Length-1] = currentScore;
        }

        
        //Sort highscore values in ascending order
        for (int i=0; i<highScores.Length; i++)
        {
            for (int j = i+1; j < highScores.Length; j++)
            {
                if (highScores[j] < highScores[i])
                {
                    //Swap
                    float tempHighScore = highScores[i];
                    highScores[i] = highScores[j];
                    highScores[j] = tempHighScore;
                }
            }
        }

        //Save high scores
        PlayerPrefs.SetFloat("FirstPlaceScore", highScores[0]);
        PlayerPrefs.SetFloat("SecondPlaceScore", highScores[1]);
        PlayerPrefs.SetFloat("ThirdPlaceScore", highScores[2]);
        PlayerPrefs.Save();

    }

    public void NewLap()
    {
        for (int i = 0; i < pointsBooster.Length; i++)
        {
            pointsBooster[i].gameObject.SetActive(true);
        }
    }
    #endregion
}
