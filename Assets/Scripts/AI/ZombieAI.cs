using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour {

    //The zombies transform
    private Transform myTf;
    RaycastHit2D rayCast;
    const int BUMP_DIST = 1; // The range that will check for movement collisions
    private Vector2 pos;
    public GameObject myTargetTile;
    public GameObject myTargetToAttack;
    private EnemyUnit myStats;
    private int myMovement;
    private float speed = 5f;
    private int movesLeft;
    private bool isStillTurn;
    private bool movementCooldown;
    private bool dying;
    public float BUFFER_TIME = 0.2f;
    GameObject turnManager;
    Collider2D myTarget;
    // Use this for initialization
    void Start()
    {
        turnManager = GameObject.Find("Manager");
        dying = false;
       //BuildDecisionTree();
        myTf = GetComponent<Transform>();
        pos = myTf.position;
        myStats = GetComponent<EnemyUnit>();
        myMovement = myStats.speed;
        movesLeft = myMovement;
        isStillTurn = false;
        movementCooldown = false;
    }

    public void amDying()
    {
        dying = true;
    }

    public RaycastHit2D whatIsInfrontOfMe()
    {
        rayCast = rayCheckLine(BUMP_DIST);
        return rayCast;
    }

    public void MoveX(int xToMove)
    {
        if (xToMove > 0) { 
            pos += Vector2.right;
            startMoveCooldown();
        }
        else
        {
           pos += Vector2.left;
           startMoveCooldown();
        }
    }

    public void MoveY(int yToMove)
    {
        if (yToMove > 0)
        {
            pos += Vector2.up;
            startMoveCooldown();
        }
        else
        {
            pos += Vector2.down;
            startMoveCooldown();
        }
    }

    private void startMoveCooldown()
    {
        //Does nothing possibly
    }

    private void unsetMovementCooldown()
    {
        movementCooldown = false;
    }

    RaycastHit2D rayCheckLine(int L)
    {
        return Physics2D.Linecast(myTf.position, (Vector2)(pos + (Vector2)(myTf.up)), L);
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

     private void BuildDecisionTree()
     {
        
    }

    void FixedUpdate()
    {     
     //   root.Search(root);
        if(isStillTurn && !movementCooldown){
            myTf.position = Vector3.MoveTowards(myTf.position, pos, Time.deltaTime * speed * 1/BUFFER_TIME);
        }

    }

    private void moveBuffer()
    {    
        //Debug.Log("mvoe buffer was invoked and "+movesLeft+" moves are left, and it is still turn: "+isStillTurn + "I am" + gameObject.name);
        MoveTowardsTarget();      
    }

    private void MoveTowardsTarget()
    {
        //isStillTurn = true;
        if (movesLeft<=0)
        {
            Debug.Log(this.gameObject.name + "Finished Turn");
            turnManager.SendMessage("CalculateTurn");
        }
            //can make a move
        else 
        {
            unsetMovementCooldown();

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
       
                            // movementCooldown = true;
                            Invoke("moveBuffer", BUFFER_TIME);
                        }
                        else
                        {
                           // Debug.Log("Trying to  go left");
                            MoveX(-1);
                            distanceToMoveX++;
                         //   Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                            // movementCooldown = true;
                            Invoke("moveBuffer", BUFFER_TIME);
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
                            // movementCooldown = true;
                            Invoke("moveBuffer", BUFFER_TIME);
                        }
                        else
                        {

                            MoveY(-1);
                            distanceToMoveY++;
                          //  Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                            // movementCooldown = true;
                            Invoke("moveBuffer", BUFFER_TIME);
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
                        // movementCooldown = true;
                        Invoke("moveBuffer", BUFFER_TIME);
                    }
                    else
                    {
                        MoveX(-1);
                        distanceToMoveX++;
                       // Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                        // movementCooldown = true;
                        Invoke("moveBuffer", BUFFER_TIME);
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
                        Invoke("moveBuffer", BUFFER_TIME);
                    }
                    else
                    {
                        MoveY(-1);
                        distanceToMoveY++;
                       // Debug.Log(movesLeft + "/" + myMovement + " making a move!");
                        //movementCooldown = true;
                        Invoke("moveBuffer", BUFFER_TIME);
                    }
                }
                else
                {

                  //  Debug.Log(this.gameObject.name + "arrived at target");
                    RaycastHit2D myTargetInRange = scanForTargetRange(1);

                    if(myTargetInRange){
                        //Swing at enemy only in direction up!!!
                        myTargetToAttack.GetComponent<HeroUnit>().takeDamage(7,this.gameObject);
                    }

                    turnManager = GameObject.Find("Manager");
                    turnManager.SendMessage("CalculateTurn");
                }
        }  
    }

    private void resetMovement()
    {
        movesLeft = GetComponent<EnemyUnit>().speed;
    }

    public void StartTurn()
    {
        if (dying)
        {
            Debug.Log(this.gameObject.name + "was lingering after death, passing Turn");
            turnManager = GameObject.Find("Manager");
            turnManager.SendMessage("CalculateTurn");
        }
        else
        {
            isStillTurn = true;
            resetMovement();
            DecideOnTarget();
            DecideWhoToAttack();
            movementCooldown = false;
            //myTarget = myTargetTile.GetComponent<Collider2D>();

            MoveTowardsTarget();
        }   
    }

    private RaycastHit2D scanForTargetRange(int L){

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

}

