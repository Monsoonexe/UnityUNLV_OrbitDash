// Author: Richard Osborn
// Date: 11/15/18
// Summary: uses transform.Rotate() on object it is attached to at a rate set in Inspector. 
//
//


using UnityEngine;

public class Rotator : MonoBehaviour {
    [Header("Rotate Along These Axes at Speed")]
    //[Range(-1, 1)]
    public Vector3 rotationVector;
	
	// Update is called once per frame
	void Update () {
        RotateThisObject();
	}

    //allows other code to see if this is rotating

    void RotateThisObject()
    {
        this.transform.Rotate(rotationVector * Time.deltaTime);
    }
}
