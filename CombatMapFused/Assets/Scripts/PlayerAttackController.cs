using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class PlayerAttackController : MonoBehaviour {

	private const int NUMBER_OF_ABILITIES = 4;
	//Dummy action for when less skills are mapped
	public bool[] emptySlots = new bool[NUMBER_OF_ABILITIES]; 
	// Find the collection library public Hashtable<string,Abiity> playerAbilities;
	public List<BasicAttack> activeAbilities;
	public bool allowAttack;
	public bool AllowAttack { get { return allowAttack; } set { allowAttack = value;} }
	//public BasicAttack test;
	public HeroUnit myHero;
	// Use this for initialization
	private Animator anim;

	/*public GameObject ability1;
	public GameObject ability2;
	public GameObject ability3;
	public GameObject ability4;*/

	private bool baseButtons;
	private bool hasBaseAttack;

	void Awake(){
		//activeAbilities = new List<BasicAttack> ();
	}

	void Start () {
		//activeAbilities = new List<BasicAttack> ();
		//activeAbilities.Insert (0,test);
		//Debug.Log (activeAbilities.ToString());
		myHero = GetComponent<HeroUnit> (); //!
		anim = GetComponent<Animator> ();
		setEmptySlots ();
		if (emptySlots [0] == true) {
			hasBaseAttack=false;
		}

		//On build get 3 active abilities and 1 basic attack   //Implies that wait, defend are the same all the time

		//activeAbilities.Capacity = NUMBER_OF_ABILITIES;

		baseButtons = true;	
	}

	/*void AllowAttack(){
		allowAttack = true;

		//attackController = GetComponent<PlayerAttackController> ();
		//AllowMovement();
		//attackController.AllowAttack = true;
		Debug.Log( "I, " + this.gameObject.name + " Started turn and can attack.");

	}
	*/
	void setEmptySlots(){
		int i = 0;

		activeAbilities.ForEach(delegate(BasicAttack ab) 
		{
				if(ab == null){

					emptySlots[i]=true;
				}
				i++;
		});
		if (activeAbilities [0] == null) {
			hasBaseAttack = false;
		}
	}
	//Basic = 0, A1=1 ....
	void Update (){	
		//WHEN CAN USE SPECIAL ABILITIES
		if (allowAttack) {
			if (!baseButtons) {
				if (Input.GetKeyDown (KeyCode.Alpha1) && emptySlots [1] == false) {//if (Input.GetButtonDown ("AButton") && emptySlots [1] == false) {
					ExecuteAbility (activeAbilities [1]);
				}
				if (Input.GetKeyDown (KeyCode.Alpha2) && emptySlots [2] == false) {//if (Input.GetButtonDown ("XButton") && emptySlots [2] == false) {
					ExecuteAbility (activeAbilities [2]);
				}
				if (Input.GetKeyDown (KeyCode.Alpha3) && emptySlots [3] == false) {
					ExecuteAbility (activeAbilities [3]);
				}
			} else {
				//WHEN CANNOT USE SPECIAL ABILITIES
				if (Input.GetKeyDown (KeyCode.Alpha1) && hasBaseAttack == false) {
					//0 is used because
					Debug.Log ("press 1");
					ExecuteAbility (activeAbilities [0]);
					anim.SetTrigger ("Attack1");
				}
				if (Input.GetKeyDown (KeyCode.Alpha2)) {
					myHero.SendMessage ("WaitAction");
				}
				if (Input.GetKeyDown (KeyCode.Alpha3)) {
					myHero.SendMessage ("FortifyAction");
				}
			}
			if (Input.GetKeyDown (KeyCode.Alpha4)) {
				switchButtonInput ();
			}
		}
	}
	public  void switchButtonInput(){
		baseButtons = !baseButtons;
    }
						

	void stopMovement(){
		SendMessage ("UnAllowMovement");
	}
	//public void attackReady;
	public void ExecuteAbility(BasicAttack ability){
		Debug.Log("Start casting ability");
		anim.SetTrigger ("Attack1");

		allowAttack = false;
		//List<GameObject> thisAttackTargets = ability.FindTargets ();
		List<RaycastHit2D> thisAttackTargets = ability.CheckLine ();

		foreach (RaycastHit2D target in thisAttackTargets) {
			GameObject tar = target.collider.gameObject;
			tar.SendMessage ("DoDamage",ability.damage);

		}
			
	}


	}



