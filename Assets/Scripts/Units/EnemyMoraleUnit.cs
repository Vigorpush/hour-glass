using UnityEngine;
using System.Collections;

public class EnemyMoraleUnit : EnemyUnit {
	public enum MoraleStatus
	{
		SHUTDOWN = 0,
		UNINSPIRED = 1,
		NORMAL     =2,
		INSPIRED = 3,
		ZEALOUS = 4
	}
	//THRESHHOLDS for changing your morale status
	public float UNINSPIRED_THRESH;
	public float NORMAL_THRESH;
	public float INSPIRED_THRESH;
	public float ZEALOUS_THRESH;


	public MoraleStatus status;
	public float defaultFriendlyAoeBoost;
	public float initMoraleHP;
	public float curMoraleHP;
	public float bravery;  // scaler from 0 -> 1  , 1 means 100% morale block
	public float maxMorale;
	public float supportPresenceRange;
	public float creditValue;

	public void moraleDamage(float amt){
		curMoraleHP-= amt - amt*bravery;
		updateStatus ();
	}

	public void moraleHeal(float amt){
		curMoraleHP += amt;
		updateStatus ();
	}
	public void updateStatus(){
		if(curMoraleHP<UNINSPIRED_THRESH){
			status = MoraleStatus.SHUTDOWN;
		}
		else if(curMoraleHP<NORMAL_THRESH){
			status = MoraleStatus.UNINSPIRED;
		}
		else if(curMoraleHP<INSPIRED_THRESH){
			status = MoraleStatus.NORMAL;
		}
		else if(curMoraleHP<ZEALOUS_THRESH){
			status = MoraleStatus.INSPIRED;
		}
		else{
			status = MoraleStatus.ZEALOUS;
		}
	}

	public string damageType;
	public bool canFailMorale;
	public int curMorale;

}
