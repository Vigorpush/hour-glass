using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LvlUpTooltips : MonoBehaviour {

    public GameObject AttackButton;
    public GameObject DefenseButton;
    public GameObject SpeedButton;

    public Button attackUp;
    public Button defenseUp;
    public Button speedUp;

    public GameObject P1;
    public GameObject P2;
    public GameObject P3;

    public bool allowLevelUp;

    public GameObject HeroToLvl;

    public Text stats;

    protected HeroUnit heroSheet;

    protected int hpToAdd;
    protected int attackToAdd;
    protected int speedToAdd;

    protected string improvedStatAttack;
    protected string improvedStatDefense;
    protected string improvedStatSpeed;


	// Use this for initialization
	void Start () {
        attackUp = AttackButton.GetComponent<Button>();
        defenseUp = DefenseButton.GetComponent<Button>();
        speedUp = SpeedButton.GetComponent<Button>();
        allowLevelUp = false;
        heroSheet = HeroToLvl.GetComponent<HeroUnit>();
       // stats = GetComponent<Text>();
        hpToAdd = 1;
        improvedStatAttack = "1";
        improvedStatDefense = "1";
        improvedStatSpeed = "1";




        updateText();
	}

    protected void updateText()
    {
        stats.text = "Max Hp : " + heroSheet.getmaxhp() + " -> " +
            "\nAttack : " + heroSheet.getattack() + " -> " +
            "\nSpeed : " + heroSheet.getspeed() + " -> ";          
    }

    public void showLvlUpInterface()
    {
        this.gameObject.active = true;
    }

    public void BaseUp()
    {
        hpToAdd = 1;
        attackToAdd = 1;
        speedToAdd = 1;
    }

    public void lvlThisHero(GameObject heroIn){
        HeroToLvl = heroIn;
    }

    public void AttackUp(){
       Debug.Log("Level up attack");
        hpToAdd = 1;
        attackToAdd = 5;
        speedToAdd = 1;
        updateText();

    }
	
    public void DefenseUp(){
       Debug.Log("Level up defense");
        hpToAdd = 10;
        attackToAdd = 1;
        speedToAdd = 1;
        updateText();
    }
    public void SpeedUp(){
        Debug.Log("Level up speed");
        hpToAdd = 1;
        attackToAdd = 1;
        speedToAdd = -1;
        updateText();
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (allowLevelUp)
        {
            Debug.Log("lvlup screen listening for interaction");
        }
	}
}
