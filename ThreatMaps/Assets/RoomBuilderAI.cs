using UnityEngine;
using System.Collections;

public class RoomBuilderAI {
	
	//VARIABLES PUBLIC FOR TESTING
	MonsterDictionary dict;
	int totalRoomsCleared =0;
	float roomFactor;
	public float difficultyFactor = 1f;  // will be constant
	int dungeonDepth;
	float depthFactor = 25f;
	TileGrid currentRoom;
	float BASE_CREDITS = 100f;
	float MAX_CREDITS;
	float curCredits;
	encounterType currentEncounterType;
	enum encounterType{
		GOBLIN = 1,
		UNDEADS = 2,
		ANIMALS = 3,
		BOSS_SOLO=4,
		BOSS_BUDDIES = 5,
		RANDOM = 6
	}
	public RoomBuilderAI(int startlevel,TileGrid room){
		currentRoom = room;
		dungeonDepth = startlevel;
	}
	void Start () {
		dict = new MonsterDictionary ();

	}

	void replaceRoom(TileGrid newRoom){


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
	void purchaseUnit(){
		curCredits= curCredits -
	}

	//void applyModifier(EnemyUnit){
	//	MonsterMod chosen = UnityEngine.Random.Range(0,MonsterMod.NUM_MODS);
		//EnemyUnit.hp
	//}
	//Creates A list of potential enemies sorted by credit cost
	void setUnitPool(){
		for

	}
	int rollEncounterType(){
		return UnityEngine.Random.Range (1, 5);

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
