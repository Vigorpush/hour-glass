using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	const int BUMP_DIST = 1; // The range that will check for movement collisions
	private Transform tf;
	private float speed;
	public Vector2 pos;
	public float h,v;
	public Vector2 initialPosition = new Vector2(2,2);
	RaycastHit2D rayCast;
	private bool moving=false,isTurn=false;
	public GameObject turnManager;
	private Animator anim;




	// Use this for initialization
	void Start () {
		initializeController ();

	}
	// Update is called once per phys tick
	void FixedUpdate () {

		if (isTurn) {
			checkPlayerMovement ();
			tf.position = Vector3.MoveTowards (tf.position, pos, Time.deltaTime * speed);
			Debug.DrawRay (pos,(Vector2)(tf.up), Color.blue);

		}
	}
		
	//----------------------------------------------------//
	//HELPERS
	public void AllowMovement()
	{
		isTurn = true;
		Debug.Log ("is turn on");
	}
	void initializeController(){
		turnManager = GameObject.Find("Manager");
		tf = GetComponent<Transform>();
		tf.position = initialPosition;
		pos=tf.position;
		Debug.Log ("Initial postion" + tf.position);

		anim = GetComponent<Animator>();
		if (speed == null || speed==0) {
			speed = 4;
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
		Debug.Log ("moving on");
	}
	void movingOff(){
		Debug.Log ("moving off");
		moving = false;
	}
	//----------------------------------------------------//

	//Turn Management


	public void EndTurn(){
		isTurn = false;
		Debug.Log("endingturn");
		if (turnManager != null) {
			turnManager.SendMessage ("CalculateTurn");
		} else {

			Debug.Log ("No manager found");
		}
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
					Debug.Log ("Move right failed:" + rayCast.collider.gameObject + " in the way.");

				}
			} else{
				
				tf.localEulerAngles = new Vector3 (0, 0, 90);
				rayCast = rayCheckLine (BUMP_DIST);
				if (rayCast.collider == null) {	
					pos += Vector2.left;
					anim.SetTrigger ("Walk Left");
					startMoveCooldown ();
				
				} else {
					Debug.Log ("Move left failed:" + rayCast.collider.gameObject  + " in the way.");

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
					anim.SetTrigger("Walk Up");
					startMoveCooldown ();
				} else {
					Debug.Log ("Move up failed:" + rayCast.collider.gameObject  + " in the way.");
				
				}

			}
			else
			{
				
				tf.localEulerAngles = new Vector3 (0, 0, 180);
				rayCast = rayCheckLine (BUMP_DIST);
				if (rayCast.collider == null) {	
					pos += Vector2.down;
					anim.SetTrigger ("Walk Down");
					startMoveCooldown ();
				} else {
					Debug.Log("Move down failed:" + rayCast.collider.gameObject + " in the way.");
				}

			}

		}
	


	}

}