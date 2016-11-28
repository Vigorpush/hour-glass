using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class PlayerAttackController : MonoBehaviour {

	private const int NUMBER_OF_ABILITIES = 4;

	public bool[] emptySlots = new bool[NUMBER_OF_ABILITIES]; 

	public List<AttackTemplate> activeAbilities;
	public bool allowAttack;

	public HeroUnit myHero;
	// Use this for initialization
    public Animator anim;
  public  List<GameObject> thisAttackTargets;
  public List<KeyCode> theInputButtons;
  public int[] damageValues;
  private bool inputActivated;
  public bool abilityIsASpell;
  public bool rangerAbility;
  public GameObject cam;
    public bool showHelpText;

    //Known abilities
    public GameObject meleePrefab;
    public GameObject arrowPrefab;
    public GameObject rayPrefab;
    public GameObject healPrefab;
    public GameObject LightningStormPrefab;
    public GameObject MageMissileBarragePrefab;
    public GameObject MultishotPrefab;
    public GameObject WhirlwindPrefab;

    //Ability icons
    public Sprite meleeIcon;
    public Sprite rayIcon;
    public Sprite healIcon;
    public Sprite arrowIcon;
    public Sprite LightningStormIcon;
    public Sprite MissileStormIcon;
    public Sprite MultishotIcon;
    public Sprite whirlwindIcon;
    public Sprite noAbilityIcon;

    public Sprite[] myAbilityIcons;

    //instructions
    public Text inputInstruction;

    //bools to determine what buttons can be pressed on interface
    private bool baseButtons;
	private bool hasBaseAttack;

    private string spellName;

    public AudioSource abilitySound;
    private AudioSource[] unitSounds;

    public void setAttackTargets(List<GameObject> listIn)
    {
        this.gameObject.GetComponent<ComboController>().setAbilityActivator(this.gameObject);
        if (listIn ==null)
        {
           // Debug.Log(this.gameObject.name + "'s Attack controller told no targets");
            anim.SetTrigger("Attack1");
            anim.SetTrigger("ComboFail");
            this.gameObject.GetComponent<PlayerMovement>().EndTurn();
            return;
        }
        else {
            unitSounds[3].Play();
            cam.GetComponent<CameraFollow>().SetCameraFollow(this.gameObject);
            thisAttackTargets = listIn;
            if (abilityIsASpell)
            {
                this.gameObject.GetComponent<ComboController>().setThisSpellName(spellName);
               // Debug.Log("Casting: " + spellName);
            }
            
            //Debug.Log("params: " +thisAttackTargets.Count+" "+abilityIsASpell+" " + rangerAbility + theInputButtons + thisAttackTargets[0].collider.gameObject.name + damageValues+ this.gameObject.name);
            this.gameObject.GetComponent<ComboController>().BeginComboInput(theInputButtons, thisAttackTargets, damageValues, abilityIsASpell, rangerAbility);          
        }
    }

    public void setHealTarget(GameObject theTarget, int healAmount)
    {
        if (theTarget ==null )
        {
            // Debug.Log(this.gameObject.name + "'s Attack controller told no targets");
            anim.SetTrigger("Attack1");
            anim.SetTrigger("ComboFail");
            this.gameObject.GetComponent<PlayerMovement>().EndTurn();
            return;
        }
        else
        {
            unitSounds[3].Play();
            anim.SetTrigger("Attack1");
            anim.SetTrigger("ComboFail");
            //Debug.Log(theTarget.name);
            theTarget.GetComponent<Unit>().heal(healAmount);
            Invoke("endTurnWithBuffer", 1f);
        }
    }

    private void endTurnWithBuffer()
    {
        this.gameObject.GetComponent<PlayerMovement>().EndTurn();
    }

    public void disableHelpText()
    {
        showHelpText = false;
    }


    void Start () {
        unitSounds=GetComponents<AudioSource>();

        showHelpText = true;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
		myHero = GetComponent<HeroUnit> (); //!
		anim = GetComponent<Animator> ();
        CharacterBuilder charBuilder = GetComponent<CharacterBuilder>();
        abilityIsASpell =false;
        rangerAbility = false;
        if (emptySlots [0] == true) {
			hasBaseAttack=false;
		}
        thisAttackTargets = new List<GameObject>();

		//On build get 3 active abilities and 1 basic attack   //Implies that wait, defend are the same all the time
		baseButtons = true;

        myAbilityIcons = new Sprite[3];


    }

    public void buildActiveAbilites(List<GameObject> abilitiesIn)
    {
        List<AttackTemplate> toRet =  new List<AttackTemplate>();
        //Debug.Log(abilitiesIn.Count);

        for (int i = 0; i < 3; i++)
        {
            
            if (abilitiesIn[i] != null)
            {
               // Debug.Log(this.gameObject.name + " working with " + abilitiesIn[i]);
                emptySlots[i] = true;
                GameObject tempAbil;
                switch (abilitiesIn[i].gameObject.name)
                {
                    case "BasicMeleeKey":
                       // Debug.Log("Player: "+this.gameObject.name + " gets a(n): "+abilitiesIn[i].gameObject.name);
                        tempAbil = Instantiate(meleePrefab) as GameObject;
                        tempAbil.GetComponent<AttackTemplate>().informOfParent(gameObject);
                        emptySlots[i] = true;
                        myAbilityIcons[i] = meleeIcon;
                        toRet.Add(tempAbil.GetComponent<AttackTemplate>());
                        break;
                    case "BasicArrowKey":
                       // Debug.Log("Player: " + this.gameObject.name + " gets a(n): " + abilitiesIn[i].gameObject.name);
                        tempAbil = Instantiate(arrowPrefab) as GameObject;
                        tempAbil.GetComponent<AttackTemplate>().informOfParent(gameObject);
                        emptySlots[i] = true;
                        myAbilityIcons[i] = arrowIcon;
                        toRet.Add(tempAbil.GetComponent<AttackTemplate>());
                        break;
                    case "BasicRayKey":
                       // Debug.Log("Player: " + this.gameObject.name + " gets a(n): " + abilitiesIn[i].gameObject.name);
                        tempAbil = Instantiate(rayPrefab) as GameObject;
                        tempAbil.GetComponent<AttackTemplate>().informOfParent(gameObject);
                        emptySlots[i] = true;
                        myAbilityIcons[i] = rayIcon;
                        toRet.Add(tempAbil.GetComponent<AttackTemplate>());
                        break;
                    case "BasicHealKey":
                       // Debug.Log("Player: " + this.gameObject.name + " gets a(n): " + abilitiesIn[i].gameObject.name);
                        tempAbil = Instantiate(healPrefab) as GameObject;
                        tempAbil.GetComponent<AttackTemplate>().informOfParent(gameObject);
                        emptySlots[i] = true;
                        myAbilityIcons[i] = healIcon;
                        toRet.Add(tempAbil.GetComponent<AttackTemplate>());
                        break;
                    case "LightningStormKey":
                       // Debug.Log("Player: " + this.gameObject.name + " gets a(n): " + abilitiesIn[i].gameObject.name);
                        tempAbil = Instantiate(LightningStormPrefab) as GameObject;
                        tempAbil.GetComponent<AttackTemplate>().informOfParent(gameObject);
                        emptySlots[i] = true;
                        myAbilityIcons[i] = LightningStormIcon;
                        toRet.Add(tempAbil.GetComponent<AttackTemplate>());
                        break;
                    case "MageMissileBarrageKey":
                       // Debug.Log("Player: " + this.gameObject.name + " gets a(n): " + abilitiesIn[i].gameObject.name);
                        tempAbil = Instantiate(MageMissileBarragePrefab) as GameObject;
                        tempAbil.GetComponent<AttackTemplate>().informOfParent(gameObject);
                        emptySlots[i] = true;
                        myAbilityIcons[i] = MissileStormIcon;
                        toRet.Add(tempAbil.GetComponent<AttackTemplate>());
                        break;
                    case "MultishotKey":
                       // Debug.Log("Player: " + this.gameObject.name + " gets a(n): " + abilitiesIn[i].gameObject.name);
                        tempAbil = Instantiate(MultishotPrefab) as GameObject;
                        tempAbil.GetComponent<AttackTemplate>().informOfParent(gameObject);
                        emptySlots[i] = true;
                        myAbilityIcons[i] = MultishotIcon;
                        toRet.Add(tempAbil.GetComponent<AttackTemplate>());
                        break;
                    case "WhirlwindKey":
                       // Debug.Log("Player: " + this.gameObject.name + " gets a(n): " + abilitiesIn[i].gameObject.name);
                        tempAbil = Instantiate(WhirlwindPrefab) as GameObject;
                        tempAbil.GetComponent<AttackTemplate>().informOfParent(gameObject);
                        emptySlots[i] = true;
                        myAbilityIcons[i] = whirlwindIcon;
                        toRet.Add(tempAbil.GetComponent<AttackTemplate>());
                        break;

                }
            }
            else
            {
                emptySlots[i] = false;
                myAbilityIcons[i] = noAbilityIcon;
               // Debug.Log(this.gameObject.name + " setting icon to empty for slot: "+i);
            }
        }
       // Debug.Log("Abilities for "+this.gameObject.name + " was this long: "+toRet.Count);
        activeAbilities = toRet;
    }

    public Sprite[] getMyAbilityIcons()
    {
        return myAbilityIcons;
    }

	void Update (){
        if (inputActivated) {
            //WHEN CAN USE SPECIAL ABILITIES
            if (allowAttack) {
                if (!baseButtons) {
                    if (Input.GetKeyDown(KeyCode.Alpha1) && emptySlots[1] == false) {//if (Input.GetButtonDown ("AButton") && emptySlots [1] == false) {
                        ExecuteAbility(activeAbilities[1]);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2) && emptySlots[2] == false) {//if (Input.GetButtonDown ("XButton") && emptySlots [2] == false) {
                        ExecuteAbility(activeAbilities[2]);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3) && emptySlots[3] == false) {
                        ExecuteAbility(activeAbilities[3]);
                    }
                } else {
                    //WHEN USING NORMAL ABILITIES
                    if (Input.GetKeyDown(KeyCode.Alpha1) &&emptySlots[0] ==true) {
                        ExecuteAbility(activeAbilities[0]);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2) && emptySlots[1] == true) {
                        //myHero.SendMessage ("WaitAction"); //this is what alpha2 should actually do, ray hardcoded
                        ExecuteAbility(activeAbilities[1]);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3) && emptySlots[2] == true) {
                       // myHero.SendMessage("FortifyAction"); //this is what alpha 3 should actually do, arrow hardcoded
                        ExecuteAbility(activeAbilities[2]);
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha4) && emptySlots[3] == true) {
                  //  switchButtonInput();  // this is what a4 should do
                    ExecuteAbility(activeAbilities[3]);
                }
            }
        }
	}
	public  void switchButtonInput(){
		baseButtons = !baseButtons;
    }
						
	void stopMovement(){
		SendMessage ("UnAllowMovement");
	}


    public void ExecuteAbility(AttackTemplate ability) {

       Debug.Log("executing " + ability.getMyName());
        if (ability.hasASpellAnimation)
        {
            abilityIsASpell = true;
            spellName = ability.getMyName();
        }
        else
        {
            abilityIsASpell = false;
        }

        if (ability.isARangerAbility) {
            rangerAbility = true;
        }
        else {
            rangerAbility = false;
        }

        allowAttack = false;
        stopMovement();
        theInputButtons = ability.GetComboInputSequence();
        damageValues = ability.GetDamageSteps();
        if (showHelpText) { 
           // inputInstruction.text = "Press spacebar to select targets, cycle with WASD";
        }
        // damageValues = setDamageValues();
        ability.CheckLine(); //start aquiring targets
  
        disableAttackInput();		
	}

    public void setDamageValues(int[] damageIn)
    {
        damageValues = damageIn;
    }

    public void enableAttackInput()
    {
        Invoke("enableAfterBuffer",0.5f);
    }

    public void enableAfterBuffer()
    {
        inputActivated = true;
       // Debug.Log("Listening for attack input from :" +this.gameObject.name);
    }

    public void disableAttackInput()
    {
        inputActivated = false;
    }


}



