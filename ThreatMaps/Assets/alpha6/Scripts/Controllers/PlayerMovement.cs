using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
	const int BUMP_DIST = 1; // The range that will check for movement collisions
	const float TURN_BUFFER = .5f;
	public Transform tf;
	public float speed;
	public Vector2 pos;
	public float h,v;
	public Vector2 initialPosition = new Vector2(2,2);
	RaycastHit2D rayCast;
	public bool moving=false,isTurn=false,endTurnBuffer=false;  //isTurn technically means that it is your turn AND you can move
	public GameObject turnManager;
	public Animator anim;
	public PlayerAttackController attackController;
    public Text inputInstruction;
    bool turnOver;

    // Use this for initialization
    void Start () {	
		initializeController ();
	}

	void checkEndTurn(){
		if (Input.GetButton ("Jump")) {
            turnOver = true;
            Invoke("EndTurn", 0.1f); //for some reason, if not invoked this disables the next players end turn
		}
	}

	// Update is called once per phys tick
	void FixedUpdate () {
      // Debug.Log(this.gameObject.name + " is allowed to move or attack: " +turnOver);
		if (isTurn ) {
			checkPlayerMovement ();  //listen for WASD movement input
			checkEndTurn ();    //listen for endturn button
			tf.position = Vector3.MoveTowards (tf.position, pos, Time.deltaTime * speed);   //animate movement
			//Debug.DrawRay (pos,(Vector2)(tf.up), Color.blue);
		}
	}
		
	//----------------------------------------------------//
	//HELPERS
	public void AllowMovement()
	{
		isTurn = true;
		//Debug.Log ("is turn on");
	}
	public void UnAllowMovement()
	{
		isTurn = false;
		moving = false;

		//Debug.Log ("is turn on");
	}
	//Ends turn and gets to next turn faster
	public void WaitAction(int speedUp){
		GetComponent<HeroUnit> ().initiative -= speedUp;
		EndTurn ();
	}

	public void FortifyAction(float mult){
		HeroUnit player = GetComponent<HeroUnit> ();
		player.isFortified = true;
		player.fortifyMultiplier =+mult;
		EndTurn ();
	}

	void initializeController(){
        turnOver = false;
		turnManager = GameObject.Find("Manager");
		tf = GetComponent<Transform>();
		tf.position = initialPosition;
		pos=tf.position;
		//Debug.Log ("Initial postion" + tf.position);

        anim = GetComponent<Animator>();
        anim.SetBool("Moving", false);
        if (speed == null || speed==0) {
			speed = 5;
		}
	}

	public void DisableMovement(){
		isTurn = false;
	}

	//Limits input speed
	void startMoveCooldown(){
		movingOn ();
		Invoke ("movingOff", 1f/ speed);
	}

	//Makes movement less choppy
	void movingOn(){
		moving = true;
		//Debug.Log ("moving on");
		attackController.allowAttack=false;
		//Debug.Log (this.gameObject.name + "This game object is not allowed attacking and is moving.");
        anim.SetBool("Moving", true);
    }
	void movingOff(){
        //Debug.Log ("moving off");
        //attackController.SendMessage("AllowAttack");
        //Debug.Log (this.gameObject.name + "This game object is allowed to attack and is notmoving.");
        if (!turnOver) {
            attackController.allowAttack = true; //needed to you don't turn attack on after ending turn while moving
        }
        anim.SetBool("Moving", false);
        moving = false;
	}
	//----------------------------------------------------//

	//Turn Management
	public void EndTurn(){
		if(endTurnBuffer){
		    isTurn = false;
		    //Debug.Log("endingturn");
		    if (turnManager != null) {
			    endTurnBuffer = false;
             attackController.allowAttack = false;
             this.gameObject.GetComponent<BasicRayAttack>().clearTarget();
                turnManager.SendMessage ("CalculateTurn");
		    } else {
			    Debug.Log ("No manager found");
		    }
		}
	}
	void endOK(){
		endTurnBuffer = true;
	}
	//Starts turn which allows movement and attack controls
	void StartTurn(){
        inputInstruction.text = "Press 1 for Melee attack\nPress 2 to target (spacebar confirms)";
        SendMessage ("AllowMovement");
		Invoke ("endOK", TURN_BUFFER);
        this.GetComponent<PlayerAttackController>().enableAttackInput();
		attackController = this.gameObject.GetComponent<PlayerAttackController> ();
        Debug.Log("Beginning turn :" + this.gameObject.name);
		//AllowMovement();
		attackController.allowAttack = true;
        turnOver = false;
    }

	//RAY CAST METHODS
	//-------------------------------------------------//
	//Checks for collisions with units in a line of length L
	RaycastHit2D rayCheckLine(int L){
		return Physics2D.Linecast(tf.position,(Vector2)(pos+(Vector2)(tf.up)),L);
	}

	//Input Handling
	void checkPlayerMovement(){
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
		if (Input.GetButton("Horizontal") && !moving) {  
			if (h>0) {	
				tf.localEulerAngles = new Vector3 (0, 0, 270);   // Always rotate the character even if you cannot move
				rayCast = rayCheckLine (BUMP_DIST);
				if (rayCast.collider == null) {
					pos += Vector2.right;
					startMoveCooldown ();
				} else {
					//Debug.Log ("Move right failed:" + rayCast.collider.gameObject.name + " in the way.");
				}
			} else {
				
				tf.localEulerAngles = new Vector3 (0, 0, 90);
				rayCast = rayCheckLine (BUMP_DIST);
				if (rayCast.collider == null) {	
					pos += Vector2.left;
					//anim.SetTrigger ("Walking");
					startMoveCooldown ();
				
				} else {
					//Debug.Log ("Move left failed:" + rayCast.collider.gameObject.name  + " in the way.");
				}
			}
		}
		else if (Input.GetButton("Vertical")&& !moving) {
			if (v>0)
			{
				
				tf.localEulerAngles = new Vector3 (0, 0, 0);
				rayCast = rayCheckLine (BUMP_DIST);
				if (rayCast.collider == null) {	
					pos += Vector2.up;
					//anim.SetTrigger("Walking");
					startMoveCooldown ();
				} else {
					//Debug.Log ("Move up failed:" + rayCast.collider.gameObject.name  + " in the way.");				
				}
			}
			else
			{
				
				tf.localEulerAngles = new Vector3 (0, 0, 180);
				rayCast = rayCheckLine (BUMP_DIST);
				if (rayCast.collider == null) {	
					pos += Vector2.down;
					//anim.SetTrigger ("Walking");
					startMoveCooldown ();
				} else {
					//Debug.Log("Move down failed:" + rayCast.collider.gameObject.name + " in the way.");
				}
			}
		}
	}
}