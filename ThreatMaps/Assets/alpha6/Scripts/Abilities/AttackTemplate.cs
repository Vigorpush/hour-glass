using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AttackTemplate : MonoBehaviour {
	//TODO 4 cardinal classes should have interfaces for their unique actions
	public int damage;
	public string damageType;// { get; set;}
	public float consistency;// { get; set;}
	Vector3 abilityOrigin;// { get; set;}
	Vector3 abilityDestination;// { get; set;}
	public GameObject executor;// { get; set;}
	public List<GameObject> target;// { get; set;}  //This can be a coord or a unit    : use a if typeof...
    public bool hasASpellAnimation;
    public bool isARangerAbility;
	public SpriteRenderer img;

	//BARE CONSTRUCTOR
	//Comes out naked
	public AttackTemplate(){ }
	//PSEUDOCODE
	//Use this for initialization
	//Use the ability
	public void Execute(){ 
	}

    public abstract void CheckLine(); /*{
      List<RaycastHit2D> blankList = new List<RaycastHit2D>();
      return blankList;
    }*/

    public abstract int[] GetDamageSteps();/*
    {
        /*int[] i = new int[3];
        i[0] = 1;
        i[1] = 1;
        i[2] = 1;
        return i;
    }*/

    public abstract string getMyName();

    public abstract List<KeyCode> GetComboInputSequence();
    /*
    {
        List<KeyCode> noCodes = new List<KeyCode>();
        Debug.Log("abstract class made an empty list");
        return noCodes;
    }*/

}
