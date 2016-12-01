using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hourglass : MonoBehaviour
{
    public float startingTime = 100f;                            	
    public float currentTime;                                   	
    public Slider hourglass;
	public float speedofGlass=10f;   


	GameObject Hourglassbackground;
	GameObject timeleftObejct;
	Text timeleft;

	void Start(){
		assignedvalues ();
	}


	public void assignedvalues(){
		hourglass.maxValue = startingTime;
		currentTime = startingTime;
		Hourglassbackground = GameObject.FindGameObjectWithTag("hourglassbackground");
		timeleftObejct = GameObject.FindGameObjectWithTag("timeleft");
		timeleft= timeleftObejct.GetComponent<Text>();
		Hourglassbackground.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, currentTime/startingTime);
	}
    void Update ()
    {

    }

	public void runningHour(){
		currentTime -= Time.deltaTime * speedofGlass;
		hourglass.value = currentTime;
		setColor ();
		timeleft.text = currentTime+" Second Left";
		if(currentTime <= 0f){
			explosion();
		}
	}

	void explosion(){}
	
	void setColor(){
		Hourglassbackground.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, currentTime/startingTime);
	}


	/*
	//change the music when the current time is low
    void setMusic (){
		if (( this.currentTime <  this.StartTime*0.5)&&(  this.currentTime > 0)){
			if(_AudioSource.clip == normalClip){
				_AudioSource.clip = nighwareClip;
				_AudioSource.Play();
			}
		}
		if( this.currenTime >=  this.StartTime * 0.5){
		if(_AudioSource.clip == nighwareClip){
				_AudioSource.clip = normalClip;
				_AudioSource.Play();
			}
		}
	}
	
	
	void addedTime (float timeAdded){
		if (addedSound!=null)
			//audio.PlayOneShot (addedSound);
		currentTime += timeAdded;
	}
	
	void minusTime (float timeMinused){
		if (minusSound!=null)
			//audio.PlayOneShot (minusSound);
		currentTime -= timeMinused;
	}
	*/
}