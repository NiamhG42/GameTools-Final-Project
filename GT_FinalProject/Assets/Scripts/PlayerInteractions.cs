using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    #region Fields
    public GameObject raceManager;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        raceManager = GameObject.Find("RaceManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        //If you run into a PointsBooster, increase points
        if (other.tag == "PointsBooster")
        {
            raceManager.GetComponent<RaceManagerScript>().currentPoints++;            
            other.gameObject.SetActive(false);
        }

        //If you run into a PointsBooster, increase points
        if (other.tag == "LapCounter")
        {
            raceManager.GetComponent<RaceManagerScript>().UpdateLapCount();
            raceManager.GetComponent<RaceManagerScript>().NewLap();
        }
    }
    #endregion
}
