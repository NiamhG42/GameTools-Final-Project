using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    #region Fields
    public GameObject raceManager, teleportTarget1, teleportTarget2;
    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        raceManager = GameObject.Find("RaceManager");
    }
    #endregion
    #region Methods
    private void OnTriggerEnter(Collider other)
    {
        //If you run into a PointsBooster, increase points
        if (other.tag == "PointsBooster")
        {
            raceManager.GetComponent<RaceManagerScript>().currentPoints++;            
            other.gameObject.SetActive(false);
        }

        //If you run into a LapCounter, increase Lap Count
        if (other.tag == "LapCounter")
        {
            raceManager.GetComponent<RaceManagerScript>().UpdateLapCount();
            raceManager.GetComponent<RaceManagerScript>().NewLap();
        }

        if (other.tag == "Teleporter1")
        {
            transform.position = teleportTarget1.transform.position;
            transform.rotation = teleportTarget1.transform.rotation;
        }
        if (other.tag == "Teleporter2")
        {
            transform.position = teleportTarget2.transform.position;
            transform.rotation = teleportTarget2.transform.rotation;
        }
    }
    #endregion
}
