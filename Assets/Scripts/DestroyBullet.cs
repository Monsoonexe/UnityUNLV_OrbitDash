using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {

    private bool checkDestroy;
    public GameObject playerObject;
    public PlayerController player;

    // Use this for initialization
    void Start () {
        checkDestroy = false;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(player.movingCheck())
        {
            checkDestroy = true;
        }
        else
        {
            checkDestroy = false;
        }
       

	}

    private void OnTriggerEnter2D(Collider2D col)//does not work! collider not on this object
    {
        Debug.Log("OnTriggerEnter()!!!!");
        if (checkDestroy)
        {
            if (col.gameObject.CompareTag("Enemy"))//if object's tag in list of objects can destroy....
            {
                Destroy(col.transform.parent.gameObject);//destroy the parent game object
            }
        }
    }
}
