using UnityEngine;
using System.Collections;

public abstract class Unit : MonoBehaviour {
	public int hp,initiative,speed,range,attack;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//Returns if dead
	void Damage(){
		Debug.Log ("Oww");
	}

	void Die(){

		Debug.Log ("I died");
	}
}
