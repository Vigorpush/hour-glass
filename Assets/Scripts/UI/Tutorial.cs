using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    protected ArrayList tutorialText;

    public GameObject magePic;
    public GameObject rangerPic;
    protected GameObject theHourglass;

    public Text info;
    protected int index;

	// Use this for initialization
	void Start () {
        index = 0;
        theHourglass = GameObject.FindGameObjectWithTag("OfficialHourGlass");

        tutorialText = new ArrayList();
        string one = "Welcome to Hourglass!";
        string two = "The goal of the game is to explore a dungeon, "+
            "and to get as far as possible before the Hourglass runs out of time.";
        string three = "The hourglass is located in the top left of the screen, with the time remaining printed below it.";
        string four = "The hourglass is currently paused, but will tick down while you explore and battle.  In battle, "+
            "taking too long penalizes the hourglass, but timing abilities well inversely adds time to it.";
        string five = "Your party consists of a daring Fighter.";
        string six = "A cunning mage.";
        string seven ="And a wise Ranger.";
        string eight = "While exploring the dungeon, entering rooms or triggering traps will begin an encounter.";
        string nine = "During an encounter, the party and enemies take turns acting, based on their initiative."+
            "You will have 10 seconds during a turn.  If you take longer than 10 seconds, you borrow time, but at a cost...";
        string ten = "During enemy turns the hourglass does not tick down, but during party member turns it does!";
        string eleven = "The Fighter possesses 2 abilities, [1] is a devastating single target attack,  [2] is"+
            " a whirlwind that attacks all adjacent enemies, and [3] is a dangerous enrage, which increases damage at the cost of health";
        string twelve = "The Mage possesses [1] a single target fireball [2] a wide area "+
           "lightning storm, and [3] a room-wide AOE.";
        string thirteen = "The Ranger possesses [1] a single target ranged attack, and [2] is an area of affect" +
            " attack. The Rangers [3] is the only party member with a healing ability.";          
        string fourteen = "Each party member may move, and press [1] [2] or [3] to initiate an ability.  [space] will skip their turn";
        string fifteen = "Single target abilities can be cycled by the [tab] key, multi targets hit all"+
            " available targets.  Press [space] to confirm targets and begin the ability.";
        string sixteen = "While executing an ability, there are several input windows for a keypress.  Successfully inputing "+
            " the key will deal more damage, and lead to further combos of the ability.";
        string eighteen = "While attacking,"
           + " each character has a small window to 'perfect' combo.  This window occurs just before the input text flashes blue.";
        string nineteen = "The goal is to reach the red exit tiles.  Good Luck!";

        tutorialText.Add(one);
        tutorialText.Add(two);
        tutorialText.Add(three);
        tutorialText.Add(four);
        tutorialText.Add(five);
        tutorialText.Add(six);
        tutorialText.Add(seven);
        tutorialText.Add(eight);
        tutorialText.Add(nine);
        tutorialText.Add(ten);
        tutorialText.Add(eleven);
        tutorialText.Add(twelve);
        tutorialText.Add(thirteen);
        tutorialText.Add(fourteen);
        tutorialText.Add(fifteen);
        tutorialText.Add(sixteen);
       // tutorialText.Add(seventeen);
        tutorialText.Add(eighteen);
        tutorialText.Add(nineteen);


        info.text = tutorialText[index].ToString();
        index++;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            theHourglass.SendMessage("tutorialFinished");
            this.gameObject.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (index ==5)
            {
                magePic.SetActive(true);
            }
            if (index == 6)
            {
                rangerPic.SetActive(true);
            }


            if (index ==18)
            {
                theHourglass.SendMessage("tutorialFinished");
                this.gameObject.SetActive(false);
            }
            else {
                nextWindow();
            }
        }
    }

    void nextWindow()
    {
        info.text = tutorialText[index].ToString();
        index++;
    }
}
