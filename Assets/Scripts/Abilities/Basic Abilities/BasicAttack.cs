using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class BasicAttack : AttackTemplate {
	public static GameObject origin;
	private const int NUM_DIMENSIONS = 1, LENGTH = 1;
	private List<int> dimensions;
	public Vector3 pos;
	private Transform startTransform;
	private List<KeyCode> theCombo;
	private RaycastHit2D[] allTargets;
    private int[] damageSteps;
    private HeroUnit myHero;
    public int attackDamageScaler;
    public List<GameObject> toRet;
    private GameObject thePlayer;

    void Start(){
        
    }


    public override void CheckLine(){     
		if (startTransform == null) {
			Debug.Log ("The start transform when basicattack.checkline() line is null");
		}

        toRet.Clear();
       // toRet.Clear();
		//Debug.DrawRay ((Vector2)startTransform.position, (Vector2)startTransform.up ,Color.green,5f);	
        allTargets = Physics2D.RaycastAll ((Vector2)startTransform.position, (Vector2)startTransform.up,1f); ///3f is hard coded for testing!!!!!!!!!!!!!

        if(allTargets.Length == 0)
        {
            Debug.Log("No Targets in range");
        }

       // Debug.Log("Swing at " + allTargets.ToString());
        foreach (RaycastHit2D hit in allTargets) {
           // Debug.Log("There was something in front of me ("+startTransform.gameObject.name+") named: " + hit.collider.gameObject.name);
			if (hit.collider.tag=="Baddy") {
				//Debug.Log ("The something was a bad guy infront of me to hit");
				toRet.Add (hit.collider.gameObject);
			}
		}
        Invoke("setTarget", 0.5f);
    }

    private void setTarget()
    {
        thePlayer.GetComponent<PlayerAttackController>().setAttackTargets(toRet);
    }


    public override void informOfParent(GameObject playerIn)
    {
       // Debug.Log("I am basic attack and I think I am attached to : "+ playerIn.name);
        thePlayer = playerIn;
        myHero = thePlayer.GetComponent<HeroUnit>();
        startTransform = thePlayer.GetComponent<Transform>();
        damageSteps = new int[3];
        toRet = new List<GameObject>();
        // myHero = (HeroUnit)gameObject.GetComponentInParent(typeof (HeroUnit));
        //Debug.Log("111Basic attack thinks it is attached to unti: " +myHero.gameObject.name);
        attackDamageScaler = 2;

        hasASpellAnimation = false;
    }
    //Ray Shape Type

    public BasicAttack(){
		//Debug.Log ("Creating atk");
	}

    public override string getMyName()
    {
        return "BasicAttack";
    }

    public override int[] GetDamageSteps()
    {
         Debug.Log("Basic attack script thinks it is attached to " + myHero.gameObject.name);
         damageSteps[0]= myHero.getattack() + myHero.weap.getDamage();
         damageSteps[1] = myHero.getattack() + myHero.weap.getDamage() + attackDamageScaler;
         damageSteps[2] = myHero.getattack() + myHero.weap.getDamage() + attackDamageScaler*2;
         return damageSteps;
    }

    public override List<KeyCode> GetComboInputSequence()
    {
        theCombo = new List<KeyCode>();
        theCombo.Add(KeyCode.Alpha2);
        theCombo.Add(KeyCode.Alpha2);
        theCombo.Add(KeyCode.Alpha3);
        Debug.Log("Basic attack generated a combo list for "+ this.gameObject.name);
        return theCombo;
    }




}
