using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Whirlwind : AttackTemplate
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
    private bool whirlTarLock;
    private int sizeOfTargetArray;
    private int currentSelectedIndex;
    List<GameObject> toRet;
    private GameObject cam;
    private GameObject thePlayer;
    public GameObject theMaestro;
    

    // Use this for initialization
    void Awake()
    {
        theMaestro = GameObject.FindGameObjectWithTag("Maestro");
        whirlTarLock = true;
        hasASpellAnimation = false;
        isARangerAbility = false;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
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
    void Update()
    {
        if (!whirlTarLock)
        {
            if (Input.GetKeyDown("space"))
            {
                foreach (GameObject tar in toRet)
                {
                    tar.SendMessage("Untargetted");
                }
                whirlTarLock = true;
                thePlayer.GetComponent<PlayerAttackController>().setAttackTargets(toRet);
            }
        }
    }

    public override void CheckLine()
    {
        theMaestro.SendMessage("TargetPing");
        enemiesInRange = null;
        enemiesInRange = GameObject.FindGameObjectsWithTag("Baddy");
        toRet.Clear();
        sizeOfTargetArray = enemiesInRange.Length;
        bool thereWasATarget = false;
        {
            RaycastHit2D rayHit;
            foreach (GameObject tar in enemiesInRange)
            {
                Debug.DrawRay((Vector2)myHero.transform.position, (Vector2)tar.GetComponent<Transform>().position - (Vector2)myHero.transform.position, Color.cyan, 20f);
                rayHit = Physics2D.Raycast((Vector2)myHero.transform.position, (Vector2)tar.GetComponent<Transform>().position - (Vector2)myHero.transform.position);
                if (rayHit.collider != null) //alive enemies 1 tile away
                    Debug.Log("Distance to ww tar: " +rayHit.collider.name +  " this far away: "+ rayHit.distance);
                    var distance =Vector3.Distance(myHero.transform.position, tar.transform.position);
                Debug.Log("Maybe bettwer distance is:  "+distance);
                {
                    if (distance <= 1.9f)
                    {
                        
                        tar.SendMessage("BeingTargetted");
                        toRet.Add(tar);
                        thereWasATarget = true;
                    }
                }
                   
            }

            if (!thereWasATarget)
            {
                thePlayer.GetComponent<PlayerAttackController>().setAttackTargets(null);
                Debug.Log("Attack whiffed");
            }
            whirlTarLock = false;
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
        theCombo.Add(KeyCode.Alpha2);
        theCombo.Add(KeyCode.Alpha2);
        theCombo.Add(KeyCode.Alpha2);
        //  Debug.Log("Basic attack made a list");
        return theCombo;
    }

    public override string getMyName()
    {
        return "Basic Whirlwind Attack";
    }

    public void clearTarget()
    {
        toRet.Clear();
    }

}
