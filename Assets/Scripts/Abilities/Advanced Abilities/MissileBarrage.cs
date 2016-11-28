using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileBarrage : AttackTemplate
{

    // private Transform startTransform;
    private GameObject[] allEnemies;
    private GameObject[] enemiesInRange;
    private GameObject theSelectedTarget;
    private SpriteRenderer targetSprite;
    private int[] damageSteps = new int[3];
    private HeroUnit myHero;
    public int attackDamageScaler;
    private List<KeyCode> theCombo;
    private bool missileTarLock;
    private int sizeOfTargetArray;
    private int currentSelectedIndex;
    List<GameObject> toRet;
    private GameObject cam;
    private GameObject thePlayer;

    // Use this for initialization
    void Awake()
    {
        missileTarLock = true;
        hasASpellAnimation = true;
        isARangerAbility = false;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public override void informOfParent(GameObject playerIn)
    {
        thePlayer = playerIn;
        myHero = thePlayer.gameObject.GetComponent<HeroUnit>();
        toRet = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!missileTarLock)
        {
            if (Input.GetKeyDown("space"))
            {
                foreach (GameObject tar in enemiesInRange)
                {
                    tar.SendMessage("Untargetted");
                    toRet.Add(tar);
                }
                missileTarLock = true;
                thePlayer.GetComponent<PlayerAttackController>().setAttackTargets(toRet);
            }
        }
    }

    public override void CheckLine()
    {
        enemiesInRange = null;
        enemiesInRange = GameObject.FindGameObjectsWithTag("Baddy");
        //All enemies on board
        //All eneimes in range
        toRet.Clear();
        sizeOfTargetArray = enemiesInRange.Length;
        if (sizeOfTargetArray <= 0)
        {
            thePlayer.GetComponent<PlayerAttackController>().setAttackTargets(toRet);
        }
        else
        {
            foreach (GameObject tar in enemiesInRange)
            {
                tar.SendMessage("BeingTargetted");
            }
            missileTarLock = false;
        }
    }

    public override int[] GetDamageSteps()
    {
        // Debug.Log("Basic attack script thinks it is attached to " + myHero);
        damageSteps[0] = myHero.getattack() + myHero.weap.getDamage();
        damageSteps[1] = myHero.getattack() + myHero.weap.getDamage();
        damageSteps[2] = myHero.getattack() + myHero.weap.getDamage();
        return damageSteps;
    }

    public override List<KeyCode> GetComboInputSequence()
    {
        theCombo = new List<KeyCode>();
        theCombo.Add(KeyCode.Alpha3);
        theCombo.Add(KeyCode.Alpha3);
        theCombo.Add(KeyCode.Alpha3);
        //  Debug.Log("Basic attack made a list");
        return theCombo;
    }

    public override string getMyName()
    {
        return "MissileStorm";
    }

    public void clearTarget()
    {
        toRet.Clear();
    }

}
