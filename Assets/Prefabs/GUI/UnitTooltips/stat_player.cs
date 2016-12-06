using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stat_player : MonoBehaviour {

	private GameObject playerstatPanel;//accessing the Game object of the panel
	
	//Using those to save times
	public Text HP1;
	//public Text HP2;
	//public Text HP3;
	
	public Text Attack1;
	//public Text Attack2;
	//public Text Attack3;
	
	public Text Speed1;
	//public Text Defense2;
	//public Text Defense3;

    public Unit myStats;
	
	void Start(){
		playerstatPanel = GameObject.FindGameObjectWithTag("playerstat");
		playerstatPanel.gameObject.SetActive(false);

        
        myStats = this.gameObject.GetComponent<Unit>();

	}
	void Update(){
		if(Input.GetKeyUp(KeyCode.LeftShift)){
			closeWindow();
		}
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			showWindow();
		}
	}
	void closeWindow(){
		playerstatPanel.gameObject.SetActive(false);
	}
	void showWindow(){	
		refreshStat();
		playerstatPanel.gameObject.SetActive(true);
	}
	//this is function will set the HP, attack and defense values into table
	void refreshStat(){
        HP1.text = "HP: " + myStats.getcurhp() + " / " + myStats.getmaxhp();
        Attack1.text = "ATK: " + (myStats.getDamageMin() + myStats.getattack())+ "-"
         + (myStats.getDamageMax()+ myStats.getattack());
        Speed1.text = "DEF: " + myStats.getArmour();

		/*Debug.Log("Refresh Player Stat");
		HP1.text = "Player 1 HP : ";
		HP2.text = "Player 2 HP : ";
		HP3.text = "Player 3 HP : ";
		Attack1.text = "player 1 Attack : ";
		Attack2.text = "player 2 Attack : ";
		Attack3.text = "player 3 Attack : ";
		Defense1.text = "Player 1 Defense : ";
		Defense2.text = "Player 2 Defense : ";
		Defense3.text = "Player 3 Defense : ";*/
	}
}
