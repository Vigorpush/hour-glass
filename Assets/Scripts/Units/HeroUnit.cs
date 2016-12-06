using UnityEngine;
using System.Collections;

public class HeroUnit :  Unit, IButtonMap  {

    private TurnManager turnScript;
    /**
     * The players experience
     */
    public float myExperience;

    void Start()
    {
        anim = GetComponent<Animator>();
        theTurnManager = GameObject.FindGameObjectWithTag("Manager");
        turnScript = theTurnManager.GetComponent<TurnManager>();
    }

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

    
    public override void takeDamage(int attackAmount, GameObject damageDealer)
    {
        anim.SetTrigger("GotHit");
        unitThatHitMeThisTurn = damageDealer;
        int result = CalculateDamageReduction(attackAmount);
        curhp -= result;
        GetComponent<SpawnTextBubble>().gotHit(result);
        AnnounceDamage(result, damageDealer);
        InitCBT(result.ToString());
        logDamageToBattleMaster(result,damageDealer);
        if (curhp <= 0)
        {
            Die();
        }
    }

    public override void takeCriticalDamage(int critAmount, GameObject damageDealer)
    {
        anim.SetTrigger("GotHit");
        unitThatHitMeThisTurn = damageDealer;
        int result = CalculateDamageReduction(critAmount);
        curhp -= result;
        GetComponent<SpawnTextBubble>().gotHit(result);
        AnnounceDamage(result, damageDealer);
        InitCBT(result.ToString());
        logDamageToBattleMaster(result, damageDealer);
        if (curhp <= 0)
        {
            Die();
        }
    }

    public void logDamageToBattleMaster(int damageTaken, GameObject damageDealer)
    {
        turnScript.logDamage(this.gameObject, damageTaken, damageDealer);
    }


    // @override
    private void Die(){
        turnScript.removeFromInitiativeQueue(this.gameObject);
        Debug.Log("A hero unit has died");
        Invoke("ActuallyDie",1.5f);
    }

    private void ActuallyDie()
    {  
        this.gameObject.SetActive(false);
    }

}
