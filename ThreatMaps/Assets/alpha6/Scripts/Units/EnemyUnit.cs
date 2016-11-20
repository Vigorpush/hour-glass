using UnityEngine;
using System.Collections.Generic;
using C5;

public class EnemyUnit : Unit {
	//Moral checks start happening when bellow 50
	public float maxMorale;
	public int scaredThresh = 50;
    public float creditValue;
    public int meleeDamage;
    public string damageType;
    public bool canFailMorale;
	public int curMorale;
	public  ArrayList<MonsterMod> mods;

	public EnemyUnit(){
		
	}
	//True = passed the test
    private bool MakeMoraleCheck()
    {
		if (canFailMorale && underThreshold()) {
			//Check if it's brave enough to pass check
			float roll =Random.Range(0f,100f);
			if (roll >= curMorale) {
				return true;
			}
		} 

		return false;
    }
	private bool underThreshold(){
		return curMorale < scaredThresh;
	}
	public void AddMobModifier(MonsterMod newMod)
    {
		mods.Add (newMod);
        
    }
	public void ClearMobModifiers(){
		mods = new ArrayList<MonsterMod> ();
	}
	public bool hasModifier(MonsterMod modInQuestion){
		return mods.Contains(modInQuestion);
	}

	// Use this for initialization
	
}
