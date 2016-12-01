using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyUnitFactory : MonoBehaviour
{
    public int AMT = 5;
    protected static EnemyUnitFactory instance;

    public GameObject ZombiePrefab;
    public GameObject GoblinPrefab;
    public GameObject SkeletonPrefab;

    // Use this for initialization
    void Start()
    {
        instance = this;  
    }

    public void MoveHere(int xIn, int yIn)
    {
        this.transform.position = new Vector3(xIn,yIn, 0);

        //Debug.Log("Spawner moving to :"+ xIn + ", " + yIn);
        PlaceSomeZombies();
    }

    private void PlaceSomeZombies()
    {
        for(int i =0;i<AMT;i++){
            Vector3 change = new Vector3(UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(-2, 2), 0);
            this.transform.position += change;

           // Debug.Log("Position on loop "+ i + " location is " + this.transform.position);

            CreateZombie();
            //Invoke("CreateZombie",0.1f);
        }
    //Debug.Log("ding done");
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