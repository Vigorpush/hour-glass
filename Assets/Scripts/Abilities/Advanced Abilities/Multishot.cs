using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Multishot : AttackTemplate
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
    private bool ArrowTargetLock;
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

//Debug.Log("arrow attack woke up");
        ArrowTargetLock = true;
        //Debug.Log(ArrowTargetLock);
        hasASpellAnimation = false;
        isARangerAbility = true;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Start(){
        theMaestro = GameObject.FindGameObjectWithTag("Maestro");

    }


    public override void informOfParent(GameObject playerIn)
    {
        // cycleTargetLock = true;
        thePlayer = playerIn;
        //startTransform = thePlayer.gameObject.GetComponent<Transform>();
        myHero = thePlayer.gameObject.GetComponent<HeroUnit>();
        toRet = new List<GameObject>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!ArrowTargetLock)
        {
            if (Input.GetKeyDown("space"))
            {

                foreach (GameObject tar in enemiesInRange)
                {
                    tar.SendMessage("Untargetted");
                    RaycastHit2D rayHit = Physics2D.Raycast((Vector2)myHero.transform.position, (Vector2)tar.GetComponent<Transform>().position - (Vector2)myHero.transform.position);
                    // Debug.DrawRay((Vector2)myHero.transform.position, (Vector2)theSelectedTarget.GetComponent<Transform>().position - (Vector2)myHero.transform.position, Color.cyan, 3f);
                    // Debug.Log("Mage ray targetting: " + rayHit.collider.gameObject.name);
                    // cam.GetComponent<CameraFollow>().UnsetTargettingCam();
                    toRet.Add(rayHit.collider.gameObject);
                }
                ArrowTargetLock = true;
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
            ArrowTargetLock = false;
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
        return "Basic Arrow Attack";
    }

    public void clearTarget()
    {
        toRet.Clear();
    }

}
