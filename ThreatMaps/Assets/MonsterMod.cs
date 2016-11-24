using UnityEngine;
using System.Collections;



public class MonsterMod : MonoBehaviour {
	//HARDCODED values for
	static public float HEARTY_MOD = 1.2f;

	 static public float CLEVER_MOD = 1.2f;
	static public int FAST_MOD = 1;
	static public float VICIOUS_MOD = 1.1f;
	static public float CAUCIOUS_MOD = 2f;
	static public float BRAVE_MOD = 1.2f;
	static public Vector2 BULKY_MOD = new Vector2 (HEARTY_MOD, VICIOUS_MOD);
 
	public Vector3  RAGING_MOD = new Vector3(FAST_MOD,VICIOUS_MOD*2f,-HEARTY_MOD+1);
	public Vector3 TERRIFYING_MOD = new Vector3(CLEVER_MOD,FAST_MOD,VICIOUS_MOD);
	public bool VAMPIRIC;

	public const int NUM_MODS = 10;
	public enum MMods{
		
		BULKY = 1,
		RAGING = 2,
		TERRIFYING    = 3,
		BRAVE	  = 4,
		CLEVER = 5,
		CAUCIOUS     = 6,
		HEARTY    = 7,
		FAST =8,
		VICIOUS = 9,
		VAMPIRIC = 10




	}
	public MonsterMod(){
		setBase ();



	}
	public  ArrayList generateModList(){
		ArrayList allMods  =new ArrayList ();
		//Making all mod types
		//Bulky
		MonsterMod hearty = new MonsterMod();
		hearty.hpMod = HEARTY_MOD;
		MonsterMod bulky = new MonsterMod ();
		bulky.hpMod = HEARTY_MOD;
		bulky.dmgMod = VICIOUS_MOD;
		MonsterMod clever = new MonsterMod ();
		clever.initMod = CLEVER_MOD;
		MonsterMod fast = new MonsterMod ();
		fast.moveMod = (int)FAST_MOD;
		MonsterMod vicious = new MonsterMod ();
		vicious.dmgMod = VICIOUS_MOD;
		MonsterMod caucious = new MonsterMod ();
		caucious.fortifyMod = CAUCIOUS_MOD;
		MonsterMod raging = new MonsterMod();
		raging.moveMod = (int)RAGING_MOD.x;
		raging.dmgMod = RAGING_MOD.y;
		raging.hpMod = RAGING_MOD.z;
		MonsterMod terrifying = new MonsterMod ();
		terrifying.initMod = TERRIFYING_MOD.x;
		terrifying.moveMod = (int)TERRIFYING_MOD.y;
		terrifying.dmgMod = TERRIFYING_MOD.z;
		MonsterMod vampiric = new MonsterMod ();
		vampiric.vampiric = true;
		MonsterMod brave = new MonsterMod ();
		brave.moraleMod = BRAVE_MOD;




		//allMods.in
		//Finally insert all of the created Modifiers and return the list

		allMods.Add(bulky);
		allMods.Add(raging);
		allMods.Add(terrifying);
		allMods.Add(brave);
		allMods.Add(clever);
		allMods.Add(caucious);
		allMods.Add(hearty);
		allMods.Add(fast);
		allMods.Add(vicious);
		allMods.Add(vampiric);

		return allMods;



	}

	public float hpMod,initMod,dmgMod,fortifyMod,moraleMod;
	public int moveMod;
	public bool vampiric;

	void setBase(){
		hpMod = 1;		
		initMod = 1;	// Multiply init by the reciprocal this value   EX: initMod 1.5 means new init = oldInit*1/1.5
		dmgMod = 1;	
		moveMod = 0;    // add this value to movement
		fortifyMod = 1; // scaler
		moraleMod = 1;   //Modifies starting moral value
		vampiric=false;     // 1 = true


	}
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
	
	}
}
