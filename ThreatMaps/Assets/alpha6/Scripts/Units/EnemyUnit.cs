using UnityEngine;
using System.Collections;

public class EnemyUnit : Unit {

    public float creditValue;
    public int meleeDamage;
    public string damageType;
    public bool canFailMorate;
    public string mobModifier;

    private void MakeMoraleCheck()
    {

    }

    public void SetMobModifier(string modifierType)
    {
        switch (modifierType){
            //base types
            case "Hearty": break;
            case "Clever": break;
            case "Fast": break;
            case "Vicious": break;
            case "Cautious": break;
            case "Brave": break;
            //compound types
            case "Bulky": break;
            case "Raging": break;
            case "Wizardly": break;
            case "Leader": break;
            case "Uber": break;
        }
    }

	// Use this for initialization
	
}
