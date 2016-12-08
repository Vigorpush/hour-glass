using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HourGlassListener : MonoBehaviour {

    public GameObject timerText;
    private AudioSource demonVoices;

    void Start()
    {
        demonVoices= GetComponent<AudioSource>();
    }

	// Use this for initialization
   public void TakingTooLong()
   {
       demonVoices.Play();
       timerText.GetComponent<Text>().fontSize = 32;
   }

   public void BackToNormal()
   {
       timerText.GetComponent<Text>().fontSize = 25;
   }
}
