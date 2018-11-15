using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour { 
    public float moveSpeed = 1.0f;
    public OrbiterController orbiterController;

    private Vector3 startPosition;
    private Vector3 targetMovePosition;
    private bool moving = false;

    //these values used for linear interpolation (lerp)
    private float moveStartTime;
    private float movementLength;
    private float distanceCovered;
    private float percentOfJourneyCompleted;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Jump"))
        {
            //Debug.Log("Jump pressed!");//print test
            
            //for lerping
            moving = true;
            targetMovePosition = orbiterController.GetOrbiterLocalPosition();
            startPosition = this.transform.position;
            moveStartTime = Time.time;//start
            movementLength = Vector3.Distance(targetMovePosition, startPosition);//how far do I have to move?

            //this.transform.position = orbiterController.GetOrbiterWorldPosition();//teleport broken
        }
        if (moving)
        {
            Move();
        }
        

    }

    private void Move()
    {
        //Debug.Log("MOVE IT!");//print test
        distanceCovered = ((Time.time - moveStartTime) * moveSpeed);
        percentOfJourneyCompleted = distanceCovered / movementLength;
        //Debug.Log("StartPos = " + startPosition + " " + "targetMovePosition = " + targetMovePosition + " " + percentOfJourneyCompleted + "%");//print test
        this.transform.position = Vector3.Lerp(startPosition, targetMovePosition, percentOfJourneyCompleted);

    }
}
