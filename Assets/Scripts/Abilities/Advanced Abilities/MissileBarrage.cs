using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileBarrage : AttackTemplate
{

    // protected Transform startTransform;
    protected GameObject[] allEnemies;
    protected GameObject[] enemiesInRange;
    protected GameObject theSelectedTarget;
    protected SpriteRenderer targetSprite;
    protected int[] damageSteps = new int[3];
    protected HeroUnit myHero;
    public int attackDamageScaler;
    protected List<KeyCode> theCombo;
    protected bool missileTarLock;
    protected int sizeOfTargetArray;
    protected int currentSelectedIndex;
    List<GameObject> toRet;
    protected GameObject cam;
    protected GameObject thePlayer;
    public GameObject theMaestro;

    // Use this for initialization
    void Awake()
    {
        missileTarLock = true;
        hasASpellAnimation = true;
        isARangerAbility = false;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        theMaestro = GameObject.FindGameObjectWithTag("Maestro");
    }

    void Start()
    {
        theMaestro = GameObject.FindGameObjectWithTag("Maestro");
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
        theMaestro.SendMessage("TargetPing");
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
