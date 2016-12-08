using UnityEngine;
using System.Collections;

public interface IMorale  {
	EnemyMoraleUnit myMoraleStats{ get; set;}
	void Flee();
	int MoraleCheck();

	/*
	 * 
	 *    protected bool MakeMoraleCheck()
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
	 * 
	 * 
	 */



}
