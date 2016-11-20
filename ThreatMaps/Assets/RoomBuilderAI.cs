using UnityEngine;
using System.Collections.Generic;
using C5;

public class RoomBuilderAI {
	//VARIABLES PUBLIC FOR TESTING
	MonsterMod mod;
	MonsterDictionary d;
	CreditTable cTable;
	int totalRoomsCleared =0;
	float roomFactor = 5f;
	public float difficultyFactor = 1f;  // will be constant
	int dungeonDepth;
	float depthFactor = 40f;
	TileGrid currentRoom;
	float BASE_CREDITS = 100f;
	float MAX_CREDITS=float.MaxValue;
	float curCredits;
	float BASE_MODIFIER_COST = 5f;
	int NUM_ENCOUNTER_TYPES = 8;
	encounterType currentEncounterType;
	ArrayList<EnemyUnit> unitPool;
	ArrayList<EnemyUnit> encounterParty;
	float MOD_ROLL_CHANCE = 0.2f;

	enum encounterType{
		GOBLIN = 1,
		UNDEADS = 2,
		ANIMALS = 3,
		BOSS_SOLO=4,
		BOSS_BUDDIES = 5,
		RANDOM = 6,
		CHEAP = 7,
		PREFER_MODS = 8
	}
	public RoomBuilderAI(int startlevel,TileGrid room){
		currentRoom = room;
		dungeonDepth = startlevel;
		MonsterDictionary dict = new MonsterDictionary ();
		cTable = new CreditTable ();
	}
	void Start () {
		rollEncounterType ();


	}

	void buildRoom(TileGrid newRoom){
		currentRoom = newRoom;
	
		setUnitPool ();
		calculateBudget ();
		spendCredits ();

	}
	void spendExtraCreditsOnMods(){
		int selection = Random.Range (0, encounterParty.Count);
		//pick random enemy
		EnemyUnit subject = encounterParty[selection];
		//remove all current mods on the enemy to the validList, remove all unaffordable mods
		//roll a random mod m
		//check if m is on the blackList;
	
	}
	bool modRoll(){
		return MOD_ROLL_CHANCE > Random.Range (0f, 1f);



	}
	float calculateBudget(){
		float depthCredits = dungeonDepth * depthFactor;
		float roomsCredits = totalRoomsCleared * roomFactor;
		return BASE_CREDITS + roomsCredits + depthCredits;
	}
	void spendCredits(){
		
		while (curCredits >= cTable.cheapestPrice()) { //while can afford cheapest unit in pool
			int attemptedPurchaseOffset = Random.Range(0,unitPool.Count); //Randomly Select A Unit
			EnemyUnit curUnit = unitPool[attemptedPurchaseOffset];
			if(canPurchaseUnit(curUnit))
				
				purchaseUnit (curUnit);
			bool buffing = true;
			//CREATE BLACK LIST

			while(canAffordModifier() && buffing){
				if (modRoll ()) {
					applyModifier (curUnit);
					Debug.Log("Buffed");
				} else {
					buffing = false;
				}
			//RandomRoll if to apply or not
			//Check if can afford the modifier

		}
		//spend extra credits on modifiers 
		spendExtraCreditsOnMods();

	}
	}
	//Purchases a unit from the given list
	void purchaseUnit(EnemyUnit chosen){
		
		curCredits = curCredits - cTable.lookUp (chosen);
	}
	bool canPurchaseUnit(EnemyUnit chosen){

		return curCredits > cTable.lookUp (chosen);
	}
	/*TODO:
	void judge(EnemyUnit){


	}
	*/
	bool canAffordModifier(){
		return BASE_CREDITS >= BASE_MODIFIER_COST;
	}
	void applyModifier(EnemyUnit luckyUnit){
		bool notValidMod = true;
		while(notValidMod){
			MonsterMod chosen = mod.generateModList()[UnityEngine.Random.Range(0,MonsterMod.NUM_MODS)];
			if(!luckyUnit.hasModifier(chosen)){
				luckyUnit.AddMobModifier(chosen);
					curCredits=curCredits-BASE_MODIFIER_COST;
			}

		}
	//}
	//Creates A list of potential enemies sorted by credit cost
	}
	void setUnitPool(){
		switch (currentEncounterType) {
		case encounterType.ANIMALS:

			break;
		case encounterType.BOSS_BUDDIES:
			break;
		case encounterType.BOSS_SOLO:

			break;
		case encounterType.CHEAP:
			break;
		case encounterType.GOBLIN:

			break;
		case encounterType.PREFER_MODS:
			break;
		case encounterType.RANDOM:

			break;
		case encounterType.UNDEADS:
			
			break;




		}


	}
	 int rollEncounterType(){
		return UnityEngine.Random.Range (1, NUM_ENCOUNTER_TYPES);

	}

	TileGrid provideFinishedRoom(){
		return currentRoom;
		//void placeRandom(){


	}
	/* Debug FUNCTIONS */
	 
	void printEncounterParty(){
		foreach( EnemyUnit ai in encounterParty){
			Debug.Log(ai.name);

		}


	}
	void printCreditTableParty(){
		foreach( EnemyUnit ai in encounterParty){
			Debug.Log(ai.name);
			Debug.Log(ai.gameObject.ToString());
		}


	}
	void printCreditStatus(){
		Debug.Log("Currently at:"+  curCredits+  "credits remaining.");
		Debug.Log (((curCredits / BASE_CREDITS) * 100) + "% left.");
	}

}