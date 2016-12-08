using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyUnitFactory : MonoBehaviour
{
    public int AMT = 2;
    protected static EnemyUnitFactory instance;

    public GameObject ZombiePrefab;
	public GameObject TankyGhostPrefab;
    public GameObject GoblinPrefab;
    public GameObject SkeletonPrefab;
	public GameObject VoidEaterPrefab;

    // Use this for initialization
    void Start()
    {
        instance = this;  
    }

    public void MoveHere(int xIn, int yIn)
    {
        this.transform.position = new Vector3(xIn,yIn, 0);

        //Debug.Log("Spawner moving to :"+ xIn + ", " + yIn);
		if (UnityEngine.Random.Range (0f, 1f) > 0.3f) {     //70% chance of normal encounter
			PlaceSomeZombies ();
		} else {
            hardEncounter();

		}
			
    }

    private void hardEncounter()
    {
        Debug.Log("Hard Encounter");
        AMT = 8;
        for (int i = 0; i < AMT; i++)
        {
            Vector3 hardChange = new Vector3(UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(-2, 2), 0);
            this.transform.position += hardChange;

            // Debug.Log("Position on loop "+ i + " location is " + this.transform.position);
            float rand = UnityEngine.Random.Range(0f, 1f);
            if (rand < .1f)
            {
                CreateZombie();
                

            }
            else if( rand < .3f)
            {

                PlaceTanker();
            }
            else if (rand < .7f)
            {
                CreateVoidEater();
                

            }

            //Invoke("CreateZombie",0.1f);
        }

    }
    private void PlaceSomeZombies()
    {
        AMT = 4 + (UnityEngine.Random.Range(-2,2));
        for(int i =0;i<AMT;i++){
            Vector3 change = new Vector3(UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(-2, 2), 0);
            this.transform.position += change;

           // Debug.Log("Position on loop "+ i + " location is " + this.transform.position);
			if (UnityEngine.Random.Range (0f, 1f) > .3f) {
				CreateZombie ();
				

			} else {
				
				PlaceTanker ();
			}

            
            //Invoke("CreateZombie",0.1f);
        }
        Vector3 change3 = new Vector3(UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(-2, 2), 0);
        this.transform.position += change3;
        CreateVoidEater();
    //Debug.Log("ding done");
    }

	private void oneVoid(){
		CreateVoidEater ();
	}
	public GameObject CreateVoidEater()
	{
		GameObject zombie = (GameObject)UnityEngine.Object.Instantiate(instance.VoidEaterPrefab, this.transform.position, Quaternion.identity);
		return zombie;
	}
	private GameObject PlaceTanker(){
		GameObject TankyGhost = (GameObject)UnityEngine.Object.Instantiate(instance.TankyGhostPrefab, this.transform.position, Quaternion.identity);
		return TankyGhost;
	}


    public GameObject CreateZombie()
    {
        GameObject zombie = (GameObject)UnityEngine.Object.Instantiate(instance.ZombiePrefab, this.transform.position, Quaternion.identity);
        return zombie;
    }

    public GameObject createGoblin()
    {
        GameObject goblin = (GameObject)UnityEngine.Object.Instantiate(instance.GoblinPrefab, this.transform.position, Quaternion.identity);
        return goblin;
    }

    public GameObject createSkeleton()
    {
        GameObject skeleton = (GameObject)UnityEngine.Object.Instantiate(instance.SkeletonPrefab, this.transform.position, Quaternion.identity);
        return skeleton;
    }
   
}