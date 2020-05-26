using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    #region Fields
    public GameObject raceManager, teleportTarget1, teleportTarget2, caveGroup;
    private Rigidbody rb;
    public AudioSource myAudioSource;
    public AudioClip [] audioClip;
    private int pointBoosterSoundControl;
    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        pointBoosterSoundControl = 0;
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
            //Play Audio
            if (pointBoosterSoundControl == 0)
            {
                myAudioSource.clip = audioClip[4];
                pointBoosterSoundControl = 1;
            }
            else
            {
                myAudioSource.clip = audioClip[5];
                pointBoosterSoundControl = 0;
            }

            myAudioSource.Play();


            raceManager.GetComponent<RaceManagerScript>().currentPoints++;            
            other.gameObject.SetActive(false);
        }

        //If you run into a LapCounter, increase Lap Count
        if (other.tag == "LapCounter")
        {
            raceManager.GetComponent<RaceManagerScript>().UpdateLapCount();
            raceManager.GetComponent<RaceManagerScript>().NewLap();

            //Play Audio
            if (raceManager.GetComponent<RaceManagerScript>().currentLap > 3)
            {
                myAudioSource.clip = audioClip[7];
                myAudioSource.Play();
            }
            else
            {
                myAudioSource.clip = audioClip[8];
                myAudioSource.Play();
            }
        }

        if (other.tag == "Teleporter1")
        {
            //Play Audio
            myAudioSource.clip = audioClip[3];
            myAudioSource.Play();

            caveGroup.gameObject.SetActive(true);
            transform.position = teleportTarget1.transform.position;
            transform.rotation = teleportTarget1.transform.rotation;    
            
        }
        if (other.tag == "Teleporter2")
        {
            //Play Audio
            myAudioSource.clip = audioClip[3];
            myAudioSource.Play();

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
            //Play Audio
            myAudioSource.clip = audioClip[6];
            myAudioSource.Play();

            Vector3 verticalForceDirection = gameObject.GetComponent<SimplePhysicsController>().verticalForceDirection;
            Vector3 elevationForceDirection = gameObject.GetComponent<SimplePhysicsController>().elevationForceDirection;
         
            rb.AddForce(-verticalForceDirection*0.7f, ForceMode.Impulse);
            rb.AddForce(-elevationForceDirection * 0.7f, ForceMode.Impulse);

        }

        else if (collision.gameObject.name == "Dome")
        {
            //Play Audio
            myAudioSource.clip = audioClip[1];
            myAudioSource.Play();
        }

        else if (collision.gameObject.tag == "Crystal")
        {
            //Play Audio
            myAudioSource.clip = audioClip[2];
            myAudioSource.Play();
        }

        else
        {
            //Play Audio
            myAudioSource.clip = audioClip[0];
            myAudioSource.Play();
        }
    }
    #endregion
}
