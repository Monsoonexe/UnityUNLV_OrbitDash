using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour { 
    public float dashSpeed = 100.0f; //how quickly the player object moves towards the orbiter
    public OrbiterController orbiterController; //the script 

    private Vector3 dashStartPosition; //where the playerModel is when the dash began
    private Vector3 dashEndPosition;//the endpoing of the dash
    private bool isDashing = false;//is currently dashing?
    private bool dashCooledDown; //time when the dash began
    [SerializeField] private float dashCooldownTime; //[attribute] means this private variable will be visible in Inspector

	private Rigidbody2D rb;//

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody2D>();//get a reference to the rb
	}
	
	// Update is called once per frame
	void Update () {
        
        //If key is held down, increase the radius of orbiter
        if(Input.GetButton("Jump") && !isDashing && dashCooledDown) //if the button is being held down
        {
            orbiterController.changeRadius(true);//charge the radius and increase size of orbit
        }

        else
        {
            orbiterController.changeRadius(false);
        }

        if(Input.GetButtonUp("Jump") && !isDashing && dashCooledDown)//if button has been released
        {
            isDashing = true;//playerHandler now dashing towards the orbiter
            dashEndPosition = orbiterController.GetOrbiterLocalPosition();//get the endpoint of the dash
            dashStartPosition = this.transform.position;//the startPoint is the position it was in when the button was pressed
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            Dash();//do it
            StartCoroutine(DashCooldown());//co routine runs separately, so this script continues on
        }
    }

    //Summary: basic ability cooldown
    //IEnumerator gives the function the ability to sleep, hang, halt, yield, wait
    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldownTime);//do nothing for this much time
        dashCooledDown = true;//it is now cooled down
    }

    private void Dash()
    {
        //Debug.Log("Dashing through the snow....");//print test
        
        rb.AddForce(dashEndPosition * dashSpeed);//add force in the direction of the endpoint at this speed
        isDashing = false; //immediately set to false so force is only applied once.
    }

    public bool movingCheck()
    {
        //used for checking by destroy bullet script
        //if true, destroy
        //if false, game over 
        return isDashing;
    }
}
