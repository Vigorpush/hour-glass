using UnityEngine;
using System.Collections;

public class FollowParent : MonoBehaviour {

	// Use this for initialization
	Transform parentPos;
	void Start(){
		parentPos = GameObject.FindGameObjectWithTag ("Player1").transform;
		//Debug.Log ("My daddy is:" + this.gameObject.GetComponentInParent<Transform> ().parent);
	}
	// Update is called once per frame
	void Update () {
		this.transform.position = parentPos.position;
	}
}
