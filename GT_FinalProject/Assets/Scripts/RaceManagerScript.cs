using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManagerScript : MonoBehaviour
{
    #region Fields

    public bool isRacing;
    public Text timerText;
    private float currentTime, finalTime;
    #endregion

    #region Methods
    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {  
        if (isRacing)
        {
            RunTimer();
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
        timerText.text = "Time: " + minutes + ":" + seconds;
    }

    void FinishRace()
    {
        if (currentTime != 0)
        {
            isRacing = false;
            //Record final time
            finalTime = currentTime;
            //Reset current time
            currentTime = 0;
        }
    }
    #endregion
}
