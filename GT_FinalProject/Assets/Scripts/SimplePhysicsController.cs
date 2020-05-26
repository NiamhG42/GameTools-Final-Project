using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePhysicsController : MonoBehaviour
{
    #region Fields
    private float vInput;
    private float hInput;
    private float eInput;

    private Rigidbody rb;
    private GameObject raceManager;

    public float speed = 5.0f;
    public float rotSpeed = 180.0f;
    public float rollResetSpeed = 400f;

    public Vector3 verticalForceDirection, elevationForceDirection;

    public AudioSource normalSpeedAudioSource, fastAudioSource;
    private bool soundPlaying, soundStarted;
    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        raceManager = GameObject.Find("RaceManager");
    }


    // Update is called once per frame
    void Update()
    {
        vInput = Input.GetAxis("Vertical") * speed;
        hInput = Input.GetAxis("Horizontal") * rotSpeed;
        eInput = -Input.GetAxis("Elevate") * speed;
    }

    private void FixedUpdate()
    {
        if (rb!=null && raceManager.GetComponent<RaceManagerScript>().isRacing)
        {

            //forward and backward movement
            verticalForceDirection = transform.forward * vInput;
            rb.AddForce(verticalForceDirection);

           //yaw (left/right heading control)
            Quaternion yawRot = Quaternion.AngleAxis(hInput * Time.fixedDeltaTime,Vector3.up);
            rb.MoveRotation(rb.rotation*yawRot);

            //elevation (move up/down control)
            elevationForceDirection = transform.up * eInput;
           rb.AddForce(elevationForceDirection);
            
            //Go faster if space is held down
            if (Input.GetKey("space")){    
                speed = 10.0f;
            }
            else {
                speed = 5.0f;                
            }

            PlaySounds();

            //the code below stabilises the vehicle roll after a collision
            // if ((vInput==0f)&&(hInput == 0f)&&(eInput == 0f))
            // {
            //first we look at the local "right" vector of the vehicle
            //if that vector is in the horizontal plane (parallel to the ground) then we don't need to do anything
            Vector3 currentRight=transform.right;
                //we only test the y component of the "right" vector:
                float y = currentRight.y;
                //Uncomment the line and watch the console to convince yourself that when y is 0 then we don't need to correct rotation
                //Debug.Log(y.ToString());

                //we create a Quaternion corresponding to a small rotation along the vehicle's "forward" axis:
                Quaternion rollRot = Quaternion.AngleAxis(- y * rollResetSpeed * Time.fixedDeltaTime, Vector3.forward);
                //we provide target rotation by multiplying the existing rigidbody rotation by the Quaternion:
                rb.MoveRotation(rb.rotation * rollRot);
           // }
           

        }

        if (!raceManager.GetComponent<RaceManagerScript>().isRacing)
        {
            fastAudioSource.Stop();
            normalSpeedAudioSource.Stop();
        }
    }

    void PlaySounds()
    {
        //If any of the keys are down, should be playing sounds
        if ((Input.GetKeyDown("w") || Input.GetKeyDown("s")
            || Input.GetKeyDown("a") || Input.GetKeyDown("d")
            || Input.GetKeyDown("r") || Input.GetKeyDown("f")
            ) && !soundPlaying)
        {     
            soundPlaying = true;
        }

        //If no move keys are down, should NOT be playing sounds
        if (!Input.GetKey("w") && !Input.GetKey("s")
            && !Input.GetKey("a") && !Input.GetKey("d")
            && !Input.GetKey("r") && !Input.GetKey("f")
            && soundPlaying)
        {
            soundPlaying = false;
        }

        //Check what sound should be playing and start playing it
        if (soundPlaying && !soundStarted)
        {
            if (speed == 10f)
            {
                normalSpeedAudioSource.Stop();
                fastAudioSource.Play();
            }
            else
            {
                fastAudioSource.Stop();
                normalSpeedAudioSource.Play();
            }
            soundStarted = true;
        }

        //Stop playing sounds
        if (!soundPlaying)
        {
            fastAudioSource.Stop();
            normalSpeedAudioSource.Stop();
            soundStarted = false;
        }

        //If starting or stopping going fast, recheck what sounds to play
        if (Input.GetKeyDown("space"))
        {
            soundStarted = false;
        }
        if (Input.GetKeyUp("space"))
        {
            soundStarted = false;
        }

    }
    #endregion
}