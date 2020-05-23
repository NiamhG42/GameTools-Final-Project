using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void PlayScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
