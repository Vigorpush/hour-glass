using UnityEngine;
using System.Collections;

public class SpecialAbility : BasicAttack {
	//In turns
	private const int BASE_CD= 1;
	public int coolDown,coolDownLeft;


	// Use this for initialization
	public int TriggerCD(){
		coolDownLeft = BASE_CD;
		return coolDownLeft;
	}

	public bool isCD(){
		return coolDownLeft == 0;
	}

}
