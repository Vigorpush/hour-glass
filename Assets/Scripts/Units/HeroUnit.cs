using UnityEngine;
using System.Collections;

public class HeroUnit :  Unit, IButtonMap  {

    //remove the start method to avoid override stats
	/*Todo  list of  skill points / maybe seperate passives
	 * mapping of active abilities public to allow drag drop prefabs
	 * 
	 * 
	 */

    /**
     * The players experience
     */
    public float myExperience;

    public void lvlUp(){

    }
    public void attackLvlUp(){

    }
    public void defenseLvlUp()
    {

    }
    public void speedLvlUp()
    {

    }

    public void addExperience(int ExperienceToAdd)
    {
        myExperience += ExperienceToAdd;
    }
    
}
