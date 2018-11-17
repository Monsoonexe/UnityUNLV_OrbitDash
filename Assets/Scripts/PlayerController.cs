using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float dashSpeed = 1.0f;
    public OrbiterController orbiterController;

    private Vector3 dashStartPosition;
    private Vector3 dashEndPosition;
    private bool isDashing = false;

    //these values used for linear interpolation (lerp)
    private float dashStartTime;
    private float movementLength;
    private float distanceCovered;
    private float percentOfJourneyCompleted;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        /*if (Input.GetButtonDown("Jump") && !isDashing)
        {
            //Debug.Log("Jump pressed!");//print test
            
            //for lerping
            isDashing = true;
            dashEndPosition = orbiterController.GetOrbiterLocalPosition();
            dashStartPosition = this.transform.position;
            moveStartTime = Time.time;//start
            movementLength = Vector3.Distance(dashEndPosition, dashStartPosition);//how far do I have to move?
            //this.transform.position = orbiterController.GetOrbiterWorldPosition();//teleport broken
        }*/

        if (isDashing)
        {
            Dash();
        }

        //If key is held down, increase the radius of orbiter
        if (Input.GetButton("Jump") && !isDashing)
        {
            orbiterController.changeRadius(true);
        }

        else
        {
            orbiterController.changeRadius(false);
        }

        if (Input.GetButtonUp("Jump") && !isDashing)
        {
            isDashing = true;
            dashEndPosition = orbiterController.GetOrbiterLocalPosition();
            dashStartPosition = this.transform.position;
            dashStartTime = Time.time;//start
            movementLength = Vector3.Distance(dashEndPosition, dashStartPosition);
        }

    }

    private void Dash()
    {
        //Debug.Log("MOVE IT!");//print test
        distanceCovered = ((Time.time - dashStartTime) * dashSpeed);
        percentOfJourneyCompleted = distanceCovered / movementLength;
        //Debug.Log("StartPos = " + dashStartPosition + " " + "dashEndPosition = " + dashEndPosition + " " + percentOfJourneyCompleted + "%");//print test
        this.transform.position = Vector3.Lerp(dashStartPosition, dashEndPosition, percentOfJourneyCompleted);
        if (percentOfJourneyCompleted >= .95)
            isDashing = false;
    }

    public bool movingCheck()
    {
        //used for checking by destroy bullet script
        //if true, destroy
        //if false, game over 
        return isDashing;
    }
}                           