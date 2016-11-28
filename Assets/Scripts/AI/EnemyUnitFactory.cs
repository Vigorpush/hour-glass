using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyUnitFactory : MonoBehaviour
{

    protected static EnemyUnitFactory instance;

    public GameObject ZombiePrefab;
    public GameObject GoblinPrefab;
    public GameObject SkeletonPrefab;

    // Use this for initialization
    void Start()
    {
        instance = this;

    }

    public GameObject CreateZombie()
    {
        GameObject zombie = (GameObject)UnityEngine.Object.Instantiate(instance.ZombiePrefab, Vector3.zero, Quaternion.identity);
        return zombie;
    }

    public GameObject createGoblin()
    {
        GameObject goblin = (GameObject)UnityEngine.Object.Instantiate(instance.GoblinPrefab, Vector3.zero, Quaternion.identity);
        return goblin;
    }

    public GameObject createSkeleton()
    {
        GameObject skeleton = (GameObject)UnityEngine.Object.Instantiate(instance.SkeletonPrefab, Vector3.zero, Quaternion.identity);
        return skeleton;
    }

}