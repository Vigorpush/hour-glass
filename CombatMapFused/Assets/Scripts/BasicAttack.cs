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
		
		origin=  GameObject.Find ("Player1");
		//attempts
		//startTransform=this.GetComponentInParent<Transform>();
		////executor =  GameObject.Find ("Player1");
		startTransform = origin.transform;

		Debug.Log ("!!!!"+origin.name + origin.transform.position);
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
		origin=  GameObject.Find ("Player1");
		startTransform = origin.transform;
		
		if (startTransform == null) {
			Debug.Log ("The start transform when checking line is null");
		}

		Debug.Log ("Swing");
		List<RaycastHit2D> toRet = new List<RaycastHit2D>();

		Debug.DrawRay ((Vector2)startTransform.position, (Vector2)startTransform.position+(Vector2)((startTransform.up) * 3f),Color.green);	
		//allTargets = (Physics2D.RaycastAll ((Vector2)pos, (Vector2)pos+(Vector2)startTransform.up*dimensions[LENGTH]));

		allTargets = Physics2D.RaycastAll ((Vector2)startTransform.position, (Vector2)startTransform.position+Vector2.up,3f); ///3f is hard coded for testing!!!!!!!!!!!!!
		foreach (RaycastHit2D hit in allTargets) {
			if (hit.collider.tag=="Baddie") {
				Debug.Log ("There is a bad guy infront of me to hit");
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
