using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    #region Fields
    public GameObject raceManager, teleportTarget1, teleportTarget2, caveGroup;
    private Rigidbody rb;
    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        raceManager = GameObject.Find("RaceManager");
        caveGroup.gameObject.SetActive(false);
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
            caveGroup.gameObject.SetActive(true);
            transform.position = teleportTarget1.transform.position;
            transform.rotation = teleportTarget1.transform.rotation;
        }
        if (other.tag == "Teleporter2")
        {
            caveGroup.gameObject.SetActive(false);
            transform.position = teleportTarget2.transform.position;
            transform.rotation = teleportTarget2.transform.rotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If run into something bouncy, bounce back in direction you drove into it from
        if(collision.gameObject.tag == "Bouncy")
        {
            Vector3 verticalForceDirection = gameObject.GetComponent<SimplePhysicsController>().verticalForceDirection;
            Vector3 elevationForceDirection = gameObject.GetComponent<SimplePhysicsController>().elevationForceDirection;
         
            rb.AddForce(-verticalForceDirection*0.7f, ForceMode.Impulse);
            rb.AddForce(-elevationForceDirection * 0.7f, ForceMode.Impulse);

        }
    }
    #endregion
}
