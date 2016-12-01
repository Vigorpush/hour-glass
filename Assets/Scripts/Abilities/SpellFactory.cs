using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SpellFactory : MonoBehaviour {

    protected static SpellFactory instance;

    public GameObject fireballPrefab;
    public GameObject lightningStormPrefab;
    public GameObject missileStormPrefab;

    // Use this for initialization
    void Start () {
        instance = this;

    }
	
    public GameObject CreateFireball(int damage, GameObject target, GameObject caster)
    {
        //Debug.Log("Casting a fireball?");
       Debug.Log("Spell casting from here: " + caster.transform.position);
        GameObject spell = (GameObject)UnityEngine.Object.Instantiate(instance.fireballPrefab, caster.transform.position, Quaternion.identity);
        Debug.Log(spell.GetComponent<FireballFlight>().gameObject);
        spell.GetComponent<FireballFlight>().Initialize(damage,target,caster);
        return spell;
    }

    public List<GameObject> CreateLightningStorm(int damage, List<GameObject> targets, GameObject caster)
    {
        List<GameObject> spells = new List<GameObject>();
        Debug.Log("Length of spell list is "+targets.Count+ instance.lightningStormPrefab.gameObject.name);
        foreach (GameObject tar in targets)
        {
            GameObject thisSpell = (GameObject)UnityEngine.Object.Instantiate(instance.lightningStormPrefab, Vector3.zero, Quaternion.identity);
            thisSpell.GetComponent<LightningStorm>().Initialize(damage, tar, caster);
            spells.Add(thisSpell);     
        }
        return spells;
    }

    public List<GameObject> CreateMissileBarrage(int damage, List<GameObject> targets, GameObject caster)
    {
        List<GameObject> spells = new List<GameObject>();
        foreach (GameObject tar in targets)
        {
            GameObject thisSpell = (GameObject)UnityEngine.Object.Instantiate(instance.missileStormPrefab, Vector3.zero, Quaternion.identity);
            thisSpell.GetComponent<MissileBarrageBehavior>().Initialize(damage, tar, caster);
            spells.Add(thisSpell);

        }
        return spells;
    }
}
