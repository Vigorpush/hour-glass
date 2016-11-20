using UnityEngine;
using System.Collections.Generic;
using System;

//A class for storing the current credit cost of everyUnit
public static class CreditTable {
	
	Dictionary<String,CreditTableEntry> table;

	static class CreditTableEntry {
		
		//Initializes the table entry
		CreditTableEntry(EnemyUnit u){
			unit = u;
			cost = u.creditValue;
		}
		EnemyUnit unit;
		float cost;
		public void alterCost(float value){
			cost += value;
		}
		//Makes the unit 

	}
	public bool addEntry(EnemyUnit unit){
		if (table.ContainsKey (unit.getname ())) {
			throw(new Exception ("Already in table!!!"));
			return false;
		}
		CreditTableEntry newEntry = new CreditTableEntry (unit);
		table.Add (unit.getname (), newEntry);

	}
	public bool raiseCost(EnemyUnit unit, float amount){
		if (table.ContainsKey (unit.name)) {
			CreditTableEntry toBeRaised = table.TryGetValue (unit.name);
			toBeRaised.alterCost (amount);

		}

	}
	public bool cheapen(EnemyUnit unit,float amount){
		if (table.ContainsKey (unit.name)) {
			CreditTableEntry toBeRaised = table.TryGetValue (unit.name);
			toBeRaised.alterCost (amount);

		}

	} 
	// Use this for initialization

	
	// Update is called once per frame

}
