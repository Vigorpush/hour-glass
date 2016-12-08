using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour {
	protected Quaternion UP = Quaternion.AngleAxis(0, Vector3.forward);
	protected Quaternion RIGHT = Quaternion.AngleAxis(270, Vector3.forward);
	protected Quaternion DOWN = Quaternion.AngleAxis(180, Vector3.forward);
	protected Quaternion LEFT = Quaternion.AngleAxis(90, Vector3.forward);

    //The zombies transform
	public bool firstMove=true;
	protected float END_TURN_BUFFER= 0.3f;
	public float SPEED_COEF = .1f;
	protected Transform myTf;
    RaycastHit2D rayCast;
    const int BUMP_DIST = 1; // The range that will check for movement collisions
	protected Vector2 desiredCoord;
	public Vector3 moveTarget;
    public GameObject attackTarget;

	protected EnemyUnit myStats;
	protected int myMovement;
	public float speed;
	protected int movesLeft;
    public bool isStillTurn = true;
	protected DecisionTree currentBehaviour;
	protected bool dying;
    //public float BUFFER_TIME = 0.5f;
    GameObject turnManager;
    Collider2D myTarget;
	protected bool movementCooldown = false;
	protected ArrayList players;
	public int attacksLeft;
	protected DecisionTree mostBasicTree; // Tree is saved to save time
	protected AudioSource[] sources;

    // Use this for initialization
	void Awake(){
		sources = gameObject.GetComponents<AudioSource> ();
		GetAllPlayers ();
		turnManager = GameObject.Find ("Manager");
		myTf = GetComponent<Transform>();
        myStats = GetComponent<EnemyUnit>();
	}
    void Start()
    {
        //TODO: restart turn function that cleans this up a lot
		//Debug.Log("Started");
		if (firstMove) {
			GetAllPlayers ();
			turnManager = GameObject.Find ("Manager");
			dying = false;
			attacksLeft = 1;
			//BuildZombieTree();
			myTf = GetComponent<Transform> ();
			desiredCoord = myTf.position;
			myStats = GetComponent<EnemyUnit> ();
            myMovement = myStats.speed;
			speed = ((float)myStats.speed) * SPEED_COEF;
			movesLeft = myMovement;
            firstMove = false; 
          
			//Debug.Log (speed + " " + myStats.speed + " " + SPEED_COEF);

			movementCooldown = false;
		}
		MakeMove ();
    }
	void OnEnable(){
        
        isStillTurn = true;
		attacksLeft = 1; 
		desiredCoord = myTf.position;
		MakeMove();
		Debug.Log (this.gameObject.name + ": Beginning turn");
	}


	//UTIL
    public void AmDying()
    {
        dying = true;
    }


    public RaycastHit2D WhatIsInfrontOfMe()
    {
		rayCast = ScanLineFacing(BUMP_DIST);
        return rayCast;
    }
	//TODO: save cycles by having battle master do this for all units
	void GetAllPlayers ()
	{
		players = new ArrayList ();

		players.Add (GameObject.FindGameObjectWithTag ("Player1"));
		players.Add (GameObject.FindGameObjectWithTag ("Player2"));
		players.Add (GameObject.FindGameObjectWithTag ("Player3"));

	}

    public void MoveX(int xToMove)
    {
        if (xToMove > 0) { 
            desiredCoord += Vector2.right;
            myTf.rotation = RIGHT;
        }
        else
        {
           desiredCoord += Vector2.left;
           myTf.rotation = LEFT;
        }
    }
    public void MoveY(int yToMove)
    {
        if (yToMove > 0)
        {
            
            desiredCoord += Vector2.up;
            myTf.rotation = UP;
        }
        else
        {
           desiredCoord += Vector2.down;
           myTf.rotation = DOWN;
        }
    }
	protected void MoveBuffer()
	{    
		Debug.Log("mvoe buffer was invoked and "+movesLeft+" moves are left, and it is still turn: "+isStillTurn + "I am" + gameObject.name);
		StepTowardsTarget();      
	}
	protected bool atMoveTarget (){
		if (moveTarget == null) {
			Debug.Log ("oh no don't know what move target is so cannot say im there or not. true i guess....");
			return true;
		}
		return myTf.position == moveTarget;
	} 
	protected void UnsetMovementCooldown(){
		movementCooldown = false;
	}
		
    //Random
    protected bool CoinFlip()
    {
         int coinFlip = UnityEngine.Random.Range(0, 3);
         if (coinFlip == 1)
         {
             return true;
         }
         else return false;
     }
	//TREE STUFF
	protected void NavigateTree(){
		currentBehaviour.Search(currentBehaviour);

	}

	protected void ChooseTree(){
		currentBehaviour = BuildZombieTree ();
	     
	}
	//ALL TREES

	protected DecisionTree BuildZombieTree()
	{  //save cycMles if ya can
		if (mostBasicTree != null) {
			return mostBasicTree;

		}
		DecisionTree canMove = new DecisionTree ();
		canMove.setDecision (canMoveTowardTarget);
		DecisionTree tryAttack = new DecisionTree ();
		tryAttack.setAction (tryMeleeAttackDumb);
		DecisionTree doNothing = new DecisionTree ();
		doNothing.setAction (StayStill);
		canMove.addChild (doNothing); // First child = offset 0 = false

		canMove.addChild (tryAttack); // True



		return canMove;
    }

    void FixedUpdate()
    {     

     //   root.Search(root);
		//Debug.Log ("Movvement cd : " +movementCooldown.ToString());
		//Debug.Log (isStillTurn);
      //  Debug.Log(this.gameObject.name + " I have this many moves left: " + movesLeft);
		//Debug.Log(desiredCoord.ToString());
        if(isStillTurn && !movementCooldown){
			//Debug.Log ("I want to go to " + desiredCoord.ToString ());
			//Debug.Log ("I am at: " + myTf.position);
			myTf.position = Vector2.MoveTowards(myTf.position, desiredCoord, Time.fixedDeltaTime * speed);

        }
		if (((Vector2)myTf.position).Equals(desiredCoord)) {
			//Debug.Log ("GOOOOOOOOOOOOOOOOOOOD");
			if (movesLeft > 0) {
				StepTowardsTarget ();
			} else if (attacksLeft > 0 && targetIsAdjacent() && atMoveTarget()) {
				
					ExecuteMeleeAttackOnTarget();

				}
            else if (movesLeft <= 0 && isStillTurn)
            {

                isStillTurn = false;
                
                    EndMyTurn();
                
            }


		} 

	}

    

	public void MakeMove(){

        resetMovement();
		DecideAttackTarget ();
		DecideTileTarget ();
		ChooseTree ();
		NavigateTree ();
		//Debug.Log(this.gameObject.name + "Finished Turn");
		if((Vector2)myTf.position != desiredCoord){
			//Debug.Log("not where I want to be");
		}



	}
	public bool ScanNearbyForTarget(){
		for (int i = 0; i < 4; i++) {
           // Debug.Log(myStats.range);
			RaycastHit2D result = ScanLineFacing (myStats.range);
            
			if (result.collider != null) {
				if (result.collider.gameObject == attackTarget) {
					return true;
				}
				myTf.Rotate (new Vector3 (0, 0, 90));


			}
		}
		return false;
	}
	//MOVEMENT
	void StayStill(){
		EndMyTurn ();
	}

	int canMoveTowardTarget(){
		return 1;

	}

    protected void StepTowardsTarget()
        
    {
		if (moveTarget == null) {
			DecideTileTarget ();
		}
        //isStillTurn = true;
        if (movesLeft<=0)
        {
			return;
           
        }
            //can make a move
        else 
        {
			//Debug.Log(this.gameObject.name + " I have this many moves left: " + movesLeft);

			UnsetMovementCooldown ();
            //Decide on a target and set myDecidedTarget
            //myDecidedTarget = someSmartmethod()....
            
           

			int distanceToMoveX = (int)(moveTarget.x - myTf.position.x);
			int distanceToMoveY = (int)(moveTarget.y - myTf.position.y);
          //  Debug.Log("Dist to move :" + distanceToMoveX + "," + distanceToMoveY);
            bool Right = distanceToMoveX > 0f;
            bool Down = distanceToMoveY > 0f;
        
                movesLeft--;
			/*
                if (ScanNearbyForTarget())
                {
                    //A target
                    movesLeft = 0;
                    ExecuteMeleeAttackOnTarget();
                    return;
                }
			*/
                if (System.Math.Abs(distanceToMoveX) >0 && System.Math.Abs(distanceToMoveY) > 0)
                {
                    //50%/50% roll for direction
                    if (CoinFlip())
                    {
                        //MOVING X
                        if (Right)
                        {
                            MoveX(1);
                            distanceToMoveX--;
                          //  Debug.Log(movesLeft + "/"+ myMovement + " making a move!");
       
                          //  movementCooldown = true;
                            //Invoke("MoveBuffer", BUFFER_TIME);
                        }
                        else
                        {
                           // Debug.Log("Trying to  go left");
                            MoveX(-1);
                            distanceToMoveX++;
                         //   Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                            // movementCooldown = true;
							
                            //Invoke("MoveBuffer", BUFFER_TIME);
                        }

                    }
                    else
                    {
                        //MOVING Y
                        if (Down)
                        {
                            MoveY(1);
                            distanceToMoveY--;
                         //  Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                            //movementCooldown = true;
                            //Invoke("MoveBuffer", BUFFER_TIME);
                        }
                        else
                        {

                            MoveY(-1);
                            distanceToMoveY++;
                        //  Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                            // movementCooldown = true;
                            //Invoke("MoveBuffer", BUFFER_TIME);
                        }
                    }
                }
                else if (System.Math.Abs(distanceToMoveX) > 0)
                {

                    if (Right)
                    {
                        MoveX(1);
                        distanceToMoveX--;
                     //  Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                       //  movementCooldown = true;
                        //Invoke("MoveBuffer", BUFFER_TIME);
                    }
                    else
                    {
                        MoveX(-1);
                        distanceToMoveX++;
                       // Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                        // movementCooldown = true;
                        //Invoke("MoveBuffer", BUFFER_TIME);
                    }
                }
                else if (System.Math.Abs(distanceToMoveY) > 0)
                {
                    if (Down)
                    {
                        MoveY(1);
                        distanceToMoveY--;
                     //  Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                        // movementCooldown = true;
                        //Invoke("MoveBuffer", BUFFER_TIME);
                    }
                    else
                    {
                        MoveY(-1);
                        distanceToMoveY++;
                      //  Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                      //  movementCooldown = true;
                       // Invoke("MoveBuffer", BUFFER_TIME);
                    }
                }
                else
                {

                Debug.Log(this.gameObject.name + "arrived at target");
				//RaycastHit2D myTargetInRange = ScanLineFacing(1);

				if(ScanNearbyForTarget()){
                        //Swing at enemy only in direction up!!!
					ExecuteMeleeAttackOnTarget();
                    }

                    turnManager = GameObject.Find("Manager");
				//Invoke ("EndMyTurn", END_TURN_BUFFER);

					//enabled = false; //Disable this script to free up the fixed update loop
                }
        }  
    }

    //Look at target

    protected void FaceTarget()
    {
        Vector3 theDirection = attackTarget.transform.position - this.gameObject.transform.position ;
        int xDiff = (int)theDirection.x;
        int yDiff = (int)theDirection.y;
        if (xDiff == 0 && yDiff == 0)
        {
            Debug.Log("bad programming, inside a guy lol");

        }

        if(xDiff>=yDiff){
            if (xDiff > 0)
            {
                //Face Right
                myTf.rotation = RIGHT;

            }
            else //x Diff <0
            {
                //Face left
                myTf.rotation = DOWN;
            }          
        }
        else //yDiff was greater than xDiff
        {
            if (yDiff > 0)
            {
                //Face Up
                myTf.rotation = UP;
            }
            else //yDiff <0
            {
                //Face Down
                myTf.rotation = LEFT;
            }
        }
        
    }

	protected void ExecuteMeleeAttackOnTarget(){
        /*myTf.LookAt(attackTarget.transform);
        myTf.RotateAround(transform.position, new Vector3 (0,0,1), 90);*/
        FaceTarget();

		if (attackTarget == null) {
			Debug.Log ("NO TARGET");
			return;
		}
        //Attack Target. magic number 1 for combat text
        attackTarget.GetComponent<HeroUnit>().takeDamage(myStats.attack, this.gameObject);
		playAttackAudio ();
		attacksLeft--;
	}
	void playAttackAudio(){
		//PLAY ATTACK AUDIO
		sources [1].Play ();
	}
	void tryMeleeAttackDumb(){
		StepTowardsTarget ();
	}
    protected void resetMovement()
    {
        movesLeft = GetComponent<EnemyUnit>().speed;
    }

    protected RaycastHit2D ScanLineFacing(int L){
        
        //MAKE SURE YOU FACE THE TARGET FIRST, maybe looktowards
        
	//	Debug.DrawLine(myTf.position,(Vector2)(desiredCoord+(Vector2)(myTf.up)));
        return Physics2D.Linecast(myTf.position,(Vector2)(desiredCoord+(Vector2)(myTf.up)),L);
    }
	protected GameObject getClosestPlayer(){
		if (players == null) {
			GetAllPlayers ();
		}
		GameObject closest = null;
		float closestDist = float.MaxValue;
        foreach (GameObject p in players) {
			if (p.activeSelf && p.GetComponent<HeroUnit>().curhp > 0 ) { 
            float dist = Vector2.Distance(desiredCoord, p.transform.position);
            Debug.Log("Distance between me and cur player: " + dist);
            if (dist < closestDist) {
                closestDist = dist;
                closest = p;
            }
        }
		}
		return closest;

	}
	protected bool targetIsAdjacent (){
		if (attackTarget == null) {
			//throw new UnityException ("Don't know target!");
			return false;

		}
		//if (tileEmpty (tar + new Vector3 (-1, 0, 0))) {
			//			moveTarget = tar + new Vector3 (-1,0 , 0);
			//			return;
			//		}
			//		if (tileEmpty(tar + new Vector3 (0, 1, 0))) {
			//			moveTarget = tar + new Vector3 (0, 1, 0);
			//			return;
			//		}
			//		if (tileEmpty(tar + new Vector3 (0, -1, 0))) {
			//			moveTarget = tar + new Vector3 (0, -1, 0);
			//			return;
			//		}
			//		if (tileEmpty (tar + new Vector3 (1, 0, 0))) {
			//			moveTarget = tar + new Vector3 (1, 0, 0);
			//			return;
			//		}
			if (Vector2.Distance (myTf.position, attackTarget.transform.position) < 1.1f) {
				Debug.Log ("My target is adjacent to me!");
				return true;
			}
			return false;


	}
    // quasi delegate AI stubs
    protected void DecideTileTarget()
    {
		if (attackTarget == null) {
			Debug.Log("NO TARGET SO NO PLACE TO MOVE!!!!!");
			return;
		}
		//TEMP SOLUTION CAUSE NO MAP...

		//moveTarget = 
//		Debug.Log(attackTarget.transform.GetChild (0));
//		moveTarget = attackTarget.transform.GetChild (0).gameObject;
//
//		Debug.Log (moveTarget.gameObject.name);
//		//attackTarget
		//Look for an empty side
		
		//If no empty then change target or stack?
		//switch statement producese more believable selection	
		Vector3 tar =attackTarget.transform.position;
		//Debug.Log("My target is at " + attackTarget.transform.position.ToString() );
		//switch (Random.Range (1, 4)){

		//case 1: 
		if (tileEmpty (new Vector3 (tar.x + 1, tar.y, 0))) {
			moveTarget = new Vector3 (tar.x + 1, tar.y, 0);
			Debug.Log("Going to target's right");
			return;
		} else {
			Debug.Log("The tile is occupied!");
			moveTarget = attackTarget.transform.position;
		}
		/*
		if (tileEmpty(tar + new Vector3 (-1, 0, 0))) {
			moveTarget = tar + new Vector3 (-1,0 , 0);
			return;
		}
		if (tileEmpty(tar + new Vector3 (0, 1, 0))) {
			moveTarget = tar + new Vector3 (0, 1, 0);
			return;
		}
		if (tileEmpty(tar + new Vector3 (0, -1, 0))) {
			moveTarget = tar + new Vector3 (0, -1, 0);
			return;
		}
//			break;
//		*/
//
//		case 2: 
//
//
//		if (tileEmpty(tar + new Vector3 (-1, 0, 0))) {
//			moveTarget = tar + new Vector3 (-1,0 , 0);
//			return;
//		}
//		if (tileEmpty(tar + new Vector3 (0, 1, 0))) {
//			moveTarget = tar + new Vector3 (0, 1, 0);
//			return;
//		}
//		if (tileEmpty(tar + new Vector3 (0, -1, 0))) {
//			moveTarget = tar + new Vector3 (0, -1, 0);
//			return;
//		}
//		if (tileEmpty (tar + new Vector3 (1, 0, 0))) {
//			moveTarget = tar + new Vector3 (1, 0, 0);
//			return;
//		}
//		break;
//
//		case 3: 
//
//
//		
//			if (tileEmpty(tar + new Vector3 (0, 1, 0))) {
//				moveTarget = tar + new Vector3 (0, 1, 0);
//				return;
//			}
//			if (tileEmpty(tar + new Vector3 (0, -1, 0))) {
//				moveTarget = tar + new Vector3 (0, -1, 0);
//				return;
//			}
//			if (tileEmpty(tar + new Vector3 (-1, 0, 0))) {
//				moveTarget = tar + new Vector3 (-1,0 , 0);
//				return;
//			}
//			if (tileEmpty (tar + new Vector3 (1, 0, 0))) {
//				moveTarget = tar + new Vector3 (1, 0, 0);
//				return;
//			}
//			break;
//
//		case 4: 
//
//
//
//			if (tileEmpty(tar + new Vector3 (0, -1, 0))) {
//				moveTarget = tar + new Vector3 (0, -1, 0);
//				return;
//			}
//			if (tileEmpty (tar + new Vector3 (1, 0, 0))) {
//				moveTarget = tar + new Vector3 (1, 0, 0);
//				return;
//			}
//			if (tileEmpty(tar + new Vector3 (0, 1, 0))) {
//				moveTarget = tar + new Vector3 (0, 1, 0);
//				return;
//			}
//			if (tileEmpty(tar + new Vector3 (-1, 0, 0))) {
//				moveTarget = tar + new Vector3 (-1,0 , 0);
//				return;
//			}
//
//			break;


	//}

    }
	
	protected bool tileEmpty(Vector3 target){
		//new Vector3(0,0,1)
		Debug.DrawRay(target,new Vector3(target.x,target.y,-1));
		RaycastHit2D[]  hits= Physics2D.LinecastAll(target,new Vector3(target.x,target.y,-1));
		for (int i = 0; i < hits.Length; i++) {
			Debug.Log (hits [0].collider.gameObject.name);

		}
		if (hits.Length != 1) {
			return false;
		}
		return true;
	}

    protected void  DecideAttackTarget()
    {
		attackTarget =getClosestPlayer ();
		//While cannot target unit
		//if no closes
		if (attackTarget == null) {
			attackTarget = (GameObject)players [Random.Range (0, 2)];
			Debug.Log ("-----------------------Choosing a random target");
		}



    }



	protected void EndMyTurn(){
        
        
		//Debug.Log ("I want to end turn.");

		turnManager.SendMessage("AIDisableThenCalcTurn");
           
		this.enabled = false;


	}

}

