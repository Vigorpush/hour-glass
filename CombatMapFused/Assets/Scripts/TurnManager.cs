using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;



public class TurnManager : MonoBehaviour
{
	//Holds the pairs
	[System.Serializable]
	public class UnitInitiativeP : IComparable
	{
		public Unit u;
		public int initiative;
		//Compare by initiative
		public int CompareTo (object otherUnit)
		{
			if (otherUnit == null) {
				return 1;
			}
			int result = this.u.initiative - ((UnitInitiativeP)otherUnit).initiative;
			if (result < 0) {
				return -1;
			} else if (result == 0) {
				return 1;
			} else {

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

	private List<GameObject> players;
	SortedDictionary<int,GameObject> currentInitiativeTable;
	//Contains all original initiatives to get via object
	Dictionary<GameObject,int> baseInitiativeTable;
	//The main Camera!
	GameObject cam = GameObject.FindGameObjectWithTag ("MainCamera");
	public List<UnitInitiativeP> currentInitiatives;


	GameObject player1;
	GameObject player2;
	GameObject player3;

	//FOR SWAPPING TURN ON BUTTON PRESS
	GameObject endTurnButton;

	public GameObject currentUnit;


	void setUpButton ()
	{
		endTurnButton = GameObject.Find ("EndTurnButton");
	}
	// Use this for initialization

	void initializeTables ()
	{

		baseInitiativeTable = new Dictionary<GameObject,int> ();
		currentInitiativeTable = new SortedDictionary<int,GameObject> ();
		currentInitiatives = new List<UnitInitiativeP> ();

		//Fill the tables

		//Get all players and units by calling respective methods
		addPlayersToTable ();


		CalculateTurn ();

	}

	void addPlayersToTable ()
	{

		currentInitiatives.Add (new UnitInitiativeP (player1.GetComponent<HeroUnit> (), player1.GetComponent<HeroUnit> ().initiative));
		baseInitiativeTable.Add (player1, player1.GetComponent<HeroUnit> ().initiative);

		currentInitiatives.Add (new UnitInitiativeP (player2.GetComponent<HeroUnit> (), player2.GetComponent<HeroUnit> ().initiative));
		baseInitiativeTable.Add (player2, player2.GetComponent<HeroUnit> ().initiative);

		currentInitiatives.Add (new UnitInitiativeP (player3.GetComponent<HeroUnit> (), player3.GetComponent<HeroUnit> ().initiative));
		baseInitiativeTable.Add (player3, player3.GetComponent<HeroUnit> ().initiative);
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
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetButton ("Jump") && currentUnit != null) {
			currentUnit.SendMessage ("DisableMovement");
			CalculateTurn ();

		}

	}

	public void CalculateTurn ()
	{

		currentInitiatives.Sort ();

		//The pair of the unit to act
		UnitInitiativeP curPair = currentInitiatives.First ();
		//Get the gameObject based on the sorted list which is the game object of the unit in the pair
		currentUnit = curPair.u.gameObject;


		//Get the unchanged initiative value

		currentInitiatives.Remove (curPair);  //Remove the value


		int baseInitiative = baseInitiativeTable [currentUnit];

		int newInitiative = curPair.initiative + baseInitiative;  

		curPair.initiative = newInitiative;  //Add initiative value to current unit

		currentInitiatives.Add (curPair);
		currentInitiatives.Sort ();
		MakeTurn ();



	}

	void DebugTurn ()
	{


	}

	void MakeTurn ()
	{
		currentUnit.SendMessage ("StartTurn");

		cam.SendMessage ("SetCameraFollow", currentUnit);
	}
}