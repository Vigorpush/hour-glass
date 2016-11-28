using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour {

    //The zombies transform
	public bool firstMove=true;
	private float END_TURN_BUFFER= 0.5f;
	public float SPEED_COEF = 25f;
    private Transform myTf;
    RaycastHit2D rayCast;
    const int BUMP_DIST = 1; // The range that will check for movement collisions
    private Vector2 pos;
    public GameObject myTargetTile;
    public GameObject myTargetToAttack;
    private EnemyUnit myStats;
    private int myMovement;
	private float speed;
    private int movesLeft;
    private bool isStillTurn = false;
	private DecisionTree currentBehaviour;
    private bool dying;
    public float BUFFER_TIME = 0.2f;
    GameObject turnManager;
    Collider2D myTarget;
	private bool movementCooldown = false;

    // Use this for initialization
    void Start()
    {
		Debug.Log("STarted");
		if (firstMove) {
			turnManager = GameObject.Find ("Manager");
			dying = false;
			//BuildDecisionTree();
			myTf = GetComponent<Transform> ();
			pos = myTf.position;
			myStats = GetComponent<EnemyUnit> ();
			myMovement = myStats.speed;
			movesLeft = myMovement;
			SPEED_COEF = 25f;
			firstMove = false;
			speed = SPEED_COEF / myStats.speed;
			Debug.Log (speed + " " + myStats.speed + " " + SPEED_COEF);

			movementCooldown = false;
		}


    }

	void OnEnable(){
		Debug.Log (this.gameObject.name + ": Beginning turn");
		movementCooldown = false;
		Debug.Log (isStillTurn);
		isStillTurn = true;

		BeginTurn ();
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

    public void MoveX(int xToMove)
    {
        if (xToMove > 0) { 
            pos += Vector2.right;
          
        }
        else
        {
           pos += Vector2.left;

        }
    }
    public void MoveY(int yToMove)
    {
        if (yToMove > 0)
        {
            pos += Vector2.up;
           
        }
        else
        {
            pos += Vector2.down;
    
        }
    }
	private void MoveBuffer()
	{    
		//Debug.Log("mvoe buffer was invoked and "+movesLeft+" moves are left, and it is still turn: "+isStillTurn + "I am" + gameObject.name);
		MoveTowardsTarget();      
	}

	private void unsetMovementCooldown(){
		movementCooldown = false;
	}
		
    //Random
    private bool CoinFlip()
    {
         int coinFlip = UnityEngine.Random.Range(0, 3);
         if (coinFlip == 1)
         {
             return true;
         }
         else return false;
     }
	//TREE STUFF
	private void NavigateTree(){


	}
	private void ChooseTree(){
		currentBehaviour = BuildZombieTree ();
	}
	//ALL TREES
	private DecisionTree BuildZombieTree()
     {
		return new DecisionTree ();
    }

    void FixedUpdate()
    {     
     //   root.Search(root);
		//Debug.Log ("Movvement cd : " +movementCooldown.ToString());
		//Debug.Log (isStillTurn);
        if(isStillTurn && !movementCooldown){
			Debug.Log ("jfdksajksj");
            myTf.position = Vector3.MoveTowards(myTf.position, pos, Time.deltaTime * speed * 1/BUFFER_TIME);
        }

    }

 
	//MOVEMENT
    private void MoveTowardsTarget()
    {
        //isStillTurn = true;
        if (movesLeft<=0)
        {
            Debug.Log(this.gameObject.name + "Finished Turn");
			Invoke ("EndMyTurn", END_TURN_BUFFER);
			enabled = false; //Disable script to free fixedupdate loop
        }
            //can make a move
        else 
        {
			//Debug.Log(this.gameObject.name + " I have this many moves left: " + movesLeft); 
			unsetMovementCooldown ();
            //Decide on a target and set myDecidedTarget
            //myDecidedTarget = someSmartmethod()....
            Transform tarTf = myTargetTile.GetComponent<Transform>();
            myTf = GetComponent<Transform>();

            float distanceToMoveX = tarTf.position.x - myTf.position.x;
            float distanceToMoveY = tarTf.position.y - myTf.position.y;
           // Debug.Log("Dist to move :" + distanceToMoveX + "," + distanceToMoveY);
            bool Right = distanceToMoveX > 0f;
            bool Down = distanceToMoveY > 0f;
        
                movesLeft--;

                if (System.Math.Abs(distanceToMoveX) > 0.9 && System.Math.Abs(distanceToMoveY) > 0.9)
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
                            Invoke("MoveBuffer", BUFFER_TIME);
                        }
                        else
                        {
                           // Debug.Log("Trying to  go left");
                            MoveX(-1);
                            distanceToMoveX++;
                         //   Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                            // movementCooldown = true;
                            Invoke("MoveBuffer", BUFFER_TIME);
                        }

                    }
                    else
                    {
                        //MOVING Y
                        if (Down)
                        {
                            MoveY(1);
                            distanceToMoveY--;
                           // Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                            //movementCooldown = true;
                            Invoke("MoveBuffer", BUFFER_TIME);
                        }
                        else
                        {

                            MoveY(-1);
                            distanceToMoveY++;
                          //  Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                            // movementCooldown = true;
                            Invoke("MoveBuffer", BUFFER_TIME);
                        }
                    }
                }
                else if (System.Math.Abs(distanceToMoveX) > 0.9)
                {

                    if (Right)
                    {
                        MoveX(1);
                        distanceToMoveX--;
                       // Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                       //  movementCooldown = true;
                        Invoke("MoveBuffer", BUFFER_TIME);
                    }
                    else
                    {
                        MoveX(-1);
                        distanceToMoveX++;
                       // Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                        // movementCooldown = true;
                        Invoke("MoveBuffer", BUFFER_TIME);
                    }
                }
                else if (System.Math.Abs(distanceToMoveY) > 0.9)
                {
                    if (Down)
                    {
                        MoveY(1);
                        distanceToMoveY--;
                       // Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                        // movementCooldown = true;
                        Invoke("MoveBuffer", BUFFER_TIME);
                    }
                    else
                    {
                        MoveY(-1);
                        distanceToMoveY++;
                       // Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                      //  movementCooldown = true;
                        Invoke("MoveBuffer", BUFFER_TIME);
                    }
                }
                else
                {

                  //  Debug.Log(this.gameObject.name + "arrived at target");
				RaycastHit2D myTargetInRange = ScanLineFacing(1);

                    if(myTargetInRange){
                        //Swing at enemy only in direction up!!!
                        myTargetToAttack.GetComponent<HeroUnit>().takeDamage(7,this.gameObject);
                    }

                    turnManager = GameObject.Find("Manager");
				Invoke ("EndMyTurn", END_TURN_BUFFER);

					enabled = false; //Disable this script to free up the fixed update loop
                }
        }  
    }

    private void resetMovement()
    {
        movesLeft = GetComponent<EnemyUnit>().speed;
    }

    public void BeginTurn()
    {
        
        
            
		//Debug.Log(this.gameObject.name + " I am starting my turn");
            isStillTurn = true;
            resetMovement();
            DecideOnTarget();
            DecideWhoToAttack();
            //myTarget = myTargetTile.GetComponent<Collider2D>();
            MoveTowardsTarget();
         	
    }

    private RaycastHit2D ScanLineFacing(int L){

        //MAKE SURE YOU FACE THE TARGET FIRST, maybe looktowards

         return Physics2D.Linecast(myTf.position,(Vector2)(pos+(Vector2)(myTf.up)),L);
    }

    // quasi delegate AI stubs
    private GameObject DecideOnTarget()
    {
        return myTargetTile;
    }

    private GameObject DecideWhoToAttack()
    {
        return myTargetToAttack;
    }

	private void EndMyTurn(){
		turnManager.SendMessage("CalculateTurn");
	}

}

