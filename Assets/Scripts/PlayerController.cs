using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour { 
    public float moveSpeed = 100.0f;
    public OrbiterController orbiterController;

    private Vector3 startPosition;
    private Vector3 targetMovePosition;
    private bool moving = false;
	private Rigidbody rb;

    //these values used for linear interpolation (lerp)
    private float moveStartTime;
    private float movementLength;
    private float distanceCovered;
    private float percentOfJourneyCompleted;


    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        /*if (Input.GetButtonDown("Jump") && !moving)
        {
            //Debug.Log("Jump pressed!");//print test
            
            //for lerping
            moving = true;
            targetMovePosition = orbiterController.GetOrbiterLocalPosition();
            startPosition = this.transform.position;
            moveStartTime = Time.time;//start
            movementLength = Vector3.Distance(targetMovePosition, startPosition);//how far do I have to move?

            //this.transform.position = orbiterController.GetOrbiterWorldPosition();//teleport broken
        }*/

        if (moving)
        {
            Move();
        }
        
        //If key is held down, increase the radius of orbiter
        if(Input.GetButton("Jump") && !moving)
        {
            orbiterController.changeRadius(true);
        }

        else
        {
            orbiterController.changeRadius(false);
        }

        if(Input.GetButtonUp("Jump") && !moving)
        {
            moving = true;
            targetMovePosition = orbiterController.GetOrbiterLocalPosition();
            startPosition = this.transform.position;
            moveStartTime = Time.time;//start
            movementLength = Vector3.Distance(targetMovePosition, startPosition);
        }

    }

    private void Move()
    {
        //Debug.Log("MOVE IT!");//print test
        distanceCovered = ((Time.time - moveStartTime) * moveSpeed);
        percentOfJourneyCompleted = distanceCovered / movementLength;
        //Debug.Log("StartPos = " + startPosition + " " + "targetMovePosition = " + targetMovePosition + " " + percentOfJourneyCompleted + "%");//print test
        Vector3 movement = Vector3.Lerp(startPosition, targetMovePosition, percentOfJourneyCompleted);
        rb.AddForce(movement*speed);
		if (percentOfJourneyCompleted >= .95f)
            moving = false;
    }

    public bool movingCheck()
    {
        //used for checking by destroy bullet script
        //if true, destroy
        //if false, game over 
        return moving;
    }
}
