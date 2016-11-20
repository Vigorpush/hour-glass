using UnityEngine;
using System.Collections;
using C5;


public class MonsterMod : MonoBehaviour {
	//HARDCODED values for
	public float HEARTY_MOD = 1.2f;
	/*
	 * public float BRAVE_MOD = 1.2f;
	 * public float HEARTY_MOD = 1.2f;
	 * public float HEARTY_MOD = 1.2f;
	 * public float HEARTY_MOD = 1.2f;
	 * public float HEARTY_MOD = 1.2f;
	 * public float HEARTY_MOD = 1.2f;
	 * public float HEARTY_MOD = 1.2f;
	 * public float HEARTY_MOD = 1.2f;
	 * */
	public const int NUM_MODS = 10;
	enum MMods{
		/*
		BULKY = 1,
		B     = 2,
		B2    = 3,
		c	  = 4,
		BULKY = 5,
		B     = 6,
		B2    = 7,
		c			  
		*/


	}
	public MonsterMod(){
		setBase ();



	}
	static ArrayList<MonsterMod> generateModList(){
		ArrayList<MonsterMod> allMods  =new ArrayList<MonsterMod> ();
		//Making all mod types
		//Bulky
		MonsterMod Hearty = new MonsterMod();
		Hearty.hpMod = HEARTY_MOD;

		//Finally insert all of the created Modifiers and return the list
		allMods.InsertAll(Hearty);
		return allMods;



	}
	//Takes in enemy Unit
	public void applyMod(){

	}
	float hpMod,initMod,dmgMod,fortifyMod,moralMod;
	int moveMod;
	bool vampiric;

	void setBase(){
		hpMod = 1;		
		initMod = 1;	// Multiply init by the reciprocal this value   EX: initMod 1.5 means new init = oldInit*1/1.5
		dmgMod = 1;	
		moveMod = 0;    // add this value to movement
		fortifyMod = 1; // scaler
		moralMod = 1;   //Modifies starting moral value
		vampiric=0;     // 1 = true
	

	}
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
	
	}
}
