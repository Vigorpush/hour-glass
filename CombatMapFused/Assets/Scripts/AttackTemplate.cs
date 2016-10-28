using UnityEngine;
using System.Collections;

public abstract class AttackTemplate : MonoBehaviour {
	
	//TODO 4 cardinal classes should have interfaces for their unique actions
	public int damage { get; set;}
	public string damageType { get; set;}
	public float consistency { get; set;}
	Vector3 abilityOrigin { get; set;}
	Vector3 abilityDestination { get; set;}
	GameObject executor { get; set;}
	GameObject target { get; set;}  //This can be a coord or a unit    : use a if typeof...

	//PSEUDOCODE
	// Use this for initialization
	//Use the ability
	void Execute(){

	}
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
	
	}


}
