using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


[System.Serializable]
public class BasicAttack : AttackTemplate {
	public static GameObject origin;
	private const int NUM_DIMENSIONS = 1, LENGTH = 1;
	private List<int> dimensions;
	public Vector3 pos;
	private Transform startTransform;
	//private List<GameObject> validTargets;
	private RaycastHit2D[] allTargets;


	void Start(){
		//executor = gameObject;
		//origin=  GameObject.Find ("P1");
		origin=this.gameObject;

		Debug.Log ("Called start on origin " +origin.GetComponent<Collider2D>());
		if (origin == null) {

			Debug.Log ("the origin game object was not supposed to be null");
		}

		startTransform = origin.GetComponent<Transform>();

		//pos = startTransform.position;
		if (startTransform == null) {

			Debug.Log ("the start transform was not supposed null");
		}
		//dimensions = new List<int>();
		//dimensions.Capacity = NUM_DIMENSIONS;
		//dimensions.Add (1);

		//validTargets = new List<GameObject> ();

	}
	//public enum BasicAttacks{

		//WAR1= ,
		//GRASS=1,
		//SAND = 2,
	//}

	//public List<GameObject> CheckLine(){
	public List<RaycastHit2D> CheckLine(){
		if (startTransform == null) {
			Debug.Log ("The start transform when checking line is null");
		}

		List<RaycastHit2D> toRet = new List<RaycastHit2D>();

//Debug.DrawRay ((Vector2)pos, (Vector2)pos+(Vector2)((startTransform.up) * dimensions[LENGTH]),Color.green);	
		//allTargets = (Physics2D.RaycastAll ((Vector2)pos, (Vector2)pos+(Vector2)startTransform.up*dimensions[LENGTH]));

		allTargets = Physics2D.RaycastAll ((Vector2)startTransform.position, (Vector2)startTransform.position+Vector2.up,3f); ///3f is hard coded for testing!!!!!!!!!!!!!
		foreach (RaycastHit2D hit in allTargets) {
			if (hit.collider.tag=="Baddie") {
				toRet.Add (hit);
				//validTargets.Add (hit.collider.gameObject);
				//Debug.Log (hit.collider.ToString ());

			}
		}
		return toRet;
}

	//Ray Shape Type

	public BasicAttack(){
		Debug.Log ("Creating atk");

	}

	/*new public List<GameObject>  FindTargets(){
		return CheckLine ();
	}*/

}
