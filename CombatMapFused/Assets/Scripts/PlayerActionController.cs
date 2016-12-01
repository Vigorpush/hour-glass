using UnityEngine;
using System.Collections;

public class PlayerActionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void handleInput() {
		if (Input.GetKeyDown("fire1"))
		{
			this.SendMessage ("Attack1");


			//Debug.Log(getcurrentHP());
		}


	}

}
