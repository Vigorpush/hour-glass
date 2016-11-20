using UnityEngine;
using System.Collections.Generic;
using C5;

public class RoomBuilderAI {
	//VARIABLES PUBLIC FOR TESTING
	MonsterMod mod;

	CreditTable table;
	MonsterDictionary dict;
	int totalRoomsCleared =0;
	float roomFactor = 5f;
	public float difficultyFactor = 1f;  // will be constant
	int dungeonDepth;
	float depthFactor = 40f;
	TileGrid currentRoom;
	float BASE_CREDITS = 100f;
	float MAX_CREDITS;
	float curCredits;
	int NUM_ENCOUNTER_TYPES = 7;
	encounterType currentEncounterType;
	ArrayList<EnemyUnit> encounterParty;

	enum encounterType{
		GOBLIN = 1,
		UNDEADS = 2,
		ANIMALS = 3,
		BOSS_SOLO=4,
		BOSS_BUDDIES = 5,
		RANDOM = 6,
		CHEAP = 7
	}
	public RoomBuilderAI(int startlevel,TileGrid room){
		currentRoom = room;
		dungeonDepth = startlevel;
		MonsterDictionary dict = new MonsterDictionary ();
		CreditTable = new CreditTable ();
	}
	void Start () {



	}

	void replaceRoom(TileGrid newRoom){
		currentRoom = newRoom;

	}
	void spendExtraCreditsOnMods(){
		int selection = Random.Range (0, encounterParty.Count);
		//pick random enemy
		EnemyUnit subject = encounterParty[selection];
		//remove all current mods on the enemy to the validList, remove all unaffordable mods
		//roll a random mod m
		//check if m is on the blackList;


	}
	float calculateBudget(){
		float depthCredits = dungeonDepth * depthFactor;
		float roomsCredits = totalRoomsCleared * roomFactor;
		return BASE_CREDITS + roomsCredits + depthCredits;
	}
	void spendCredits(){
		currentEncounterType = rollEncounterType ();
		setUnitPool ();

		//while (curCredits > 0f) { while can afford cheapest unit in pool
			
			purchaseUnit ();
			//RandomRoll if to apply or not
			//Check if can afford the modifier

		//}
		//spend extra credits on modifiers 

	}
	//Purchases a unit from the given list
	void purchaseUnit(EnemyUnit chosen){
		
		curCredits= curCredits - CreditTable
	}

	void applyModifier(EnemyUnit luckyUnit){
		MonsterMod chosen = UnityEngine.Random.Range(0,MonsterMod.NUM_MODS);
		luckyUnit.AddMobModifier(Monster
	//}
	//Creates A list of potential enemies sorted by credit cost
	}
	void setUnitPool(){
		

	}
	int rollEncounterType(){
		return UnityEngine.Random.Range (1, NUM_ENCOUNTER_TYPES);

	}
	/* give it a map to put units on
	TileGrid provideFinishedRoom(){


	}
*/
	//void placeRandom(){
		
	//}	
	public void 
	// Update is called once per frame

}
