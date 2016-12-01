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
    public bool exploreMode;
    public GameObject cam;  //the main camera

    //For UI building on turn
    public GameObject abil1UI;
    public GameObject abil2UI;
    public GameObject abil3UI;

    public GameObject UIportrait;
    public Sprite myPortrait;

    private GameObject[] abilitiesUI;
    private Sprite[] abilityIcons;
    public GameObject theFloorMaker;
    public GameObject myLantern;


    // Use this for initialization
    void Start () {	
		Invoke("initializeController",1f); //allows player time to teleport to start
        
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        attackController = this.gameObject.GetComponent<PlayerAttackController>();
        //UI 3 buttons, 4th button is switch panel
        abilitiesUI = new GameObject[3];
        abilitiesUI[0] = abil1UI;
        abilitiesUI[1] = abil2UI;
        abilitiesUI[2] = abil3UI;
        exploreMode = false;
     
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
        if(exploreMode){
            if (!isTurn) { 
             isTurn = true;
             cam.GetComponent<CameraFollow>().SetCameraFollow(this.gameObject);
            }
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

    public void SetStartingPosition(Vector2 positionIn)
    {
        initialPosition = positionIn;
        this.pos = positionIn;
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
            Debug.Log("I was told to end turn!");
		    isTurn = false;
		    //Debug.Log("endingturn");
		    if (turnManager != null) {
			    endTurnBuffer = false;
             attackController.allowAttack = false;
            // this.gameObject.GetComponent<BasicRayAttack>().clearTarget();
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
        //inputInstruction.text = "";

        UIportrait.GetComponent<Image>().overrideSprite = myPortrait;

        abilityIcons = attackController.getMyAbilityIcons();
        for (int i = 0; i < 3; i++)
        {
           // Debug.Log(this.gameObject.name + " setting ability "+ i + " sprite to " + abilityIcons[i].name);
            abilitiesUI[i].GetComponent<Image>().overrideSprite = abilityIcons[i];
        }

        SendMessage ("AllowMovement");
		Invoke ("endOK", TURN_BUFFER);
        this.GetComponent<PlayerAttackController>().enableAttackInput();
		
        Debug.Log("Beginning turn :" + this.gameObject.name);
		//AllowMovement();
		attackController.allowAttack = true;
        turnOver = false;
    }

    public void EnterExplorationMode()
    {
        exploreMode = true;
        abilityIcons = attackController.hiddenAbilityIcons();
        UIportrait.GetComponent<Image>().overrideSprite = myPortrait;
        for (int i = 0; i < 3; i++)
        {
            // Debug.Log(this.gameObject.name + " setting ability "+ i + " sprite to " + abilityIcons[i].name);
            abilitiesUI[i].GetComponent<Image>().overrideSprite = abilityIcons[i];
        }
        myLantern.SetActive(true);
        cam.GetComponent<CameraFollow>().SetCameraFollow(this.gameObject);
        Debug.Log(this.gameObject.name + ": I'm party lead, and exploring!");
        attackController.allowAttack = false;
       // SendMessage("AllowMovement");
        AllowMovement();
        isTurn = true;
    }

    //Invokable method to begin calculating intiaties and first turn;
    private void SpawnEncounter()
    {
        if (exploreMode)
        {
            
           // Debug.Log("Hit an encounter tile");
            int myX =(int) pos.x;
            int myY = (int)pos.y;
            TileType returned = theFloorMaker.GetComponent<floorMaker>().getTileAt(myX,myY);

            if(returned == TileType.floor){
                //Debug.Log(gameObject.name + "I'm exiting a room, no fight yet :" + returned);
            }
            else if (returned == TileType.hallFloor)
            {
                exploreMode = false;
              //  Debug.Log(this.gameObject.name +" I though I was facing:" + this.gameObject.transform.eulerAngles.z);

                //Nudge forward 1 tile
                if (this.gameObject.transform.eulerAngles.z == 270)//if facing right
                {
                   // Debug.Log("```````I wanted to go right");
                    pos += Vector2.right;
                }
                if (this.gameObject.transform.eulerAngles.z == 180) //if facing down
                {
                   // Debug.Log("```````I wanted to go down");
                    pos += Vector2.down;
                }
                if (this.gameObject.transform.eulerAngles.z == 90)  //if facing left
                {
                  //  Debug.Log("```````I wanted to go left");
                    pos += Vector2.left;
                }
                if (this.gameObject.transform.eulerAngles.z == 0) //if facing up
                {
                   // Debug.Log("```````I wanted to go up");
                    pos += Vector2.up;
                }
                Invoke("UnAllowMovement",1f); //allow time to walk into room
                //Debug.Log(gameObject.name + "I kick down the door :" + returned);
                Invoke("surveyRoomEntered", 1f);

                
                
                //compare returned and position
                //move that way
            } 
        }     
    }

    public void CollapseForExploration()
    {
        Debug.Log(this.gameObject.name + ": That was a hard fight, I'm taking a break.");
        // this.gameObject.SetActive(false);
        //disable collider
         myLantern.SetActive(false);

    GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void EnterCombat(Vector2 spawnPos)
    {
        myLantern.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = true;
        this.transform.position = spawnPos;
        Debug.Log(this.gameObject.name + ": Ready for combat!");

    }

	//RAY CAST METHODS
	//-------------------------------------------------//
	//Checks for collisions with units in a line of length L

    RaycastHit2D rayCheckLine(int L)
    {
        RaycastHit2D rayHit = Physics2D.Linecast(tf.position, (Vector2)(pos + (Vector2)(tf.up)), L);
        if (rayHit.collider != null)
        {
           if (rayHit.collider.gameObject.tag.Equals("Loot"))
            {
                Debug.Log("Found some loot!");
                rayHit.collider.gameObject.SendMessage("rollAnUpgrade");
            }
            else if (rayHit.collider.gameObject.tag.Equals("Encounter"))
            {
                //Open door, move forward, move forward, fan party
                startMoveCooldown();             
                SpawnEncounter();
               // pos += Vector2.up; //need to be relative forward direction
                return rayHit;
            }
          
            else if (rayHit.collider.gameObject.tag.Equals("LevelClear"))
            {
                Debug.Log("The Floor was cleared!");
            }
            return rayHit;
        }
        else return rayHit;
    }

    //returns an approximation of room area
    private void surveyRoomEntered()
    {

        Vector2 buddy1SpawnLocation = new Vector2(0,0);
        Vector2 buddy2SpawnLocation = new Vector2(0,0);
        buddy1SpawnLocation = this.transform.position;
        buddy2SpawnLocation = this.transform.position;

        RaycastHit2D rayHitUp = Physics2D.Raycast(this.gameObject.transform.position,Vector2.up);
        RaycastHit2D rayHitRight = Physics2D.Raycast(this.gameObject.transform.position, Vector2.right);
        RaycastHit2D rayHitLeft = Physics2D.Raycast(this.gameObject.transform.position,Vector2.left);
        RaycastHit2D rayHitDown = Physics2D.Raycast(this.gameObject.transform.position, Vector2.down); 
        if(rayHitUp.collider!=null){
            Debug.Log(this.gameObject.name + "Looking +y I see this many units: "+rayHitUp.distance);
           // Debug.DrawRay(transform.position, (Vector2)transform.position - Vector2.up , Color.red, 20);
        }
        if (rayHitRight.collider != null)
        {
            Debug.Log(this.gameObject.name + "Looking x I see this many units: " + rayHitRight.distance);
           // Debug.DrawRay(transform.position, (Vector2)transform.position - Vector2.right, Color.red, 20);
        }
        if (rayHitLeft.collider != null)
        {
            Debug.Log(this.gameObject.name + "Looking -x I see this many units: " + rayHitLeft.distance);
            //Debug.DrawRay(transform.position, (Vector2)transform.position - Vector2.left, Color.red, 20);
        }
        if (rayHitDown.collider != null)
        {
            Debug.Log(this.gameObject.name + "Looking -y I see this many units: " + rayHitDown.distance);
           // Debug.DrawRay(transform.position, (Vector2)transform.position - Vector2.down, Color.red, 20);
        }
        float roomSizeApprox = (rayHitLeft.distance + rayHitRight.distance) * (rayHitUp.distance + rayHitDown.distance);

        int xSpawnOffset = ((int)rayHitUp.distance + (int)rayHitDown.distance) / 2;
        int ySpawnOffset = ((int)rayHitLeft.distance + (int)rayHitRight.distance )/ 2;
        
        int xToSpawn = (int) transform.position.x;
        int yToSpawn = (int)transform.position.y;

        //Figure out the likely center of the room by judging the greater of the returned distances in direction
        if (this.gameObject.transform.eulerAngles.z == 270)//if facing right
        {
            buddy1SpawnLocation += Vector2.left;
            buddy2SpawnLocation += Vector2.left * 2;
            xToSpawn += xSpawnOffset;
            //Find the greater of y options, and take that y as positive or negative direction
            if(rayHitUp.distance >= rayHitDown.distance){
                yToSpawn += ySpawnOffset;
               
            }
            else
            {
                yToSpawn -= ySpawnOffset;
            }
        }
        if (this.gameObject.transform.eulerAngles.z == 180) //if facing down
        {
            buddy1SpawnLocation += Vector2.up;
            buddy2SpawnLocation += Vector2.up * 2;
            yToSpawn -= ySpawnOffset;
            if(rayHitLeft.distance >= rayHitRight.distance){
                xToSpawn -= xSpawnOffset;
            }
            else
            {
                //Vector2.left;
                xToSpawn += xSpawnOffset;
            }
        }
        if (this.gameObject.transform.eulerAngles.z == 90) //if facing left
        {
            buddy1SpawnLocation += Vector2.right;
            buddy2SpawnLocation += Vector2.right * 2;
            xToSpawn -= xSpawnOffset;
            //Find the greater of y options, and take that y as positive or negative direction
            if (rayHitUp.distance >= rayHitDown.distance)
            {
                yToSpawn += ySpawnOffset;
            }
            else
            {
                yToSpawn -= ySpawnOffset;
            }
        }
        if (this.gameObject.transform.eulerAngles.z == 0) //if facing up
        {
            buddy1SpawnLocation += Vector2.down;
            buddy2SpawnLocation += Vector2.down * 2;
            yToSpawn += ySpawnOffset;
            if (rayHitLeft.distance >= rayHitRight.distance)
            {
                xToSpawn -= xSpawnOffset;
            }
            else
            {
                xToSpawn += xSpawnOffset;
            }
        }

      //  Debug.Log("Player survey thought here was good"+xToSpawn + " " +yToSpawn);

        turnManager.GetComponent<TurnManager>().Begin(xToSpawn,yToSpawn, buddy1SpawnLocation, buddy2SpawnLocation); 

      //  Debug.Log(this.gameObject.name + " I figure the room is about size: "+ roomSizeApprox);

    }

   /* RaycastHit2D rayCheckLine(int L){
        return Physics2D.Linecast(tf.position,(Vector2)(pos+(Vector2)(tf.up)),L);
	}
    */
	//Input Handling
	void checkPlayerMovement(){
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
      //  Debug.Log("test");
		if (Input.GetButton("Horizontal") && !moving) {  
			if (h>0) {	
				tf.localEulerAngles = new Vector3 (0, 0, 270);   // Always rotate the character even if you cannot move
				rayCast = rayCheckLine (BUMP_DIST);
				if (rayCast.collider == null || rayCast.collider.tag.Equals("Encounter")) {
					pos += Vector2.right;
					startMoveCooldown ();
				} else {
				//	Debug.Log ("Move right failed:" + rayCast.collider.gameObject.name + " in the way.");
				}
			} else {
				
				tf.localEulerAngles = new Vector3 (0, 0, 90);
				rayCast = rayCheckLine (BUMP_DIST);
                if (rayCast.collider == null || rayCast.collider.tag.Equals("Encounter"))
                {	
					pos += Vector2.left;
					//anim.SetTrigger ("Walking");
					startMoveCooldown ();
				
				} else {
				//	Debug.Log ("Move left failed:" + rayCast.collider.gameObject.name  + " in the way.");
				}
			}
		}
		else if (Input.GetButton("Vertical")&& !moving) {
			if (v>0)
			{	
				tf.localEulerAngles = new Vector3 (0, 0, 0);
				rayCast = rayCheckLine (BUMP_DIST);
                if (rayCast.collider == null || rayCast.collider.tag.Equals("Encounter"))
                {	
					pos += Vector2.up;
					//anim.SetTrigger("Walking");
					startMoveCooldown ();
				} else {
				Debug.Log ("Move up failed:" + rayCast.collider.gameObject.name  + " in the way.");				
				}
			}
			else
			{
				
				tf.localEulerAngles = new Vector3 (0, 0, 180);
				rayCast = rayCheckLine (BUMP_DIST);
                if (rayCast.collider == null || rayCast.collider.tag.Equals("Encounter"))
                {	
					pos += Vector2.down;
					//anim.SetTrigger ("Walking");
					startMoveCooldown ();
				} else {
				//	Debug.Log("Move down failed:" + rayCast.collider.gameObject.name + " in the way.");
				}
			}
		}
	}
}