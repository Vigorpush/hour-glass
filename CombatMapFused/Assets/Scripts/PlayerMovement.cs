using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	const int BUMP_DIST = 1; // The range that will check for movement collisions
	private Transform tf;
	private float speed;
	private Vector2 pos;
	public float h,v;
	public Vector2 initialPosition;
	RaycastHit2D rayCast;
	private bool moving;

	private Animator anim;

	void movingOn(){
		moving = true;
	}
	void movingOff(){
		moving = false;
	}

	void startMoveCooldown(){
		movingOn ();
		Invoke ("movingOff", 1f/ speed);

	}
	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
		speed = 4;
		tf.position = initialPosition;
		anim = GetComponent<Animator>();
	}
	//Checks for collisions with units in a line of length L

	//RAY CAST METHODS
	RaycastHit2D rayCheckLine(int L){
		return Physics2D.Linecast(pos,(Vector2)(pos+(Vector2)(tf.up)),L);
	}

	// Update is called once per phys tick
	void FixedUpdate () {
		
		checkPlayerMovement ();
		transform.position = Vector3.MoveTowards(tf.position, pos, Time.deltaTime * speed);
		Debug.DrawRay (pos, (Vector2)(tf.up),Color.blue);
	
		}
		

	void checkPlayerMovement(){
		
		//

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
			else if (Input.GetKeyDown("down"))
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