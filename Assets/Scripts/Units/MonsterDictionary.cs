using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//ssusing C5;


public class MonsterDictionary  {
	
	public ArrayList allEnemies;
	public ArrayList goblins;
	public ArrayList bosses;
	public ArrayList creatures;
	public ArrayList undeads;
	public ArrayList monsterMods;
	// Use this for initialization
	public MonsterDictionary(){
		populateCreatures();
		populateBossess ();
		populateGoblins ();
		populateUndeads ();
	}

	void populateGoblins(){
		/*
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
		*/
	}
	void populateUndeads(){


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
