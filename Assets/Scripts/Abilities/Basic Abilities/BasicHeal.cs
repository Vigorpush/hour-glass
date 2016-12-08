using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicHeal : AttackTemplate
{

    protected Transform startTransform;
    protected GameObject[] allAllies;
   // protected GameObject[] enemiesInRange;
    protected GameObject theSelectedTarget;
    protected SpriteRenderer targetSprite;
    protected int[] damageSteps = new int[3];
    protected int healAmount;
    protected HeroUnit myHero;
    public int attackDamageScaler;
    protected List<KeyCode> theCombo;
    protected bool cycleTargetLock;
    protected int sizeOfTargetArray;
    protected int currentSelectedIndex;
    List<GameObject> toRet;
    protected GameObject cam;
    protected GameObject thePlayer;
    public AudioSource tarSound;
    public GameObject theMaestro;

    // Use this for initialization
    void Start()
    {
        theMaestro = GameObject.FindGameObjectWithTag("Maestro");
        cycleTargetLock = true;
        toRet = new List<GameObject>();
        hasASpellAnimation = true;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public override void informOfParent(GameObject playerIn)
    {
        thePlayer = playerIn;
        startTransform = thePlayer.GetComponent<Transform>();
        myHero = thePlayer.GetComponent<HeroUnit>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!cycleTargetLock)
        {
            if (Input.GetKeyDown("space"))
            {
                cycleTargetLock = true;
                theSelectedTarget.SendMessage("Untargetted");
               // RaycastHit2D rayHit = Physics2D.Raycast((Vector2)myHero.transform.position, (Vector2)theSelectedTarget.GetComponent<Transform>().position - (Vector2)myHero.transform.position);
                //Debug.DrawRay((Vector2)myHero.transform.position, (Vector2)theSelectedTarget.GetComponent<Transform>().position - (Vector2)myHero.transform.position, Color.cyan, 3f);
                // Debug.Log("Mage ray targetting: " + rayHit.collider.gameObject.name);
                // cam.GetComponent<CameraFollow>().UnsetTargettingCam();
               // toRet.Add(rayHit.collider.gameObject);
                thePlayer.gameObject.GetComponent<PlayerAttackController>().setHealTarget(theSelectedTarget,damageSteps[0]*2);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                theMaestro.SendMessage("TargetPing");
                theSelectedTarget.SendMessage("Untargetted");
                currentSelectedIndex = (currentSelectedIndex + 1) % allAllies.Length;
                theSelectedTarget = allAllies[currentSelectedIndex];
                theSelectedTarget.SendMessage("BeingTargetted");
                // cam.GetComponent<CameraFollow>().SetTargettingCam(theSelectedTarget);
                // Debug.DrawRay((Vector2)myHero.transform.position, (Vector2)theSelectedTarget.GetComponent<Transform>().position - (Vector2)myHero.transform.position, Color.cyan, 3f);
               // Debug.Log(allAllies.Length + "Archer arrow targetting: " + theSelectedTarget.gameObject.name);
            }
        }
    }

    public override void CheckLine()
    {
        theMaestro.SendMessage("TargetPing");
       // allAllies = null;
        List<RaycastHit2D> toRet = new List<RaycastHit2D>();

        allAllies = new GameObject[3];

        allAllies[0] = GameObject.FindGameObjectWithTag("Player1");
        allAllies[1] = GameObject.FindGameObjectWithTag("Player2");
        allAllies[2] = GameObject.FindGameObjectWithTag("Player3");

        healAmount = myHero.getattack() + myHero.weap.getDamage();

        toRet.Clear();
        sizeOfTargetArray = allAllies.Length;

        if (sizeOfTargetArray <= 0)
        {
            thePlayer.GetComponent<PlayerAttackController>().setAttackTargets(null);
        }
        else
        {
            currentSelectedIndex = 0;
            theSelectedTarget = allAllies[0];
            theSelectedTarget.SendMessage("BeingTargetted");
            cycleTargetLock = false;
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
        theCombo.Add(KeyCode.Alpha1);
        theCombo.Add(KeyCode.Alpha1);
        theCombo.Add(KeyCode.Alpha1);
        //  Debug.Log("Basic attack made a list");
        return theCombo;
    }

    public override string getMyName()
    {
        return "Basic Heal Ability";
    }

    public void clearTarget()
    {
        toRet.Clear();
    }

}
