//Author: Richard Osborn
//Date 11/11/18
//Spawns things
using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject[] thingsToSpawn;
    [Header("Weights not yet implemented.")]
    public int[] weights; //one thingsToSpawn should have one weight. There should always be more higher weight than lower weighted things
    public Transform[] spawnPoints;
    public int maxThingsAliveAtOneTime;
    public int minDelayBetweenSpawns;
    public int maxDelayBetweenSpawns;
    public bool spawning = true;

    public GameObject[] listOfExistingObjects;

    private bool DetermineValidSetup()
    {
        bool setupIsValid = true;

        if (thingsToSpawn == null ||thingsToSpawn.Length  < 1)
        {
            Debug.LogError("ERROR! There is nothing to spawn!");
            setupIsValid = false;
        }
        if (spawnPoints == null || spawnPoints.Length < 1)
        {
            Debug.LogError("ERROR! There are no valid spawn points!");
            setupIsValid = false;
            //check children for spawn points and try to assign them. retry
        }

        return setupIsValid;
    }

	// Use this for initialization
	void Start ()
    {
        //check for valid setup configuration of thingsToSpawn and spawnPoints
        if (!DetermineValidSetup())
        {
            Debug.Log("Destroying self....");
            Destroy(this.gameObject);
        }
        listOfExistingObjects = new GameObject[maxThingsAliveAtOneTime];

        StartCoroutine(StartSpawning());
        
	}

    //counts how many objects in the list are still alive
    private static int CountAliveObjects(GameObject[] listOfObjects)
    {
        int accumulator = 0;
        foreach(GameObject obj in listOfObjects)
        {
            if (obj != null) ++accumulator;
        }

        return accumulator;        
    }

    private IEnumerator StartSpawning()
    {
        int objectsAlive;//will count how many objects in listOfExistingObjects is not null
        Transform parent; 
        while (spawning)//unless told otherwise, do this forever
        {
            objectsAlive = CountAliveObjects(listOfExistingObjects);//how many alive objects are there in this list?
            if (objectsAlive < maxThingsAliveAtOneTime)//do we need to spawn more things?
            {
                Debug.Log("Spawning!!!");
                parent = spawnPoints[(int)Random.Range(0, spawnPoints.Length)];//pick a random parent
                for(int i = 0; i < listOfExistingObjects.Length; ++i)//look through each element of the list until you find an empty spot
                {
                    if(listOfExistingObjects[i] == null)//if this is an empty slot
                    {
                        //fill slot with newly instantiated object
                        listOfExistingObjects[i] = Instantiate(thingsToSpawn[(int)Random.Range(0, thingsToSpawn.Length)], //spawn a random object from this list
                            parent.position, parent.rotation);//relative to its parent
                        //Debug.Log(parent.rotation);
                        break;//only do this once per check
                    }//end if
                }//end for
                
            }//end if
            yield return new WaitForSeconds(Random.Range(minDelayBetweenSpawns, maxDelayBetweenSpawns));//randomizes time between spawns
        }
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        
		
	}
}
