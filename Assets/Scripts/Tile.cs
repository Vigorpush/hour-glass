using UnityEngine;
using System.Collections;
//Tiles for the map

[System.Serializable] 
public class Tile {
	public enum TileTypes{

		LAVA= 0,
		GRASS=1,
		SAND = 2,

	}

	public int code;//{ get; set;}
	public string terrain;//{ get; set;}
	public bool occupied;//{ get; set;} // Can the tile be walked onto?
	public GameObject occupant;//{ get; set;}
	//MAYBE USE TEXTURE HERE
	public GameObject visual;// { get; set;}

	// Use this for initialization
	//Marks the tile to signify that it is within range 
	void highLight(){
		//visual.GetComponent<SpriteRenderer> ().color 
	}
		

}
