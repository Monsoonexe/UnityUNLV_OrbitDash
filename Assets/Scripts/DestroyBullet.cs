using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {

    public bool checkDestroy;
    public GameObject playerObject;
    public PlayerController player;

    // Use this for initialization
    void Start () {
        checkDestroy = false;
	}
	
	// Update is called once per frame
	void Update () {
		
            checkDestroy = player.movingCheck();
       

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
        //else if not moving and touch enemy, game over 
    }
}
