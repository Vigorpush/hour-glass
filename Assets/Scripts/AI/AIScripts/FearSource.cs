using UnityEngine;
using System.Collections;

public class FearSource{
	public int radius{get; set;}
	public int strength{ get; set;}
	// Use this for initialization
	public FearSource(int rad, int str){
		strength = str;
		radius = rad;
	}

	public FearSource(){
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
