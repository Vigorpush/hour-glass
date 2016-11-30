using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class TurnManager : MonoBehaviour
{
    //The main Camera!
    GameObject cam;
    public GameObject[] units;
    public GameObject theExplorer;

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
				//Debug.Log (initiative + " was less than " + ((UnitInitiativeP)otherUnit).initiative);
				return -1;
			//other is the same
			} else if (result == 0) {
				//Debug.Log (initiative + " was equal to " + ((UnitInitiativeP)otherUnit).initiative);
				return 1;
			//other is faster
			} else {
				//Debug.Log (initiative + " was greater than " + ((UnitInitiativeP)otherUnit).initiative);
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
        addEnemiesToTable();
		currentInitiatives.Sort ();
        Debug.Log("==== Begin Encounter ====");
		Invoke("CalculateTurn",1f);
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
    private void addEnemiesToTable(){
        GetAllUnits();
        foreach(GameObject enemy in units){
            currentInitiatives.Add (new UnitInitiativeP (enemy.GetComponent<EnemyUnit> (), enemy.GetComponent<EnemyUnit> ().initiative));
		    baseInitiativeTable.Add (enemy.GetComponent<EnemyUnit> (), enemy.GetComponent<EnemyUnit> ().initiative);
        }
    }


	void GetAllUnits ()
	{
		units = GameObject.FindGameObjectsWithTag ("Baddy");

	}

	void GetAllPlayers ()
	{
		players = new List<GameObject> ();
		player1 = GameObject.FindGameObjectWithTag ("Player1");
		player2 = GameObject.FindGameObjectWithTag ("Player2");
		player3 = GameObject.FindGameObjectWithTag ("Player3");
        player1.SetActive(true);
        player2.SetActive(true);
        player3.SetActive(true);
		players.Add (player1);
		players.Add (player2);
		players.Add (player3);

	}

    int numTurns = 0;
	public void AIDisableThenCalcTurn(){
        Debug.Log("Msg");

		if (currentUnit.GetComponent<ZombieAI> () != null) {
			
			currentUnit.GetComponent<ZombieAI> ().enabled = false;
		}
		Invoke("CalculateTurn",.5f);
	}
	public void CalculateTurn ()
	{
        //TODO needs to tell current player to end turn, and next player to make turn.  Curren't doesn't end turn for active player when button pressed
		currentInitiatives.Sort ();
		UnitInitiativeP curPair = currentInitiatives [0];
		currentUnit = curPair.u;
		currentInitiatives.RemoveAt(0);
		curPair.updateInit (currentUnit.initiative);
		currentInitiatives.Add (new UnitInitiativeP(curPair.u,curPair.initiative));
		currentInitiatives.Sort ();
		currentUnit = curPair.u;

        numTurns++;

        if (currentUnit != null )
        {
            if(!curPair.u.getDying()){            
                //The unit died, do next turn;
               // Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Current unit is " + curPair.u.name);
                MakeTurn();
			
            }
            else
            {
                Debug.Log("Turn " + numTurns + "  will begin for " + curPair.u.gameObject.name);
                CalculateTurn();
            }
        }
       
    }

    public void removeFromInitiativeQueue(GameObject unit){

        foreach(UnitInitiativeP curPair in currentInitiatives){
            if (curPair.u == unit.GetComponent<Unit>())
            {
                currentInitiatives.Remove(curPair);
                //Debug.Log("Removing this unit: " + curPair.u.name);
                break;
            }
        }
        Invoke("CheckCombatOver",3f);
    }
		
	
	void MakeTurn ()
	{	
		if (currentUnit.tag.Equals ("Baddy")) {
			Debug.Log ("Current unit is a baddy");
			currentUnit.GetComponent<ZombieAI> ().enabled = true;

		} else {
			currentUnit.SendMessage ("StartTurn");

		}
		cam.SendMessage ("SetCameraFollow", currentUnit.gameObject);

	}

    //If all enemies are dead, award XP, check for level up, and enter exploration mode
   
   public void CheckCombatOver()
   {
        bool atleastOneEnemyAlive=false;
        foreach(UnitInitiativeP curPair in currentInitiatives)
        {
            if (curPair.u.tag.Equals("Baddy"))
            {
                atleastOneEnemyAlive = true;
            }
        }
        if (!atleastOneEnemyAlive)
        {
            Debug.Log("==== End of Encounter ====");
            enterExplorationMode();

        }

   }

    //Need to decide on one player as main, other 2 to collapse
    private void enterExplorationMode()
    {
        //this probably breaks if a player is dead
        bool firstPlayerInList = true;
        foreach (GameObject player in players)
        {
            Debug.Log("Selecting "+player.gameObject.name + " to think about exploring");
            if (firstPlayerInList)
            {
                //player.GetComponent<PlayerMovement>().EnterExplorationMode();
                theExplorer = player;
                firstPlayerInList = false;
            }
            else
                player.GetComponent<PlayerMovement>().CollapseForExploration();
        }
        theExplorer.GetComponent<PlayerMovement>().EnterExplorationMode();
        cam.SendMessage("SetCameraFollow", theExplorer);
    }

    //Fan out players along the Y axis
    public void Begin()
    { 
        GetAllPlayers();
        Invoke("initializeTables",0.5f);
        Transform explorerTf = theExplorer.transform;
        float xToSpawn = explorerTf.transform.position.x;
        float yToSpawn = explorerTf.transform.position.y;

        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerMovement>().EnterCombat(xToSpawn,yToSpawn);
            player.GetComponent<PlayerMovement>().SetStartingPosition(new Vector2(xToSpawn, yToSpawn));
            yToSpawn++;
        }

        //Spawn some zombies
        GameObject mobSpawner = GameObject.FindGameObjectWithTag("Spawner");
        mobSpawner.GetComponent<EnemyUnitFactory>().PlaceSomeZombies();

        GameObject theMaestro = GameObject.FindGameObjectWithTag("Maestro");
        theMaestro.SendMessage("PlayBattle1");
    }

}