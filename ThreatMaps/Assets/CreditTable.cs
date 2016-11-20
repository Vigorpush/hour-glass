using UnityEngine;
using System.Collections;
using System;


public class CreditTable {
	HashMap<String,CreditTableEntry> table;

	class CreditTableEntry {
		//Initializes the table entry
		CreditTableEntry(EnemyUnit u){
			unit = u;
			cost = u.creditValue;
		}
		EnemyUnit unit;
		float cost;
		//Makes the unit 
		void raiseCost(float amount){
			cost += amount;
		}
		void cheapen(float amount){
			cost -= amount;
		}
	}
	bool addEntry(){

	}
	// Use this for initialization

	
	// Update is called once per frame

}
