using UnityEngine;
using System.Collections;

public class stat_enermy : MonoBehaviour {
	protected GameObject enermystatPanel;//accessing the Game object of the panel
	void Start(){
		enermystatPanel = GameObject.FindGameObjectWithTag("enermystat");
		enermystatPanel.gameObject.SetActive(false);
	}
	void Update(){
		if(Input.GetKeyUp(KeyCode.Tab)){
			closeWindow();
		}
		if(Input.GetKeyDown(KeyCode.Tab)){
			showWindow();
		}
	}
	void closeWindow(){
		enermystatPanel.gameObject.SetActive(false);
	}
	void showWindow(){	
		//refreshStat();
		enermystatPanel.gameObject.SetActive(true);
	}
	void refreshStat(){
		Debug.Log("Refresh Player Stat");
		//find this map
		//find all enemy in this map
		//show the stat of the enemy
	}
}
