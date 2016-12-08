using UnityEngine;
using System.Collections;

public class exit : MonoBehaviour {

	public GameObject pauseScreen;

	void Start(){
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyUp("escape")){
			pauseScreen.SetActive(true);  
        }
	}
}
