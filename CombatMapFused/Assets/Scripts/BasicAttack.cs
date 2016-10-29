using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class BasicAttack : AttackTemplate {

	private LineRayCast cast;
	public enum BasicAttacks{

		//WAR1= ,
		//GRASS=1,
		//SAND = 2,

	}
	//Ray Shape Type

	//effect class?

	public BasicAttack(){
		//executor = this.gameObject;

	}
	List<GameObject>  FindTargets(){
		Debug.Log ("Seeking target");
	
		return cast.CheckLine ();
	}
	void Execute(){ 
		
	
	}

	

}
