//Author: Richard Osborn
//Date 11/9/18
//description
//attach this monoBehavior to an object, and it will orbit around the orbitParent transform

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbiterController : MonoBehaviour {
    public Transform orbitParent;
    public GameObject orbitingObject;
    public float orbitSpeed;
    public float orbitalRadius = 2.0f;
    public bool orbitClockwise = true;

	// Use this for initialization
	void Start () {
        if(orbitParent == null)
        {
            Debug.LogError("ERROR! orbitParent not set on " + this.gameObject.name);
            //set at runtime
        }

        orbitingObject.transform.position = (orbitingObject.transform.position - orbitParent.position).normalized * orbitalRadius + orbitParent.position;
		
	}
	
	// Update is called once per frame
	void Update () {
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

    private void OnTriggerEnter(Collider col)//does not work! collider not on this object
    {
        Debug.Log("OnTriggerEnter()!!!!");
        if (col.gameObject.CompareTag("Enemy"))//if object's tag in list of objects can destroy....
        {
            Destroy(col.gameObject);
        }

    }
}
