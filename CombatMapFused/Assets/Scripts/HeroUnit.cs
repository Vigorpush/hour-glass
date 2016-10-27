using UnityEngine;
using System.Collections;

public class HeroUnit :  Unit  {

	// Use this for initialization
	void Start () {
	
	}
		
	// Update is called once per frame
	void Update () {
	
	}

	void StartTurn(){
		SendMessage ("AllowMovement");
		Debug.Log( this.tag +" Starting turn.");

	}
}
