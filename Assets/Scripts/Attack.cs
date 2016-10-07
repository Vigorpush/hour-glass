using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Attack : MonoBehaviour {

    public float timeLeft;
    bool pauseHourglass;
    private Animator anim;
    private bool castingAnimating;
    private bool timeStampC1Set;
    private bool timeStampC2Set;
    private bool timeStampC3Set;
    private float timeDiff;
    

    public Text theHourglass;

    public Text inputMessage;

	// Use this for initialization
	void Start () {
        //start counting down timer
        timeLeft = 30;
        pauseHourglass = false;
        anim = GetComponent<Animator>();
        castingAnimating = false;
        timeStampC1Set = false;
        timeStampC2Set = false;
	}

    // Update is called once per frame
    void Update() {

        //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("Elf Casting"));

        theHourglass.text = "Time left: " + Mathf.Round(timeLeft);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Elf Casting"))
        {
            //Debug.Log("The elf is casting");
           // if (anim.GetCurrentAnimatorStateInfo(0).) { }
        }

        if (!pauseHourglass)
        {
            timeLeft -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && !castingAnimating)
        {
            anim.SetTrigger("Start Casting");
            pauseHourglass = true;
            castingAnimating = true;

        }

        if (timeStampC1Set)
        {
           // Debug.Log("First Combo Attempt: Begin keypress window for a");
           // Debug.Log(timeDiff - timeLeft);
            inputMessage.text = "Press A!";
            if(timeLeft < 0)
            {
                anim.SetTrigger("Failed Input, Timer out");
                timeStampC1Set = false;
                inputMessage.text = "Round lost, Time up!";
            }
            if (Input.GetButtonDown("Fire1"))
            {
               // Debug.Log("A pressed");
                anim.SetTrigger("Successful Input");
                timeStampC1Set = false;
                timeStampC2Set = true;
            }
            if (timeDiff-timeLeft > 2)
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
           // Debug.Log(timeDiff - timeLeft);
            inputMessage.text = "Press S!";
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
            if (timeDiff - timeLeft > 2)
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
          //  Debug.Log(timeDiff - timeLeft);
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
            if (timeDiff - timeLeft > 2)
            {
                anim.SetTrigger("Failed Input");
                timeStampC3Set = false;
                inputMessage.text = "Round lost, missed input!";
                pauseHourglass = true;
            }
        }

    }

    public void BeginCastAttempt1()  {
        pauseHourglass = false;
        if(!timeStampC1Set)     //if this is the first frame of the combo window
        {
            timeStampC1Set = true;
            timeDiff = timeLeft;
        }
    }

    public void tryCombo1()
    {
        Debug.Log("Begin keypress window for s");
        timeDiff = timeLeft;
    }

    public void tryCombo2()
    {
        Debug.Log("Begin keypress window for d");
        timeDiff = timeLeft;
    }
    
}
