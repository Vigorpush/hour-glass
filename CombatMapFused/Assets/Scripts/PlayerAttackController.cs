using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour {

	// Find the collection library public Hashtable<string,Abiity> playerAbilities;
	public ArrayList[] abilities;

	// Use this for initialization
	void Start () {
		//abilities = new ArrayList();
	}
	
	// Update is called once per frame
	/*
	void Update () {
		if(Input.GetButtonDown=="Fire1"){
			InitiateAbility (abilities[1]);
		}
		if(Input.GetButtonDown=="Fire2"){
			InitiateAbility (abilities[2]);
		}
		if(Input.GetButtonDown=="Fire3"){
			InitiateAbility (abilities[3]);
		}
		if(Input.GetButtonDown=="Fire4"){
			InitiateAbility (abilities[4]);
		}
	
	}
	/*
	/*
	public void InitiateAbility(ability){
		Debug.Log("Start casting abilitiy");
	}
	*/
}
