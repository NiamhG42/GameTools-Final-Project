using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceManagerScript : MonoBehaviour
{
    #region Fields

    public bool isRacing, raceFinished;
    public Text timerText, pointsText, lapText, countdownText, lapAnnouncerText;
    private float currentTime, finalTime, countdownTime, lapAnnounceTime;
    public int currentPoints;
    private int finalPoints;
        public int currentLap;

    public List<GameObject> pointsBoosterList = new List<GameObject>();

    private float currentScore;
    public float[] highScores = new float[3];

    //End of Game 
    public Text finalTimeText, finalPointsText, finalScoreText;
    public Text[] highScoresText = new Text[3];
   // public Text firstPlaceText, secondPlaceText, thirdPlaceText;
    public GameObject EndOfGameUI, DuringGameUI;

    private AudioSource myAudioSource;
    public AudioClip[] audioClip;
    private bool countdownSound1Played, countdownSound2Played, countdownSound3Played;

    #endregion

    #region Methods
    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();

        highScores[0] = PlayerPrefs.GetFloat("FirstPlaceScore", 0);
        highScores[1] = PlayerPrefs.GetFloat("SecondPlaceScore", 0);
        highScores[2] =PlayerPrefs.GetFloat("ThirdPlaceScore", 0);

        currentTime = 0;
        countdownTime = 3.5f;
        currentPoints = 0;
        currentLap = 1;
        lapAnnounceTime = 1.5f;
        DuringGameUI.gameObject.SetActive(true);
        EndOfGameUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DisplayPointsAndLaps();
        LapAnnouncerUpdate();

        if (isRacing)
        {
            countdownTime = 3.5f;
            countdownText.gameObject.SetActive(false);

            RunTimer();
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
        pointsText.text = "Points: " + currentPoints + "/" + (pointsBoosterList.Count*3);
        lapText.text = "Lap: " +  currentLap+ "/3";
    }

    void StartCountdown()
    {
        countdownText.gameObject.SetActive(true);

        if (countdownTime >0)
        {
            //Run Countdown
            countdownTime -= Time.deltaTime;

            //Display Countdown Text
            string countdownSeconds = (countdownTime % 60).ToString("f0");
            countdownText.text = countdownSeconds;

            //Play Countdown Sounds
            if (countdownTime <3 &&  countdownTime >2 && !countdownSound1Played)
            {
                myAudioSource.clip = audioClip[0];
                myAudioSource.Play();
                countdownSound1Played = true;
            }
            if (countdownTime < 2 && countdownTime > 1 && !countdownSound2Played)
            {
                myAudioSource.clip = audioClip[0];
                myAudioSource.Play();
                countdownSound2Played = true;
            }
            if (countdownTime < 1 && countdownTime > 0 && !countdownSound3Played)
            {      
                myAudioSource.clip = audioClip[0];
                myAudioSource.Play();
                countdownSound3Played = true;
            }
        }

        if (countdownTime < 0)
        {
            myAudioSource.clip = audioClip[2];
            myAudioSource.Play();
            StartRace();
        }
    }

    void StartRace()
    {
        if (!isRacing)
        {
            currentLap = 1;
            currentTime = 0;
            lapAnnouncerText.gameObject.SetActive(true);
            lapAnnounceTime = 1.5f;
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

            DuringGameUI.gameObject.SetActive(false);
            EndOfGameUI.gameObject.SetActive(true);

            //Record final time
            finalTime = currentTime;
            //Record final points
            finalPoints = currentPoints;

            UpdateHighScores();

            //Reset current time
            currentTime = 0;
            //Reset points
            currentPoints = 0;

            //Display Correct UI End Text
            //TIME
            string minutes = ((int)finalTime / 60).ToString();
            string seconds = (finalTime % 60).ToString("f2");
            finalTimeText.text = "  Final Time: " + minutes + "." + seconds;

            //POINTS
            finalPointsText.text = "- Final Points: " + finalPoints;

            //FINAL SCORE FOR THIS RACE
            minutes = ((int)currentScore / 60).ToString();
            seconds = (currentScore % 60).ToString("f2");
            finalScoreText.text = "  Final Score: " + minutes+ "." + seconds;

            //HIGH SCORES
            //FIRST PLACE TEXT
            minutes = ((int)highScores[0] / 60).ToString();
            seconds = (highScores[0] % 60).ToString("f2");
            highScoresText[0].text = "1. " + minutes + "." + seconds;

            //SECOND PLACE TEXT
            minutes = ((int)highScores[1] / 60).ToString();
            seconds = (highScores[1] % 60).ToString("f2");
            highScoresText[1].text = "2. " + minutes + "." + seconds;

            //THIRD PLACE TEXT
            minutes = ((int)highScores[2] / 60).ToString();
            seconds = (highScores[2] % 60).ToString("f2");
            highScoresText[2].text = "3. " + minutes + "." + seconds;

            //If your score is the same as a high score, highlight the high score in red
            for (int i=0; i<highScoresText.Length; i++)
            {
                if (currentScore == highScores[i])
                {
                    highScoresText[i].color = Color.red;
                    finalScoreText.color = Color.red;
                }

                else
                {
                    highScoresText[i].color = Color.white;
                    finalScoreText.color = Color.white;
                }
            }
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

    void LapAnnouncerUpdate()
    {
        if (lapAnnouncerText.gameObject.activeSelf && isRacing)
        {
            lapAnnounceTime -= Time.deltaTime;

            if (lapAnnounceTime < 0)
            {
                lapAnnouncerText.gameObject.SetActive(false);
            }
        }

        if (currentLap == 1 && isRacing)
        {
            lapAnnouncerText.text = "Start!";
        }
        else if (currentLap == 2)
        {
            lapAnnouncerText.text = "Lap 2";
        }
        if (currentLap == 3)
        {
            lapAnnouncerText.text = "Final Lap!";
        }
        if (currentLap > 3)
        {
            Debug.Log("Finish Text Working");
            lapAnnouncerText.text = "Finish!";
        }
    }

    public void NewLap()
    {
        lapAnnounceTime = 1.5f;
        lapAnnouncerText.gameObject.SetActive(true);

        for (int i = 0; i < pointsBoosterList.Count; i++)
        {        
            pointsBoosterList[i].gameObject.SetActive(true);
        }
    }

   public void RestartScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void playMenuClickSound()
    {
        myAudioSource.clip = audioClip[1];
        myAudioSource.Play();
    }
    #endregion
}
