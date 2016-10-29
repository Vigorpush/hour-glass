using UnityEngine;
using System.Collections;

public class HeroUnit :  Unit, IButtonMap  {

    //remove the start method to avoid override stats
	/*Todo  list of  skill points / maybe seperate passives
	 * mapping of active abilities public to allow drag drop prefabs
	 * 
	 * 
	 */
	void Update () {


        //Start of Debug for damaging
       
        //End of Debug for damaging
	
    }

	void StartTurn(){
		
		SendMessage ("AllowMovement");
        SendMessage("AllowAttack");
		Debug.Log( "I, " + this.gameObject.name + " Started turn.");

	}
}
