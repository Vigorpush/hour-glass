using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {

	private List<GameObject> players;
	SortedDictionary<int,GameObject> currentInitiativeTable;
	//Contains all original initiatives to get via object 
	Dictionary<GameObject,int> baseInitiativeTable;
	GameObject player1;
	GameObject player2;
	GameObject player;


	GameObject currentUnit;

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
		GetAllPlayers ();		
		initializeTables ();
	}

	void GetAllUnits(){
		GameObject[] units =  GameObject.FindGameObjectsWithTag("Enemy");

	}

	void GetAllPlayers(){
		player1 =  GameObject.FindGameObjectWithTag("Player");
		players.Add(player1);
		//Find and add players...

	}
	// Update is called once per frame
	void Update () {
	
	}

    void CalculateTurn()
    {
		//Get next unit at top of queue
		IEnumerable<KeyValuePair<int,GameObject>> dictionaryValue = currentInitiativeTable.Take(1);
		//Add initiative value to current unit
		//Only one in list so first gets the next unit
		GameObject UnitEntity = dictionaryValue.ElementAt(0).Value;

		//Amount to be added for new initiative value
		int baseInitiative = baseInitiativeTable[UnitEntity];
		int newInitiative = UnitEntity.GetComponent<Unit>().initiative + baseInitiative;
				


		//currentInitiativeTable.Add(,dictionaryValue.First);

    }

    void MakeTurn()
    {
		currentUnit.SendMessage ("StartTurn");
       // player1.SendMessage("StartTurn");
    }
}
