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

    #endregion

    #region Methods
    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        countdownTime = 3.4f;
        currentPoints = 0;
        currentLap = 1;
    }

    // Update is called once per frame
    void Update()
    {  
        if (isRacing)
        {
            countdownTime = 3.4f;
            countdownText.gameObject.SetActive(false);

            RunTimer();
            DisplayPointsAndLaps();
        }
        else if(raceFinished)
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
            //Reset current time
            currentTime = 0;

            //Record final points
            finalPoints = currentPoints;
            //Reset points
            currentPoints = 0;
        }
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
