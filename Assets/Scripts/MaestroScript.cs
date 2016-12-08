using UnityEngine;
using System.Collections;

public class MaestroScript : MonoBehaviour {

    public AudioSource[] theSoundtrack;

    private AudioSource exploreMusic;
    private AudioSource battleMusic;
    private AudioSource doorOpen;
    private AudioSource combatOver;
    private AudioSource levelComplete;

    private bool Combat;
    private bool Explore;
    private float exploreVolume;
    private float battleVolume;

    // Use this for initialization
    void Start () {
        exploreVolume = 0.5f;
        battleVolume = 0.5f;
        theSoundtrack = GetComponents<AudioSource>();
        battleMusic = theSoundtrack[1];
        exploreMusic = theSoundtrack[2];
        doorOpen = theSoundtrack[3];
        combatOver = theSoundtrack[4];
        levelComplete = theSoundtrack[5];
        Combat = false;
        Explore = true;
        exploreMusic.Play();
	}

    void Update()
    {
        if (!Explore && exploreVolume>0)
        {
            exploreVolume -=(float) 0.9 * Time.deltaTime;
            exploreMusic.volume = exploreVolume;             
        }
        if (Combat && battleVolume<=0.5f)
        {
            //battleVolume += (float)0.1 * Time.deltaTime;
            battleMusic.volume = 0.5f; //Fade in wasn't good for most battle music
            //battleMusic.volume = battleVolume;
        }
        if (Explore && exploreVolume <= 0.5f)
        {
            exploreVolume += (float)0.1 * Time.deltaTime;
            exploreMusic.volume = exploreVolume;
        }
        if (!Combat && battleVolume > 0)
        {
           // Debug.Log("Battle end");
            battleVolume -= (float)2 * Time.deltaTime;
            battleMusic.volume = battleVolume;
        }
    }

    private void StopBattleMusic()
    {
        battleMusic.Stop();
    }

    private void StopExploreMusic()
    {
        exploreMusic.Stop();
    }

    public void FloorCleared()
    {
        levelComplete.Play();
    }

    public void BeginCombat()
    {
        Explore = false;
        doorOpen.Play();
        Invoke("PlayBattle1",2f);
        Invoke("StopExploreMusic",3f);
    }

    public void EndCombat()
    {
        combatOver.Play();
        Combat = false;       
        Invoke("PlayExplore", 2f);
        Invoke("StopBattleMusic",3f);
    }

    public void TargetPing()
    {
        theSoundtrack[0].Play();
    }

    public void PlayBattle1()
    {
        Combat = true;
        battleMusic.Play();
    }

    public void PlayExplore()
    {
       // Debug.Log("Playing explore music?");
        Explore = true;
        exploreMusic.Play();
    }

 
}
