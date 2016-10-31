using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;



public class TurnManager : MonoBehaviour
{
    //The main Camera!
    GameObject cam;

    //Holds the pairs
    [System.Serializable]
	public class UnitInitiativeP : IComparable
	{
		public Unit u;
		public int initiative;
		//Compare by initiative
		public void updateInit(int amt){

			initiative += amt;
		}
		public int CompareTo (object otherUnit)
		{
			//Debug.Log("Comparing" + u.name + ":" + u.initiative+ " to "  + ((UnitInitiativeP)otherUnit).u.name + ":" + ((UnitInitiativeP)otherUnit).initiative) ;
			if (otherUnit == null) {
				return 1;

			}
			int result = initiative-((UnitInitiativeP)otherUnit).initiative ;
			//other is slower
			if (result < 0) {
				Debug.Log (initiative + " was less than " + ((UnitInitiativeP)otherUnit).initiative);
				return -1;
			//other is the same
			} else if (result == 0) {
				Debug.Log (initiative + " was equal to " + ((UnitInitiativeP)otherUnit).initiative);
				return 1;
			//other is faster
			} else {
				Debug.Log (initiative + " was greater than " + ((UnitInitiativeP)otherUnit).initiative);
				return 1;
			}
			//Might need to be changed..

		}

		public UnitInitiativeP (Unit unit, int init)
		{
			u = unit;
			initiative = init;

		}
	}
	//TURN MANAGER MEMBER VARIABLES

	public List<GameObject> players;
	//SortedDictionary<int,GameObject> currentInitiativeTable;
	//Contains all original initiatives to get via object   MAPS UNIT->INITIATIVE STAT
	public Dictionary<Unit,int> baseInitiativeTable;

	
	public List<UnitInitiativeP> currentInitiatives;

	GameObject player1;
	GameObject player2;
	GameObject player3;

	//FOR SWAPPING TURN ON BUTTON PRESS
	GameObject endTurnButton;

	public Unit currentUnit;

	public void Start()
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera");
		GetAllPlayers();
		initializeTables();
	}
	void setUpButton ()
	{
		endTurnButton = GameObject.Find ("EndTurnButton");
	}
	// Use this for initialization

	void initializeTables ()
	{
		baseInitiativeTable = new Dictionary<Unit,int>();
		currentInitiatives = new List<UnitInitiativeP>();

		//Fill the tables

		//Get all players and units by calling respective methods
		addPlayersToTable();
		currentInitiatives.Sort ();
		CalculateTurn();
	}



	void addPlayersToTable ()
	{
		
		currentInitiatives.Add (new UnitInitiativeP (player1.GetComponent<HeroUnit> (), player1.GetComponent<HeroUnit> ().initiative));
		baseInitiativeTable.Add (player1.GetComponent<HeroUnit>(), player1.GetComponent<HeroUnit> ().initiative);


		currentInitiatives.Add (new UnitInitiativeP (player2.GetComponent<HeroUnit> (), player2.GetComponent<HeroUnit> ().initiative));
		baseInitiativeTable.Add (player2.GetComponent<HeroUnit> (), player2.GetComponent<HeroUnit> ().initiative);


		currentInitiatives.Add (new UnitInitiativeP (player3.GetComponent<HeroUnit> (), player3.GetComponent<HeroUnit> ().initiative));
		baseInitiativeTable.Add (player3.GetComponent<HeroUnit> (), player3.GetComponent<HeroUnit> ().initiative);
  
    }



	void GetAllUnits ()
	{
		GameObject[] units = GameObject.FindGameObjectsWithTag ("Enemy");

	}

	void GetAllPlayers ()
	{
		players = new List<GameObject> ();
		player1 = GameObject.FindGameObjectWithTag ("Player1");
		player2 = GameObject.FindGameObjectWithTag ("Player2");
		player3 = GameObject.FindGameObjectWithTag ("Player3");
		players.Add (player1);
		players.Add (player2);
		players.Add (player3);

	}
	public void CalculateTurn ()
	{
		currentInitiatives.Sort ();
		UnitInitiativeP curPair = currentInitiatives [0];
		currentUnit = curPair.u;
		currentInitiatives.RemoveAt(0);
		curPair.updateInit (currentUnit.initiative);
		currentInitiatives.Add (new UnitInitiativeP(curPair.u,curPair.initiative));
		currentInitiatives.Sort ();
		currentUnit = curPair.u;

		MakeTurn ();



	}
		
	int numTurns=0;
	void MakeTurn ()
	{
		
		currentUnit.SendMessage ("StartTurn");
		numTurns++;
		Debug.Log ("Turn " + numTurns + currentUnit.name);

		cam.SendMessage ("SetCameraFollow", currentUnit.gameObject);
	}

   
}