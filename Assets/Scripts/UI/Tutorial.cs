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
        string two = "The goal of the game is to explore a dungeon, plagued by temporal distortion that has made beasts of many heroes past who did not escape. ";          
		string twoB = "Fortunately, you possess a trinket which can provide defense against time distortion : The Hourglass. It "+
            "and it's precious dust ward you and your allies against the otherwise accelerated ravages of time.";
        string three = "The Hourglass is located in the top left of the screen, with the time remaining printed below it.  Explore and attempt to locate the exit before it runs out.";
        string four = "The Hourglass is currently paused, but will evaporate dust while you explore and encounter the denizens of the dungeon. Dust is always running out when in control of your character "+
            ", but skillfully executing movement and abilities can ward off threats and recover lost time.";
        string five = "Your party consists of a daring Fighter.";
        string six = "A cunning Mage.";
        string seven ="And a wise Ranger.";
        string eight = "While exploring the dungeon, entering rooms or triggering traps will begin an encounter.";
        string nine = "During an encounter, the party and enemies take turns acting based on their initiative scores."+
            "On each turn, the Hourglass wards 10 seconds to move and attack while time passes normally. It is possible to spend more time in a turn, but at a cost...";
        string ten = "During enemy turns the Hourglass does not tick down, but during party member turns it does!";
        string eleven = "The Fighter possesses 3 abilities, [1] is a devastating Banishing Strike,  [2] is"+
            " a blade Whirlwind which attacks all enemies within sword's reach, and [3] is an Unstable Enrage, where blood is shed in exchange for uncanny strength for the battles duration.";
        string twelve = "The Mage possesses [1] a single target Fireball [2] a wide area "+
           "Lightning Storm, and [3] a room-wide Missile Cascade that deals heavy damage after a short pause as damage is charged.";
        string thirteen = "The Ranger possesses [1] a single target Arrow Strafe, and [2] is an area of affect" +
            " Arrow Multishot. The Ranger's [3] Dew Drop is the only ability capable of mending friendly wounds.";          
        string fourteen = "Each party member may move, and press [1] [2] or [3] to initiate an ability.  [Space] will skip their turn";
        string fifteen = "Single target abilities can be cycled by the [tab] key, multi targets hit all"+
            " available targets.  Press [space] to confirm targets and begin the ability.";
        string sixteen = "While executing an ability, there are several input windows for a keypress.  Successfully inputing "+
            " the key will deal more damage, and lead to further combos of the ability.";
        string eighteen = "While attacking,"
           + " each character has a small window to 'perfect' combo.  This window occurs just before the input text flashes blue.";
		string eighteenB = "Hourglass is a game of mastery. Decisions can always be made faster, navigation optimized, and combos more precisely executed.";
        string nineteen = "Good Luck!...";
		//string twenty = "YOU'LL NEED IT";

        tutorialText.Add(one);
        tutorialText.Add(two);
		tutorialText.Add(twoB);
		//tutorialText.Add(twoC);
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
		//tutorialText.Add(fourteenB);
        tutorialText.Add(fifteen);
        tutorialText.Add(sixteen);
       // tutorialText.Add(seventeen);
        tutorialText.Add(eighteen);
		tutorialText.Add(eighteenB);
        tutorialText.Add(nineteen);
		//tutorialText.Add (twenty);



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
            if (index ==6)
            {
                magePic.SetActive(true);
            }
            if (index == 7)
            {
                rangerPic.SetActive(true);
            }


			if (index ==tutorialText.Count)
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