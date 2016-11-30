using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicRayAttack : AttackTemplate {

    private Transform startTransform;
    private GameObject[] allEnemies;
    private GameObject[] enemiesInRange;
    private GameObject theSelectedTarget;
    private SpriteRenderer targetSprite;
    private int[] damageSteps = new int[3];
    private HeroUnit myHero;
    public int attackDamageScaler;
    private List<KeyCode> theCombo;
    private bool cycleTargetLock;
    private int sizeOfTargetArray;
    private int currentSelectedIndex;
    List<GameObject> toRet;
    private GameObject cam;
    private GameObject thePlayer;
    public GameObject theMaestro;
    

	// Use this for initialization
	void Start () {
        theMaestro = GameObject.FindGameObjectWithTag("Maestro");
        cycleTargetLock = true;
        toRet = new List<GameObject>();
        hasASpellAnimation = true;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public override void informOfParent(GameObject playerIn)
    {
        thePlayer = playerIn;
        startTransform = thePlayer.gameObject.GetComponent<Transform>();
        myHero = thePlayer.gameObject.GetComponent<HeroUnit>();

    }

    // Update is called once per frame
    void Update () {
        if (!cycleTargetLock)
        {
            if(Input.GetKeyDown("space")){
                cycleTargetLock = true;
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
                Debug.Log(enemiesInRange.Length+ "Mage ray targetting: " + theSelectedTarget.gameObject.name);
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
        sizeOfTargetArray = enemiesInRange.Length;

        if (sizeOfTargetArray <= 0)
        {
            thePlayer.gameObject.GetComponent<PlayerAttackController>().setAttackTargets(null);
        }
        else { 
            currentSelectedIndex = 0;
            theSelectedTarget = enemiesInRange[currentSelectedIndex];
           // Debug.Log("Mage ray targetting: " + theSelectedTarget.gameObject.name);
            theSelectedTarget.SendMessage("BeingTargetted");
            cycleTargetLock = false;
        }
    }

        public override int[] GetDamageSteps()
    {
       // Debug.Log("Basic attack script thinks it is attached to " + myHero);
         damageSteps[0]= myHero.getattack() + myHero.weap.getDamage();
         damageSteps[1] = myHero.getattack() + myHero.weap.getDamage();
         damageSteps[2] = myHero.getattack() + myHero.weap.getDamage();
         return damageSteps;
    }

        public override List<KeyCode> GetComboInputSequence()
        {
            theCombo = new List<KeyCode>();
            theCombo.Add(KeyCode.Alpha3);
            theCombo.Add(KeyCode.Alpha2);
            theCombo.Add(KeyCode.Alpha1);
            //  Debug.Log("Basic attack made a list");
            return theCombo;
        }

        public override string getMyName()
        {
            return "Fireball";
        }

    public void clearTarget()
    {
        toRet.Clear();
    }

}
