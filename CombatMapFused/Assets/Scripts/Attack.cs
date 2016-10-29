using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//Defines the time aspect of one ability
public class Attack : MonoBehaviour {

    
    //---------------------------------//
	//ANIMATOR
    public Animator anim;


	//----------------------------------//


    public bool castingAnimating;
	bool pauseHourglass;
	//---------------------------------//
	//Necessary for delta time on combos

	public bool timeStampC1Set,timeStampC2Set,timeStampC3Set;
	public float TIME_LEFT_INITIAL = 30 ; 
	public float timeLeft;
    public float comboStartTime;  


	//---------------------------------//
	//GUI ELEMENTS
    public Text theHourglass;

    public Text inputMessage;

	// Use this for initialization
	void Start () {

		//Misc setup
		anim = GetComponent<Animator>();

        //start counting down timer
		timeLeft = TIME_LEFT_INITIAL;
       
        
		//boolean locks
		pauseHourglass = false;
        castingAnimating = false;
        timeStampC1Set = false;
        timeStampC2Set = false;
	}

    // Update is called once per frame
    void Update() {

        //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("Elf Casting"));

        theHourglass.text = "Time left: " + Mathf.Round(timeLeft);

		/* Depricated
        	if (anim.GetCurrentAnimatorStateInfo(0).IsName("Elf Casting"))
        	{
            //Debug.Log("The elf is casting");
           // if (anim.GetCurrentAnimatorStateInfo(0).) { }
        	}
		*/
		// tick down from start time
        if (!pauseHourglass)
        {
            timeLeft -= Time.deltaTime;
        }
		// none move action triggered
        if (Input.GetButtonDown("Fire1") && !castingAnimating)
        {
            anim.SetTrigger("Start Casting");
            pauseHourglass = true;
			//Animator is now decides when this should unlock using keyframe function call
            castingAnimating = true;
        }
		//********************$$$$$$$$$$$$$$$$$$$$$$$TODO: modulate each attack window$$$$$$$$$$$$$$$$$$$$$$$$************************
		//Play test: may change if we add crits and other mechanics
		//Initialize game with slotted 3 slotted abilities
        if (timeStampC1Set)
        {
           // Debug.Log("First Combo Attempt: Begin keypress window for a");
           // Debug.Log(comboStartTime - timeLeft);
            inputMessage.text = "Press A!";
			//Total turn time ran out
            if(timeLeft < 0)
            {
                anim.SetTrigger("Failed Input, Timer out");
                timeStampC1Set = false;
                inputMessage.text = "Round lost, Time up!";
            }
			//Successful button press
			if (Input.GetKey(KeyCode.Alpha1))
            {
               // Debug.Log("A pressed");
                anim.SetTrigger("Successful Input");
                timeStampC1Set = false;
                timeStampC2Set = true;
            }
			//Specific combo window missed
            if (comboStartTime-timeLeft > 2)
            {
                anim.SetTrigger("Failed Input");
                timeStampC1Set = false;
                inputMessage.text = "Round lost, missed input!";
                pauseHourglass = true;
            }
        }



        if (timeStampC2Set)
        {
           // Debug.Log("First Combo Attempt: Begin keypress window for s");
           // Debug.Log(comboStartTime - timeLeft);
            inputMessage.text = "Press S!"; //ALSO VARIABLEs
            if (timeLeft < 0)
            {
                anim.SetTrigger("Failed Input, Timer out");
                timeStampC2Set = false;
                inputMessage.text = "Round lost, Time up!";
            }
            if (Input.GetButtonDown("Fire2"))
            {
               // Debug.Log("S pressed");
                anim.SetTrigger("Successful Input");
                timeStampC2Set = false;
                timeStampC3Set = true;
            }
            if (comboStartTime - timeLeft > 2) // CLASS SPECIFIC
            {
                anim.SetTrigger("Failed Input");
                timeStampC2Set = false;
                inputMessage.text = "Round lost, missed input!";
                pauseHourglass = true;
            }
        }

        if(timeStampC3Set)
        {
          //  Debug.Log("First Combo Attempt: Begin keypress window for d");
          //  Debug.Log(comboStartTime - timeLeft);
            inputMessage.text = "Press D!";
            if (timeLeft < 0)
            {
                anim.SetTrigger("Failed Input, Timer out");
                timeStampC3Set = false;
                inputMessage.text = "Round lost, Time up!";
            }
            if (Input.GetButtonDown("Fire3"))
            {
             //   Debug.Log("D pressed");
                anim.SetTrigger("Successful Input");
                timeStampC3Set = false;
                inputMessage.text = "Success";
                pauseHourglass = true;
                //Save rest of turn time to hourglass -> houglass += timeleft
            }
            if (comboStartTime - timeLeft > 2)
            {
                anim.SetTrigger("Failed Input");
                timeStampC3Set = false;
                inputMessage.text = "Round lost, missed input!";
                pauseHourglass = true;
            }
        }

    }
	//Called after the cast animation 
    public void BeginCastAttempt1()  {
        pauseHourglass = false;
        if(!timeStampC1Set)     //if this is the first frame of the combo window
        {
			//The next part of update must execute
            timeStampC1Set = true;
			//Start combo time
            comboStartTime = timeLeft;
        }
    }

    public void tryCombo1()
    {
		
        Debug.Log("Begin keypress window for s");
        comboStartTime = timeLeft;
    }

    public void tryCombo2()
    {
        Debug.Log("Begin keypress window for d");
        comboStartTime = timeLeft;
    }
    
}
