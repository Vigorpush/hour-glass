using UnityEngine;
using System.Collections.Generic;
using System;

//A class for storing the current credit cost of everyUnit
public class CreditTable {
	
	Dictionary<String,CreditTableEntry> table;

	 public class CreditTableEntry {
		
		//Initializes the table entry
		public 	CreditTableEntry(EnemyUnit u){
			 unit = u;
			 cost = u.creditValue;
		}
		public EnemyUnit unit;
		public float cost;
		public void alterCost(float value){
			cost += value;
		}
	
		//Makes the unit 

	}
	public float cheapestPrice(){
		if (table.Count == 0) {
			throw(new Exception ("NO UNITS IN TABLE CAN'T GET CHEAPEST!"));
		}
				float cheapest = Int32.MaxValue;
		foreach (KeyValuePair<string,CreditTable.CreditTableEntry> pair in table) {
			{
				if (pair.Value.cost < cheapest) {
					cheapest = pair.Value.cost;
				}
			}
		}
		return cheapest;

	}
	public bool addEntry(EnemyUnit unit){
		if (table.ContainsKey (unit.name)) {
			throw(new Exception ("Already in table!!!"));
			return false;
		}
		CreditTableEntry newEntry = new CreditTableEntry (unit);
		table.Add (unit.name, newEntry);
		return true;
	}
	public bool raiseCost(EnemyUnit unit, float amount){
		Debug.Log ("Raising the price of " + unit.name + " by " + amount);
		if (table.ContainsKey (unit.name)) {
			CreditTableEntry target = new CreditTableEntry (unit);
			table.TryGetValue(unit.name,out target);
			target.alterCost (amount);
			return true;

		}
		return false;
	}
	public bool cheapen(EnemyUnit unit,float amount){
		
		if (table.ContainsKey (unit.name)) {
						CreditTableEntry target = new CreditTableEntry (unit);
			table.TryGetValue (unit.name,out target);	
		

			target.alterCost (-amount);
			Debug.Log ("Reducing the price of " + unit.name + " by " + amount +" it now costs " + lookUp(unit));
			return true;
		}
		return false;

	} 

	//Returns negative 1 if failure
	public float lookUp(EnemyUnit unit){
		if (table.ContainsKey (unit.name)) {
			CreditTableEntry target = new CreditTableEntry (unit);
			table.TryGetValue (unit.name,out target);
			return target.cost;
		} else {

			return -1f;
		}

	}
	// Use this for initialization

	
	// Update is called once per frame

}
