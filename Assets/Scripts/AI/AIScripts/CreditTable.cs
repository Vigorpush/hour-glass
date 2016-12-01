using UnityEngine;
using System.Collections.Generic;
using System;

//A class for storing the current credit cost of everyUnit
public class CreditTable : MonoBehaviour {
	
	public Dictionary<String,CreditTableEntry> table;

	 public class CreditTableEntry {
		
		//Initializes the table entry
		public 	CreditTableEntry(EnemyUnit u){
			 unit = u;
			 cost = u.creditValue;
		}
		//FOR TESTING
		public 	CreditTableEntry(float cred){
			
			cost = cred;
		}
		public EnemyUnit unit;
		public float cost;
		public void alterCost(float value){
			cost += value;
		}
		/*
		public String ToString(){

		}
		*/
		//Makes the unit 

	}
	void Start(){
		table = new Dictionary<String,CreditTableEntry> ();
	//	Debug.Log ("Started the credit table.");
		//Debug.Log (table.ToString());
		table.Add("Goblin",new CreditTableEntry(25f));
		CreditTableEntry dummy = new CreditTableEntry (35f);
		//Debug.Log(table.TryGetValue(("Goblin"),out dummy ));
		displayTable ();

		//TESTING cheapen and raiseCost
		table["Goblin"].alterCost(10);
		//Debug.Log ("RAISING THE PRICE OF GOBLIN BY 10, EXPECT 35:" + table ["Goblin"].cost);
		table["Goblin"].alterCost(-20);
		//Debug.Log ("LOWERING THE PRICE OF GOBLIN BY 20, EXPECT 15:" + table ["Goblin"].cost);
		//TESTING LOOKUP
		//Debug.Log("Lookup should yield 15: " + lookUp("Goblin"));

	}
	void displayTable(){
		/*Debug.Log ("VALUES");
		foreach (CreditTableEntry te in table.Values) {
			
			Debug.Log ("cost:" + te.cost);
		}
		Debug.Log ("NAMES");
		Debug.Log (table.Keys.ToString ());
		Debug.Log("Accessing the dictionary by [\"string] :" + table["Goblin"].cost);*/


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
		Debug.Log ("Adding " + unit.name +" the credit table.");
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
			Debug.Log ("Reducing the price of " + unit.name + " by " + amount +" it now costs " + lookUp(unit.name));
			return true;
		}
		return false;

	} 

	//Returns negative 1 if failure
//	public float lookUp(EnemyUnit unit){
	public float lookUp(String name){
		if (table.ContainsKey (name)) {
			CreditTableEntry target = new CreditTableEntry (5f);
			table.TryGetValue (name,out target);
			return target.cost;
		} else {

			return -1f;
		}

	}
	// Use this for initialization

	
	// Update is called once per frame

}
