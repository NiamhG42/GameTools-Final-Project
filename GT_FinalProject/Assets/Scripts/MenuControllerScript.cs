using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerScript : MonoBehaviour
{
    #region Fields
    public Text firstPlaceText, secondPlaceText, thirdPlaceText;
    private float firstPlaceScore, secondPlaceScore, thirdPlaceScore;
    #endregion

    #region UnityMethods
  
    // Start is called before the first frame update
    void Start()
    {

        firstPlaceScore = PlayerPrefs.GetFloat("FirstPlaceScore", 66.129f);
        secondPlaceScore = PlayerPrefs.GetFloat("SecondPlaceScore", 100.32f);
        thirdPlaceScore = PlayerPrefs.GetFloat("ThirdPlaceScore", 110.34f);

        string minutes = ((int)firstPlaceScore / 60).ToString();
        string seconds = (firstPlaceScore % 60).ToString("f2");
        firstPlaceText.text = "1. " + minutes + "." + seconds;

        minutes = ((int)secondPlaceScore / 60).ToString();
        seconds = (secondPlaceScore % 60).ToString("f2");
        secondPlaceText.text = "2. " + minutes + "." + seconds;

        minutes = ((int)thirdPlaceScore / 60).ToString();
        seconds = (thirdPlaceScore % 60).ToString("f2");
        thirdPlaceText.text = "3. " + minutes + "." + seconds;
    }

    #endregion

    #region Methods

    public void PlayScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    #endregion
}
