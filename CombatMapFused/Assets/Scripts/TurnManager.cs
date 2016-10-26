using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TurnManager : MonoBehaviour {

	private List<GameObject> players;
	SortedDictionary<int,GameObject> currentInitiativeTable;
	//Contains all original initiatives to get via object 
	Dictionary<GameObject,int> baseInitiativeTable;
	GameObject player1;
	GameObject player2;
	GameObject player;
	GameObject endTurnButton;


	public GameObject currentUnit;


	void setUpButton(){
		endTurnButton = GameObject.Find ("EndTurnButton");
	}
	// Use this for initialization

	void initializeTables(){
		
		baseInitiativeTable = new Dictionary<GameObject,int>();
		currentInitiativeTable = new SortedDictionary<int,GameObject>();
		//Fill the tables

		//Get all players and units by calling respective methods
		currentInitiativeTable.Add(player1.GetComponent<HeroUnit>().initiative,player1);
		baseInitiativeTable.Add(player1,player1.GetComponent<HeroUnit>().initiative);

	

		CalculateTurn();

	}
	void Start () {
		setUpButton ();
		GetAllPlayers ();		
		initializeTables ();
	}

	void GetAllUnits(){
		GameObject[] units =  GameObject.FindGameObjectsWithTag("Enemy");

	}

	void GetAllPlayers(){
		players = new List<GameObject>();
		player1 =  GameObject.FindGameObjectWithTag("Player1");
		players.Add(player1);
		//Find and add players...

	}
	// Update is called once per frame
	void Update () {
		
	}

    public void CalculateTurn()
    {
		//Get next unit at top of queue
		IEnumerable<KeyValuePair<int,GameObject>> dictionaryValue = currentInitiativeTable.Take(1);
		//Remove the old top initiative

		//Add initiative value to current unit
		//Only one in list so first gets the next unit
		GameObject UnitEntity = dictionaryValue.ElementAt(0).Value;
		int myCurrentInit = dictionaryValue.ElementAt (0).Key;
		//Remove the value
		currentInitiativeTable.Remove (currentInitiativeTable.Keys.First());
		//currentInitiativeTable.Remove (currentInitiativeTable.Keys.First());
		//currentInitiativeTable.Remove (currentInitiativeTable.Keys.First());
		//currentInitiativeTable.Remove (currentInitiativeTable.Keys.First());
		//currentInitiativeTable.Clear();
		//Amount to be added for new initiative value
		int baseInitiative = baseInitiativeTable[UnitEntity];
		Debug.Log ("Base initiative" +baseInitiative);
		Debug.Log ("Offender" + UnitEntity.GetComponent<Unit>().initiative);
		Debug.Log ("This should be null: " + currentInitiativeTable.Take(1));
		int newInitiative = myCurrentInit + baseInitiative;
		Debug.Log ("Base initiative" + newInitiative);
		currentInitiativeTable.Add(newInitiative,UnitEntity);
		MakeTurn ();

		//currentInitiativeTable.Add(,dictionaryValue.First);

    }

    void MakeTurn()
    {
		currentUnit.SendMessage ("StartTurn");
       // player1.SendMessage("StartTurn");
    }
}
