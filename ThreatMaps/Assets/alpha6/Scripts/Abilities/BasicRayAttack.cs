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
    List<RaycastHit2D> toRet;
    private GameObject cam;

	// Use this for initialization
	void Start () {
	    startTransform = this.gameObject.GetComponent<Transform>();
        myHero = this.gameObject.GetComponent<HeroUnit>();
        cycleTargetLock = true;
        toRet = new List<RaycastHit2D>();
        hasASpellAnimation = true;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }
	
	// Update is called once per frame
	void Update () {
        if (!cycleTargetLock)
        {

            if(Input.GetKeyDown("space")){
                cycleTargetLock = true;
                targetSprite.color = Color.white;                
                RaycastHit2D rayHit = Physics2D.Raycast((Vector2)myHero.transform.position, (Vector2)theSelectedTarget.GetComponent<Transform>().position - (Vector2)myHero.transform.position);
                //Debug.DrawRay((Vector2)myHero.transform.position, (Vector2)theSelectedTarget.GetComponent<Transform>().position - (Vector2)myHero.transform.position, Color.cyan, 3f);
                //Debug.Log("Mage ray targetting: " + rayHit.collider.gameObject.name);
               // cam.GetComponent<CameraFollow>().UnsetTargettingCam();
                toRet.Add(rayHit);
                this.gameObject.GetComponent<PlayerAttackController>().setAttackTargets(toRet);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                targetSprite.color = Color.white;
                theSelectedTarget = enemiesInRange[currentSelectedIndex];
                targetSprite = theSelectedTarget.GetComponent<SpriteRenderer>();
                targetSprite.color = Color.red;
               // cam.GetComponent<CameraFollow>().SetTargettingCam(theSelectedTarget);
               // Debug.DrawRay((Vector2)myHero.transform.position, (Vector2)theSelectedTarget.GetComponent<Transform>().position - (Vector2)myHero.transform.position, Color.cyan, 3f);
               // Debug.Log("Mage ray targetting: " + theSelectedTarget.gameObject.name);
                if (currentSelectedIndex >= sizeOfTargetArray - 1)
                {
                    currentSelectedIndex = 0;
                }
                else
                {
                    currentSelectedIndex++;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                targetSprite.color = Color.white;
                theSelectedTarget = enemiesInRange[currentSelectedIndex];
                targetSprite = theSelectedTarget.GetComponent<SpriteRenderer>();
                targetSprite.color = Color.red;
               // cam.GetComponent<CameraFollow>().SetTargettingCam(theSelectedTarget);
               // Debug.DrawRay((Vector2)myHero.transform.position, (Vector2)theSelectedTarget.GetComponent<Transform>().position - (Vector2)myHero.transform.position, Color.cyan, 3f);
               // Debug.Log("Mage ray targetting: " + theSelectedTarget.gameObject.name);
                if (currentSelectedIndex <= 0)
                {
                    currentSelectedIndex = sizeOfTargetArray-1;
                }
                else
                {
                    currentSelectedIndex--;
                }
            }
        }
	}

    public override void CheckLine()
    {
        enemiesInRange = null;
        List<RaycastHit2D> toRet = new List<RaycastHit2D>();
        enemiesInRange = GameObject.FindGameObjectsWithTag("Baddy");
        //All enemies on board

        //All eneimes in range

        toRet.Clear();
        sizeOfTargetArray = enemiesInRange.Length;

        if (sizeOfTargetArray <= 0)
        {
            this.gameObject.GetComponent<PlayerAttackController>().setAttackTargets(toRet);
        }
        else { 
            currentSelectedIndex = 0;
            theSelectedTarget = enemiesInRange[0];
            targetSprite = theSelectedTarget.GetComponent<SpriteRenderer>();
            targetSprite.color = Color.red;
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
            return "Basic Ray Attack";
        }

    public void clearTarget()
    {
        toRet.Clear();
    }

}
