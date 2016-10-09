using UnityEngine;
using System.Collections;

public class GUI_Combat : MonoBehaviour {

	public Rect windowRect = new Rect (20,20,Screen.height,Screen.width);

	void OnGUI(){

		windowRect = GUILayout.Window (0, windowRect, DoMywindow,"My Game");

		//GUI.Button (new Rect (25,25,100,30), "I am a fixed Layout Button");

		//GUILayout.Button ("I am an Automatic");
	}

	void DoMywindow(int windowID){
		if (GUILayout.Button("Hello World"))
			
				print("Hello");
		}
}
		