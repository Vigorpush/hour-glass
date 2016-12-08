using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TheHourglass : MonoBehaviour
{
    public GameObject tutorialHelper;
    public GameObject officialImage;
    public GameObject timeAnnounce;
    public Text theCountdownText;
    private ParticleSystem partEffects;
    private AudioSource timeAddSound;
    private GameObject cam;
	public float TIME_LEFT_INITIAL = 1200f; //20 min
	public float TIME_SCALE =1f;
	public int NUM_EFFECTS = 15;
    public bool audioPlaying;

    private Animator effects;

    private Text announceTime;

    public GameObject theExplorer;
    private float timeLeft;

    public float thisTurnTime = 10f; //10s for a whole turn
    public bool thisTurnTicking;

    public float TIME_DECAY_AMOUNT = 3f;

    public bool paused;
    public bool tickPlaying;

    private string announceSeconds ="";


    public void PlayerTurnEnded()
    {
        if (thisTurnTime > 0)
        {
            Debug.Log("Player Turn Ended and adding this much time: "+ thisTurnTime);
            effects.SetTrigger("TimeAdded");
            timeLeft += thisTurnTime;
        }

        timeAnnounce.SetActive(false);
        thisTurnTicking = false;
        DefaultSpeed();
    }

    public void PlayerTurnStart(){
        audioPlaying = false;
        tickPlaying = false;
        announceTime.color = Color.white;
        announceTime.fontSize = 14;
            timeAnnounce.SetActive(true);
            thisTurnTime = 10f; //reset to standard turn time
            thisTurnTicking = true;
        }


    public void Accelerate()
    {
        TIME_SCALE += TIME_DECAY_AMOUNT;
        officialImage.SendMessage("TakingTooLong");
    }

    public void DefaultSpeed()
    {
        TIME_SCALE = 1f;
        officialImage.SendMessage("BackToNormal");
    }

    // Use this for initialization
    void Start()
    {
        audioPlaying = false;
        tickPlaying = false;
        announceTime = timeAnnounce.GetComponent<Text>();
        thisTurnTicking = false;
        paused = true;
		timeLeft = TIME_LEFT_INITIAL;
        timeAddSound = GetComponent<AudioSource>();
        officialImage = GameObject.FindGameObjectWithTag("OfficialHourGlassImage");
        effects = officialImage.GetComponent<Animator>();
        partEffects = GetComponent<ParticleSystem>();

        cam = GameObject.FindGameObjectWithTag("MainCamera");
        //Show tutorial
        cam.GetComponent<CameraFollow>().SetCameraFollow(theExplorer);
        // Invoke("tellPlayerToStart", 2f);  //begin exploring
    }

    

    public void tutorialFinished()
    {
        paused = false;
        theExplorer.SendMessage("EnterExplorationMode");
    }

    public void pause()
    {
        paused = true;
        //theCountdownText.color = Color.green;
		theCountdownText.color = Color.grey;
    }

    public void resume()
    {
        paused = false;
        theCountdownText.color = Color.red;
    }

    public void add1Second()
    {
       // timeAddSound.Play();
        //partEffects.Emit(NUM_EFFECTS);
        partEffects.Play();
        effects.SetTrigger("TimeAdded");
        timeLeft += 1f; //Add 1 second on successful ability timing
    }

    void Update()
    {
        if (!paused)
        {
			timeLeft -= Time.deltaTime * TIME_SCALE;

            string minutes = Mathf.Floor(timeLeft / 60).ToString("00");
            string seconds = Mathf.Floor(timeLeft % 60).ToString("00");

            theCountdownText.text = minutes + ":" + seconds;
			/*
            if (timeLeft <= 0)
            {
                Debug.Log("Game Over!");
            }
			*/
        }
        if(thisTurnTicking){

           
            announceSeconds = Mathf.Floor(thisTurnTime % 60).ToString("00");
            announceTime.text = announceSeconds;
            thisTurnTime -= Time.deltaTime;
            if(thisTurnTime < 3 && thisTurnTime > 2){
                announceTime.fontSize = 16;
                if (!tickPlaying)
                {
                    tickPlaying = true;
                    timeAddSound.Play();
                }
            }
            else if (thisTurnTime < 2 && thisTurnTime > 1){
                announceTime.fontSize = 18;
                announceTime.color = Color.yellow;
            }
            else if (thisTurnTime < 1 && thisTurnTime > 0){
                announceTime.fontSize = 18;
                announceTime.color = Color.red;
            }
            else if (thisTurnTime <= 0)
            {
                if (!audioPlaying)
                {
                    Accelerate();
                    audioPlaying = true;
                    timeAnnounce.SetActive(false);
                    
                }
                
            }
        }



    }

}

