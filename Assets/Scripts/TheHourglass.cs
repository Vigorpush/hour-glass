using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TheHourglass : MonoBehaviour
{

    public GameObject officialImage;
    public Text theCountdownText;
    private ParticleSystem partEffects;
    private AudioSource timeAddSound;

    private Animator effects;

    public GameObject theExplorer;
    private float timeLeft;


    public bool paused;

    // Use this for initialization
    void Start()
    {
        paused = true;
        timeLeft = 1200f;
        timeAddSound = GetComponent<AudioSource>();
        officialImage = GameObject.FindGameObjectWithTag("OfficialHourGlassImage");
        effects = officialImage.GetComponent<Animator>();
        partEffects = GetComponent<ParticleSystem>();

        //Show tutorial

        Invoke("tellPlayerToStart", 2f);  //begin exploring
    }

    private void tellPlayerToStart()
    {
        paused = false;
        theExplorer.SendMessage("EnterExplorationMode");
    }

    public void pause()
    {
        paused = true;
        theCountdownText.color = Color.green;
    }

    public void resume()
    {
        paused = false;
        theCountdownText.color = Color.red;
    }

    public void add1Second()
    {
        timeAddSound.Play();
        partEffects.Emit(10);
        effects.SetTrigger("TimeAdded");
        timeLeft += 1f;
    }

    void Update()
    {
        if (!paused)
        {
            timeLeft -= Time.deltaTime;

            string minutes = Mathf.Floor(timeLeft / 60).ToString("00");
            string seconds = Mathf.Floor(timeLeft % 60).ToString("00");


            theCountdownText.text = minutes + ":" + seconds;

            if (timeLeft <= 0)
            {
                Debug.Log("Game Over!");
            }

        }


    }

}

