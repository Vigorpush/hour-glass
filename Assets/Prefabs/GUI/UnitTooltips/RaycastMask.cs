using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RaycastMask : MonoBehaviour {

	private Slider mainhourglass;
	private RectTransform fil;
	// Use this for initialization
	void Start () {
		mainhourglass = this.gameObject.GetComponent<Slider> ();
		fil = mainhourglass.fillRect;
		Debug.Log (mainhourglass.colors);	
	}


	void drawHourglass(){
		
	}
	// Update is called once per frame
	void Update () {
	
	}
}
