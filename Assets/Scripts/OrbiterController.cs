﻿//Author: Richard Osborn
//Date 11/9/18
//description
//attach this monoBehavior to an object, and it will orbit around the orbitParent transform

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbiterController : MonoBehaviour
{
    public Transform orbitParent;
    public GameObject orbitingObject;
    public float orbitSpeed;
    public float previousSpeed;
    public float orbitalRadius = 2.0f;
    public bool orbitClockwise = true;

    public float minRadius = 2.0f;
    public float maxRadius = 4.5f;
    public float orbitChargeRate = 1.03f;

	// Use this for initialization
	void Start ()
    {
        if(orbitParent == null)
        {
            Debug.LogError("ERROR! orbitParent not set on " + this.gameObject.name);
            //set at runtime
        }

       AdjustOrbit();
	}

    private void AdjustOrbit()
    {
        if (orbitalRadius <= 0)//errors occur if orbital radius is below 0
        {
            orbitalRadius = 0.5f;
        }

        if (Vector3.Distance(orbitingObject.transform.position, orbitParent.transform.position) != orbitalRadius)
        {
            float radiusDifference = orbitalRadius - Vector3.Distance(orbitingObject.transform.position, orbitParent.transform.position);
            orbitingObject.transform.Translate(radiusDifference, 0, 0, Space.Self);
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetKey("a"))
        //{
        //    orbitalRadius += 1 * Time.deltaTime;
        //}
        
        if (Input.GetButtonDown("Jump"))
        {
            previousSpeed = orbitSpeed;
            orbitSpeed = 0;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            orbitSpeed = previousSpeed;
        }
        AdjustOrbit();
        orbitingObject.transform.RotateAround(orbitParent.position, Vector3.forward, orbitSpeed * Time.deltaTime);
		
	}

    public Vector3 GetOrbiterLocalPosition()
    {
        return orbitingObject.transform.position;
    }

    private void OnGUI()
    {
        if (GUILayout.Button("ReverseOrbit"))
        {
            ReverseOrbit();
        }
    }

    public void ReverseOrbit()
    {
        orbitSpeed *= -1;

    }

    private void OnTriggerEnter2D(Collider2D col)//does not work! collider not on this object
    {
        Debug.Log("OnTriggerEnter()!!!!");
        if (col.gameObject.CompareTag("Enemy"))//if object's tag in list of objects can destroy....
        {
            Destroy(col.transform.parent.gameObject);//destroy the parent game object
        }

    }

    public void changeRadius(bool x)
    {
        //used by playercontroller
        //change the radius by a step for every frame the key is held down
        //reduce radius if not held down
        //max, min, and step are public for now

        if(orbitalRadius < maxRadius && x == true)
        {
            orbitalRadius = Mathf.Pow(orbitalRadius, orbitChargeRate);
        }

        if(orbitalRadius > minRadius && x == false)
        {
            orbitalRadius -= orbitChargeRate;
        }


    }
    
}
