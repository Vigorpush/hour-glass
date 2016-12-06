using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class turnhourglass : MonoBehaviour {

    public float AVERAGE_TURN_TIME = 30f;

	//start of turn base hourglass
	public float turnbased_startingTime = 100f;                            	
	public float turnbased_currentTime;                                   	
	public Slider turnbased_hourglass;
	public float turnbased_speedofGlass =10f;   
	GameObject turnbased_Hourglassbackground;
	GameObject turnbased_timeleftObejct;
	Text turnbased_timeleft;

	//start of hourglass
	public float startingTime = 100f;                            	
	public float currentTime;                                   	
	public Slider hourglass;
	public float speedofGlass =10f;
	GameObject Hourglassbackground;
	GameObject timeleftObejct;
	Text timeleft;

    //private Transform glass;

	bool turnisover;//check the current turn is over

	void Start(){
       // this.transform.SetParent("MainCanvas");
        //glass = this.gameObject.GetComponent<Transform>();
		//setting the turnbase hourglass
		turnbased_hourglass.maxValue = turnbased_startingTime;
		turnbased_currentTime = turnbased_startingTime;
		turnbased_Hourglassbackground = GameObject.FindGameObjectWithTag("turnbackground");
		turnbased_timeleftObejct = GameObject.FindGameObjectWithTag("timeleft1");
		turnbased_timeleft= turnbased_timeleftObejct.GetComponent<Text>();
		turnbased_Hourglassbackground.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, turnbased_currentTime/turnbased_startingTime);

		//setting hourglass
		hourglass.maxValue = startingTime;
		currentTime = startingTime;
		Hourglassbackground = GameObject.FindGameObjectWithTag("hourglassbackground");
		timeleftObejct = GameObject.FindGameObjectWithTag("timeleft");
		timeleft= timeleftObejct.GetComponent<Text>();
		Hourglassbackground.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, currentTime/startingTime);
        turnisover = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!turnisover) { 
		turnbased_currentTime -= Time.deltaTime * turnbased_speedofGlass;
		turnbased_hourglass.value = turnbased_currentTime;
		turnbased_timeleft.text = turnbased_currentTime+" Second Left";
		turnbased_Hourglassbackground.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, turnbased_currentTime/turnbased_startingTime);
		if(turnbased_currentTime<=0f){//if turn based hourglass is run out, then start to use the hourglass
			runningHour ();
		}
		if(turnisover){//checking the turn is over and add time if there is some time of current turn left
			if (turnbased_currentTime > 0f) {
				addedTime (turnbased_currentTime);
			}	
		}
    }
	}


	public void setTurnhourglass(float time){//the turn based hourglass will be reset, by rebuild new one.
		turnbased_hourglass.maxValue = turnbased_startingTime;
		turnbased_currentTime = turnbased_startingTime;
		turnbased_Hourglassbackground = GameObject.FindGameObjectWithTag("turnbackground");
		turnbased_timeleftObejct = GameObject.FindGameObjectWithTag("timeleft1");
		turnbased_timeleft= turnbased_timeleftObejct.GetComponent<Text>();
		turnbased_Hourglassbackground.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, turnbased_currentTime/turnbased_startingTime);
	}


	public void runningHour(){//running hourglass
		currentTime -= Time.deltaTime * speedofGlass;
		hourglass.value = currentTime;
		setColor ();
		timeleft.text = currentTime + " Second Left";
		if(currentTime <= 0f){
			explosion();
		}
	}

	//enable the explosion
	void explosion(){}

	void setColor(){
		Hourglassbackground.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, currentTime/startingTime);
	}

	void addedTime (float timeAdded){
		//if (addedSound!=null)
			//audio.PlayOneShot (addedSound);
			currentTime += timeAdded;
	}

	void minusTime (float timeMinused){
		//if (minusSound!=null)
			//audio.PlayOneShot (minusSound);
			currentTime -= timeMinused;
	}

    public void PlayerTurnStarted()
    {
        setTurnhourglass(AVERAGE_TURN_TIME);
        turnisover = false;
    }
    public void PlayerTurnStopped()
    {
        turnisover = true;
    }



}
