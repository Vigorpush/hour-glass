using UnityEngine;
using System.Collections;

public class HeroUnit :  Unit  {

    //remove the start method to avoid override stats
	
	void Update () {


        //Start of Debug for damaging
       
        //End of Debug for damaging
	
    }

	void StartTurn(){
		SendMessage ("AllowMovement");
		Debug.Log( "I, " + this.gameObject.name + " Started turn.");

	}
}
