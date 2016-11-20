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
    public float canFailMorale;
	public int curMorale;
	public  ArrayList<MonsterMod> mods;

	public EnemyUnit(){
		
	}
	//True = passed the test
    private bool MakeMoraleCheck()
    {
		if (canFailMorale && curMorale <scaredThresh) {
			//Check if it's brave enough to pass check
			float roll =Random.Range(0f,100f);
			if (roll >= curMorale) {
				return true;
			}
		} else {
			return true;
		}
		return false;
    }

	public void AddMobModifier(MonsterMod newMod)
    {
		mods.Add (newMod);
        
    }
	public void ClearMobModifiers(){
		mods = new ArrayList<MonsterMod> ();
	}

	// Use this for initialization
	
}
