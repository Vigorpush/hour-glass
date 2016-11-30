using UnityEngine;
using System.Collections;

public class MaestroScript : MonoBehaviour {

    public AudioSource[] theSoundtrack;

	// Use this for initialization
	void Start () {
        theSoundtrack = GetComponents<AudioSource>();
	}

    public void TargetPing()
    {
        theSoundtrack[0].Play();
    }

    public void PlayBattle1()
    {
        theSoundtrack[1].Play();
    }
	
}
