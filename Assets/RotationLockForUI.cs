using UnityEngine;
using System.Collections;

public class RotationLockForUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
         this.transform.rotation = Quaternion.identity;
    }

}
