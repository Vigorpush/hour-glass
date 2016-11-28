using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class PlayerAttackController : MonoBehaviour {

	private const int NUMBER_OF_ABILITIES = 4;
	//Dummy action for when less skills are mapped
	public bool[] emptySlots = new bool[NUMBER_OF_ABILITIES]; 
	// Find the collection library public Hashtable<string,Abiity> playerAbilities;
	public List<AttackTemplate> activeAbilities;
	public bool allowAttack;
	//public bool AllowAttack { get { return allowAttack; } set { allowAttack = value;} }
	//public BasicAttack test;
	public HeroUnit myHero;
	// Use this for initialization
    public Animator anim;
  public  List<RaycastHit2D> thisAttackTargets;
  public List<KeyCode> theInputButtons;
  public int[] damageValues;
  private bool inputActivated;
  public bool abilityIsASpell;
  public bool rangerAbility;
  public GameObject cam;

    /*public GameObject ability1;
	public GameObject ability2;
	public GameObject ability3;
	public GameObject ability4;*/

    private bool baseButtons;
	private bool hasBaseAttack;

    public void setAttackTargets(List<RaycastHit2D> listIn)
    {
        if (listIn.Count <= 0)
        {
           // Debug.Log(this.gameObject.name + "'s Attack controller told no targets");
            anim.SetTrigger("Attack1");
            anim.SetTrigger("ComboFail");
            this.gameObject.GetComponent<PlayerMovement>().EndTurn();
            return;
        }
        else {
            cam.GetComponent<CameraFollow>().SetCameraFollow(this.gameObject);
            thisAttackTargets = listIn;
            Debug.Log("hi" +thisAttackTargets.Count+" "+abilityIsASpell+" " + rangerAbility + theInputButtons + thisAttackTargets[0].collider.gameObject.name + damageValues+ this.gameObject.name);
            this.gameObject.GetComponent<ComboController>().BeginComboInput(theInputButtons, thisAttackTargets, damageValues, abilityIsASpell, rangerAbility);
            
        }
    }

	void Start () {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
		myHero = GetComponent<HeroUnit> (); //!
		anim = GetComponent<Animator> ();
		setEmptySlots ();
        abilityIsASpell =false;
        rangerAbility = false;
        if (emptySlots [0] == true) {
			hasBaseAttack=false;
		}
        thisAttackTargets = new List<RaycastHit2D>();

		//On build get 3 active abilities and 1 basic attack   //Implies that wait, defend are the same all the time
		baseButtons = true;	

	}

	void setEmptySlots(){
		int i = 0;
		activeAbilities.ForEach(delegate(AttackTemplate ab) 
		{
			if(ab == null){
					emptySlots[i]=true;
			}
			i++;
		});
		if (activeAbilities [0] == null) {
			hasBaseAttack = false;
		}

        //This is needed so each game object does not share the same basic attack!!!
        //Do this for all abilities, do not use unity inspector to drag in abilities for now.
        activeAbilities[0] = this.gameObject.AddComponent<BasicAttack>();
        activeAbilities[1] = this.gameObject.AddComponent<BasicRayAttack>();
        activeAbilities[2]= this.gameObject.AddComponent<BasicArrowAttack>();
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
                    if (Input.GetKeyDown(KeyCode.Alpha1) && hasBaseAttack == false) {
                        ExecuteAbility(activeAbilities[0]);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2)) {
                        //myHero.SendMessage ("WaitAction"); //this is what alpha2 should actually do, ray hardcoded
                        ExecuteAbility(activeAbilities[1]);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3)) {
                       // myHero.SendMessage("FortifyAction"); //this is what alpha 3 should actually do, arrow hardcoded
                        ExecuteAbility(activeAbilities[2]);
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha4)) {
                    switchButtonInput();
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


	public void ExecuteAbility(AttackTemplate ability){
        if (ability.hasASpellAnimation)
        {
            abilityIsASpell = true;
        }
        else
        {
            abilityIsASpell = false;
        }

        if(ability.isARangerAbility){
            rangerAbility = true;
        }
        else{
            rangerAbility = false;
        }
        
        allowAttack = false;
        stopMovement();
        theInputButtons =ability.GetComboInputSequence();
        damageValues = ability.GetDamageSteps();
        ability.CheckLine(); //start aquiring targets
        disableAttackInput();		
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



