using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttackController : MonoBehaviour {

	private const int NUMBER_OF_ABILITIES = 4;
	//Dummy action for when less skills are mapped
	public bool[] emptySlots = new bool[NUMBER_OF_ABILITIES]; 
	// Find the collection library public Hashtable<string,Abiity> playerAbilities;
	public List<BasicAttack> activeAbilities;
	private bool allowAttack=false;
	public HeroUnit myHero;
	// Use this for initialization
	private Animator anim;

	/*public GameObject ability1;
	public GameObject ability2;
	public GameObject ability3;
	public GameObject ability4;*/

	private bool baseButtons;
	private bool hasBaseAttack;


	void Start () {

		myHero = GetComponent<HeroUnit> ();
		anim = GetComponent<Animator> ();
		if (emptySlots [0] == true) {
			hasBaseAttack=false;
		}
		//abilities = new ArrayList();
		//On build get 3 active abilities and 1 basic attack   //Implies that wait, defend are the same all the time
		activeAbilities = new List<BasicAttack>();
		activeAbilities.Capacity = NUMBER_OF_ABILITIES;
		//TESTTESTTEST
		BasicAttack testAtk = new BasicAttack();
		testAtk.damage = 10;

		activeAbilities.Add(testAtk);
		baseButtons = true;

	}
	void setEmptySlots(){
		int i = 0;
		activeAbilities.ForEach(delegate(BasicAttack ab) 
		{
				if(ab == null){

					emptySlots[i]=true;
				}
		});
		if (activeAbilities [0] == null) {
			hasBaseAttack = false;
		}
	}
	//Basic = 0, A1=1 ....
	void Update (){	
		//WHEN CAN USE SPECIAL ABILITIES	
		if (!baseButtons) {
			if (Input.GetKeyDown(KeyCode.Alpha1) && emptySlots [1] == false) {//if (Input.GetButtonDown ("AButton") && emptySlots [0] == false) {
				ExecuteAbility (activeAbilities [1]);
			}
			if (Input.GetKeyDown(KeyCode.Alpha2) && emptySlots [2] == false){//if (Input.GetButtonDown ("XButton") && emptySlots [1] == false) {
				ExecuteAbility (activeAbilities [2]);
			}
			if(Input.GetKeyDown(KeyCode.Alpha3) && emptySlots [3] == false) {
				ExecuteAbility (activeAbilities [3]);
			}
		} else {
			//WHEN CANNOT USE SPECIAL ABILITIES
			if (Input.GetKeyDown (KeyCode.Alpha1) && hasBaseAttack == false) {
				//0 is used because
				ExecuteAbility (activeAbilities [0]);
				anim.SetTrigger ("Attack1");
			}
			if (Input.GetKeyDown(KeyCode.Alpha2)) {
				myHero.SendMessage ("WaitAction");
			}
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				myHero.SendMessage ("FortifyAction");
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha4) ){
			switchButtonInput ();
		}



	}
	public  void switchButtonInput(){
		baseButtons = !baseButtons;
    }
						

	void stopMovement(){
		SendMessage ("UnAllowMovement");
	}

	public void AllowAttack(){
		allowAttack = true;

	}

	public void ExecuteAbility(BasicAttack ability){
		//TEMP SOLUTION FOR TESTS
		//abilities[i].Execute;
		this.SendMessage("FindTargets");//return some thing

		Debug.Log("Start casting abilitiy");
	}


}
