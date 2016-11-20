using UnityEngine;
using System.Collections;
using C5;
using System.Collections.Generic;

public class MonsterDictionary  {
	
	public static ArrayList<EnemyUnit> allEnemies;
	public static ArrayList<EnemyUnit> goblins;
	public static ArrayList<EnemyUnit> bosses;
	public static ArrayList<EnemyUnit> creatures;
	public static ArrayList<MonsterMod> monsterMods;
	// Use this for initialization
	public MonsterDictionary(){
		populateCreatures();
		populateBossess ();
		populateGoblins ();
	}

	void populateGoblins(){
		EnemyUnit goblin = new EnemyUnit (); 
		goblin.creditValue = 25;
		goblin.name = "Goblin";
		EnemyUnit goblinArcher = new EnemyUnit (); 
		goblinArcher.creditValue = 35;
		goblinArcher.name = "Goblin_Archer";
		EnemyUnit goblinShaman = new EnemyUnit (); 
		goblinShaman.creditValue = 40;
		goblinShaman.name = "Goblin Shaman";
		EnemyUnit goblinWarlord = new EnemyUnit (); 

		goblinWarlord.creditValue = 50;
		goblinWarlord.name = "Goblin Warlord";

	}
	void populateBossess(){
		EnemyUnit boss = new EnemyUnit ();
		boss.creditValue = 100;
	}
	void populateCreatures (){

	}




	// Update is called once per frame
	void Update () {
		
	}
}
