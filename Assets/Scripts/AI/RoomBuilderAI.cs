using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RoomBuilderAI : MonoBehaviour {
	//VARIABLES PUBLIC FOR TESTING
	MonsterMod mod;
	MonsterDictionary d;
	public CreditTable cTable;
	//Max number of enemies that will be alloted to an encounter party 
	float MAX_MONSTERS_FACTOR;
	int totalRoomsCleared =0;
	float roomFactor = 5f;
	public float difficultyFactor = 1f;  // will be constant
	int dungeonDepth;
	float depthFactor = 40f;
	float BASE_CREDITS = 100f;
	float MAX_CREDITS=float.MaxValue;
	float curCredits;
	float BASE_MODIFIER_COST = 5f;
	int NUM_ENCOUNTER_TYPES = 8;
	encounterType currentEncounterType;
	ArrayList unitPool;
	ArrayList bossPool;
	ArrayList encounterParty;
	float MOD_ROLL_CHANCE = 0.2f;
	int roomSize;
	//TODO: add roll chances based on room dimensions  
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
	void Awake(){
		d = GameObject.FindGameObjectWithTag ("Dict").GetComponent<MonsterDictionary>();
	}
	void Start(){
		//Testing Encounter Rolls
		cTable =  (CreditTable) this.gameObject.GetComponent<CreditTable>();

		//Debug.Log("starting");


		unitPool = new ArrayList();
		bossPool = new ArrayList ();

		rollEncounterType();
		setFloor (1);

		//Testing getting enemyPool



	}

	public void setFloor(int dungeonLevel){
		dungeonDepth = dungeonLevel;
	}
	void buildRoom(TileGrid newRoom){
	
		setUnitPool ();
		calculateBudget ();
		spendCredits ();

	}
	void spendExtraCreditsOnMods(){
		while(curCredits>=cTable.cheapestPrice()){
		int selection = Random.Range (0, encounterParty.Count);
		//pick random enemy
			EnemyUnit subject =(EnemyUnit) encounterParty[selection];
		//ArrayList<EnemyUnit> blackList = subject.
		//remove all current mods on the enemy to the validList, remove all unaffordable mods
		//roll a random mod m
		//check if m is on the blackList;
		}
	
	}

	bool modRoll(){
		return MOD_ROLL_CHANCE > Random.Range (0f, 1f);



	}
	float calculateBudget(){
		float depthCredits = dungeonDepth * depthFactor;
		float roomsCredits = totalRoomsCleared * roomFactor;
		return BASE_CREDITS + roomsCredits + depthCredits;
	}
	public void nextBattle(int roomSz,bool isNewFloor){
		if (isNewFloor) {
			dungeonDepth++;
		}
		totalRoomsCleared++;
		roomSize = roomSz;
		curCredits = calculateBudget ();
	}
	void spendCredits(){
		
		int numMonsters = 0;
		bool monsterReadyForBuff = false;
		//If boss type encounter
		if (currentEncounterType == encounterType.BOSS_SOLO || currentEncounterType == encounterType.BOSS_BUDDIES) {
			//Buy the boss
			purchaseUnit ((EnemyUnit)bossPool[0]);
			Debug.Log ("Set " + ((EnemyUnit)bossPool[0]).name + " as the room boss."); 
		}
		while (curCredits >= cTable.cheapestPrice()) { //while can afford cheapest unit in pool
			 //Randomly Select A Unit
			EnemyUnit curUnit = randomPick(encounterParty);
			if (canPurchaseUnit (curUnit)) {
				monsterReadyForBuff = false;
				purchaseUnit (curUnit);

				numMonsters++;

			}
			//CREATE BLACK LIST if needed

			while(canAffordModifier() &&  monsterReadyForBuff){
				if (modRoll ()) {
					applyModifier (curUnit);
					Debug.Log("Buffed");

				} else {
					
					monsterReadyForBuff = false;
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
		if (canPurchaseUnit(chosen)) {
			curCredits = curCredits - cTable.lookUp (chosen.name);
			Debug.Log ("Choose " + chosen.name + " as unit to buy.");
		}
		Debug.Log ("Can't afford unit.");
	}
	private bool canPurchaseUnit(EnemyUnit chosen){

		return curCredits > cTable.lookUp (chosen.name);
	}
	/* Unit picker algorithms */
	//Doesn't check if can afford
	EnemyUnit randomPick(ArrayList options){

		return (EnemyUnit)options [Random.Range (0, unitPool.Count)];
	}

	public bool canPurchaseAnyUnit(){

		return curCredits > cTable.cheapestPrice();

	}
	//Checks if can afford for efficiency sake if had to re-run
	EnemyUnit greedyValuePick(ArrayList options){
		EnemyUnit bestValue = new EnemyUnit();
		float best = -1;
		EnemyUnit curUnit;
		for (int i = 0; i < options.Count; i++) {
			curUnit = (EnemyUnit)options [i];
			//If there is a higher value selection and can afford it
			if (getUnitCreditEfficiency(curUnit) > best   && canPurchaseUnit(curUnit)) {
				bestValue = curUnit;

			}

		}

		Debug.Log("The best buy is" + bestValue.name+ " with a score of "  +best + " times regular value");
		return bestValue;
	}

	float getUnitCreditEfficiency(EnemyUnit unit){
		return cTable.lookUp (unit.name) / (unit.creditValue);

	}
	//script name is enemyUnitFactory
	//Should buy more strong units on average but won't ignore worse deals entirely
	EnemyUnit testOfTimePick(ArrayList options){
		int numberOfTrails = 5; // FUDGE
		float[] sumOfValues = new float [options.Count] ;
		//Initialize the value table
		for(int i=0;i<options.Count;i++){
			sumOfValues [i] = 0f;
		}
		for (int i = 0; i < numberOfTrails; i++) {
			for (int j = 0; j < options.Count; j++) {
				int roll = Random.Range (0, unitPool.Count);
				sumOfValues[roll]+= getUnitCreditEfficiency((EnemyUnit)options[roll]); //Randomly pick a unit that can be purchased
			}

		}
		EnemyUnit winner = new EnemyUnit();
		float highestScore = 0f;
		for(int i=0;i<options.Count;i++){
			if (sumOfValues [i] > highestScore) {
				highestScore = sumOfValues [i];
				winner = (EnemyUnit)options [i];
			}
		}
		Debug.Log (sumOfValues.ToString());

		Debug.Log ("Winner is " + winner.name + " with a score of " + highestScore);
		return winner;




	}

	/*TODO:
	 * Based on the monster's performance it is judged to decide how much its credit table entry should be altered */
	//Functions based off of an expected ratio
	public void judgeDPS(EnemyUnit judged,int damageOutput,float expectedDPC){
		Debug.Log ("Judging the dps performance of" + judged.name + ".");
		float maxChangeFactor = .15f;

		float curCost = cTable.lookUp(judged.name);
		float maxChange = curCost * .15f;

		float damagePerCredit = damageOutput/curCost;
		float extra =  (damagePerCredit - expectedDPC);
		float increaseAmt=extra * maxChangeFactor * curCost;
		//Unit achieved or exceeeded value
		if (extra >= 0) {
			if (increaseAmt > maxChange) {
				//cTable.raiseCost (judged, (damagePerCredit / expectedDPC));
				cTable.raiseCost (judged, maxChange);
			} else {
				cTable.raiseCost (judged, increaseAmt);
			}
		} else {
			//If no value was experienced lower by max 
			if (extra >= -expectedDPC) {
				cTable.cheapen (judged, maxChange);

			} else {
				cTable.cheapen (judged, increaseAmt);
			}

		}

	}
	public float judgeHeal(EnemyUnit judged,int healingOutput,float expectedHPC){
		Debug.Log ("Judging the dps performance of" + judged.name + ".");
		float maxChangeFactor = .15f;

		float curCost = cTable.lookUp(judged.name);
		float maxChange = curCost * maxChangeFactor;

		float healingPerCredit = healingOutput/curCost;
		float extra =  (healingPerCredit - expectedHPC);
		float increaseAmt=extra * maxChangeFactor * curCost;
		//Unit achieved or exceeeded value
		if (extra>=0){
			if (increaseAmt > maxChange) {
				cTable.raiseCost (judged, maxChange);
			} else {
				cTable.raiseCost (judged, increaseAmt);
			}
		} else {
			if (extra >= expectedHPC) {
				cTable.raiseCost (judged, maxChange);
			}
		}
		return healingPerCredit / expectedHPC;

	}

	

	bool canAffordModifier(){
		return BASE_CREDITS >= BASE_MODIFIER_COST;
	}
	void applyModifier(EnemyUnit luckyUnit){
		bool notValidMod = true;
		while(notValidMod){
			MonsterMod chosen = (MonsterMod)mod.generateModList()[UnityEngine.Random.Range(0,MonsterMod.NUM_MODS)];
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
			encounterParty = d.creatures;
			break;
		case encounterType.BOSS_BUDDIES:
			bossPool = d.bosses;
			encounterParty = d.allEnemies;
			break;
		case encounterType.BOSS_SOLO:
			bossPool = d.bosses;
			break;
		case encounterType.CHEAP:
			encounterParty = d.allEnemies;
			break;
		case encounterType.GOBLIN:
			encounterParty = d.goblins;
			break;
		case encounterType.PREFER_MODS:
			encounterParty = d.allEnemies;
			break;
		case encounterType.RANDOM:
			encounterParty = d.allEnemies;
			break;
		case encounterType.UNDEADS:
			encounterParty = d.undeads;
			break;




		}


	}
	private void rollEncounterType(){
		

		//currentEncounterType = (encounterType)UnityEngine.Random.Range (1, NUM_ENCOUNTER_TYPES);
		currentEncounterType = encounterType.RANDOM;
		//Debug.Log("Rolled " + currentEncounterType.ToString());
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