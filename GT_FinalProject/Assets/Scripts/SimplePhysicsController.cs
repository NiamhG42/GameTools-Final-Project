﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePhysicsController : MonoBehaviour
{

    private float vInput;
    private float hInput;
    private float eInput;

    private Rigidbody rb;
    private GameObject raceManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        raceManager = GameObject.Find("RaceManager");
    }

    public float speed = 5.0f;
    public float rotSpeed = 180.0f;
    public float rollResetSpeed = 400f;

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
            rb.AddForce(transform.forward * vInput);

           //yaw (left/right heading control)
            Quaternion yawRot = Quaternion.AngleAxis(hInput * Time.fixedDeltaTime,Vector3.up);
            rb.MoveRotation(rb.rotation*yawRot);

            //elevation (move up/down control)
           rb.AddForce(transform.up * eInput);
            
            //Go faster if space is held down
            if (Input.GetKey("space")){    
                speed = 8.0f;
            }
            else {speed = 5.0f; }



            //the code below stabilises the vehicle roll after a collision or a side turn when pitching, but only when player lets go of controls
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
    }

}