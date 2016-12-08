using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyUnitFactory : MonoBehaviour
{
    public int AMT = 1;
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
		if (UnityEngine.Random.Range (0f, 1f) > .5f) {
			//PlaceSomeZombies ();
			oneVoid ();
		} else {
			oneVoid ();
		}
			
    }

    private void PlaceSomeZombies()
    {
        for(int i =0;i<AMT;i++){
            Vector3 change = new Vector3(UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(-2, 2), 0);
            this.transform.position += change;

           // Debug.Log("Position on loop "+ i + " location is " + this.transform.position);
			if (UnityEngine.Random.Range (0f, 1f) > .5f) {
				CreateZombie ();
				CreateVoidEater ();

			} else {
				
				PlaceTanker ();
			}
            
            //Invoke("CreateZombie",0.1f);
        }
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