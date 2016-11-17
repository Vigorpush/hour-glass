using UnityEngine;
using System.Collections;

public class DecayingFearSource : FearSource{
	public float decayFactor{ get; set;}
	public DecayingFearSource(int rad, int str,float decayFact){
		decayFactor = decayFact;
		strength = str;
		radius = rad;

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
