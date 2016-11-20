using UnityEngine;
using System.Collections;
using C5;
using System.Collections.Generic;

public class MonsterDictionary  {
	
	public ArrayList<EnemyUnit> allEnemies;
	public ArrayList<EnemyUnit> goblins;
	public ArrayList<EnemyUnit> bosses;
	public ArrayList<EnemyUnit> creatures;
	public List<MonsterMod> monsterMods;
	// Use this for initialization
	public MonsterDictionary(){
		populateCreatures();
		populateBossess ();
		populateGoblins ();
	}

	void populateGoblins(){
		EnemyUnit goblin = new EnemyUnit (); 
		goblin.creditValue = 25;
		EnemyUnit goblinArcher = new EnemyUnit (); 
		goblinArcher.creditValue = 35;
		EnemyUnit goblinShaman = new EnemyUnit (); 
		goblinShaman.creditValue = 40;
		EnemyUnit goblinWarlord = new EnemyUnit (); 
		goblinWarlord.creditValue = 50;

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
