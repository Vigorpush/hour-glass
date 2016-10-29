using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineRayCast  {
	
	private const int NUM_DIMENSIONS = 1, LENGTH = 0;
	//0 = length
	private List<int> dimensions;
	private GameObject start;
	private Transform startTransform;
	private List<GameObject> validTargets;
	private RaycastHit2D[] allTargets;
	LineRayCast(GameObject origin,int length){

		dimensions.Capacity = NUM_DIMENSIONS;
		dimensions.Add (length);
		start = origin;
		validTargets = new List<GameObject> ();
	

	}

	// Use this for initialization

	public List<GameObject> CheckLine(){
		Vector3 pos = startTransform.position;
	
		allTargets = (Physics2D.RaycastAll ((Vector2)pos, (Vector2)pos+(Vector2)startTransform.up*dimensions[LENGTH]));
		foreach (RaycastHit2D hit in allTargets) {
			if (hit.collider.tag=="Baddie") {
				validTargets.Add (hit.collider.gameObject);
				Debug.Log (hit.collider.ToString ());
			}

		}
		return validTargets;
	}


	//Cone 
	//Square
	//circle
	//Donut

	// Update is called once per frame
}
