using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class EnemyUnit : Unit {
	//Moral checks start happening when bellow 50
	public  ArrayList mods;
	public float creditValue;
    public AudioSource deathCry;

	public EnemyUnit(){
		
	}
 
	public void AddMobModifier(MonsterMod newMod)
    {
		mods.Add (newMod);
        
    }
	public void ClearMobModifiers(){
		mods = new ArrayList ();
	}
	public bool hasModifier(MonsterMod modInQuestion){
		return mods.Contains(modInQuestion);
	}

    public void playDeathCry()
    {
        deathCry = this.GetComponent<AudioSource>();
        deathCry.Play();
    }

	// Use this for initialization
	
}
