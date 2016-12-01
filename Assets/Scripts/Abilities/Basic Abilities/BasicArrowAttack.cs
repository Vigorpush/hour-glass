using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicArrowAttack : AttackTemplate
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
       // Debug.Log("arrow attack woke up");
        ArrowTargetLock = true;
        //Debug.Log(ArrowTargetLock);
        hasASpellAnimation = false;
        isARangerAbility = true;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        theMaestro = GameObject.FindGameObjectWithTag("Maestro");
    }

    void Start()
    {
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
                ArrowTargetLock = true;
                theSelectedTarget.SendMessage("Untargetted");
                RaycastHit2D rayHit = Physics2D.Raycast((Vector2)myHero.transform.position, (Vector2)theSelectedTarget.GetComponent<Transform>().position - (Vector2)myHero.transform.position);
                //Debug.DrawRay((Vector2)myHero.transform.position, (Vector2)theSelectedTarget.GetComponent<Transform>().position - (Vector2)myHero.transform.position, Color.cyan, 3f);
                // Debug.Log("Mage ray targetting: " + rayHit.collider.gameObject.name);
                // cam.GetComponent<CameraFollow>().UnsetTargettingCam();
                toRet.Add(rayHit.collider.gameObject);
                thePlayer.gameObject.GetComponent<PlayerAttackController>().setAttackTargets(toRet);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                theMaestro.SendMessage("TargetPing");
                theSelectedTarget.SendMessage("Untargetted");
                currentSelectedIndex = (currentSelectedIndex + 1) % enemiesInRange.Length;
                theSelectedTarget = enemiesInRange[currentSelectedIndex];
                theSelectedTarget.SendMessage("BeingTargetted");
                // cam.GetComponent<CameraFollow>().SetTargettingCam(theSelectedTarget);
                // Debug.DrawRay((Vector2)myHero.transform.position, (Vector2)theSelectedTarget.GetComponent<Transform>().position - (Vector2)myHero.transform.position, Color.cyan, 3f);
                Debug.Log(enemiesInRange.Length + "Archer arrow targetting: " + theSelectedTarget.gameObject.name);
            }
        }
    }

    public override void CheckLine()
    {
        theMaestro.SendMessage("TargetPing");
        
        allEnemies = GameObject.FindGameObjectsWithTag("Baddy");       
        int i = 0;
        foreach (GameObject tar in allEnemies)
        {
            if (!tar.GetComponent<EnemyUnit>().getDying())
            {
                i++;
            }
        }
        enemiesInRange = new GameObject[i];
        int j = 0;
        foreach (GameObject tar in allEnemies)
        {
            if (!tar.GetComponent<EnemyUnit>().getDying())
            {
                enemiesInRange[j] = tar;
                j++;
            }
        }


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
            currentSelectedIndex = 0;
            theSelectedTarget = enemiesInRange[0];
            theSelectedTarget.SendMessage("BeingTargetted");
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
