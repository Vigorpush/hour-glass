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

   // @override
    private void Die(){
        GameObject.FindGameObjectWithTag("Manager").GetComponent<TurnManager>().removeFromInitiativeQueue(this.gameObject);
        Debug.Log("A hero unit has died");
        Invoke("ActuallyDie",1.5f);
    }

    private void ActuallyDie()
    {
        
        this.gameObject.SetActive(false);
    }

}
