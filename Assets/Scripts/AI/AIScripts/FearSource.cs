using UnityEngine;
using System.Collections;

public class FearSource : MonoBehaviour{
	public int radius{get; set;}
	public int strength{ get; set;}
   // public int defaultRange();
    protected HeroUnit reference; 
	// Use this for initialization
    /*
	public FearSource(int rad, int str){
		strength = str;
		radius = rad;
	}
    */

    public void Start()
    {
        this.gameObject.GetComponent<HeroUnit>();

    }
    public void updateStats(){
        this.radius = reference.range + reference.speed; 
        this.strength = reference.attack;
    }

	public FearSource(){
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
