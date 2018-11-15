//Author: Richard Osborn
//Date 11/11/18
//Description: Bullet class
//this object moves forward forever at a constant rate
//is destroyed when leaving PlayAreaBoundary TriggerVolume

using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] private float initialForce = 100.0f;//ex, when shot from a gun
    [SerializeField] private Vector2 direction;//ex, when shot from a gun
    //this [attribute] makes private variables visible in the Inspector.

    Bullet()//empty default constructor
    {
        ;
    }

    Bullet(float force)//constructor 
    {
        initialForce = force;//
    }

    //Bullet(float force, Vector3 desiredDirection)//constructor //probably not needed as Instantiate will determine orientation
    //{
    //    initialForce = force;//
    //    direction = desiredDirection;
    //}


    // Use this for initialization
    void Start () {
        
        if(this.transform.rotation.y == 1)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.right;
        }
        //Debug.Log(this.transform.rotation + " " + direction);
        this.GetComponent<Rigidbody2D>().AddForce(direction * initialForce, ForceMode2D.Force);//'forward'
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerExit2D(Collider2D collision)
    {
        //destroy this game object if it leaves the play area
        if (collision.gameObject.CompareTag("PlayAreaBoundary"))
        {
            Destroy(this.gameObject);
        }
    }
}
