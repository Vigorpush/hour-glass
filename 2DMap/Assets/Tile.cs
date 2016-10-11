using UnityEngine;
using System.Collections;
//Tiles for the map

[System.Serializable] 
public class Tile {
	private enum TileTypes{

		LAVA= 0,
		GRASS=1,
		SAND = 2,

	}
	private bool selected;
	private int code;
	public string terrain;
	private bool occupied; // Can the tile be walked onto?
	public GameObject occupant;
	//MAYBE USE TEXTURE HERE
	public GameObject visual;

	// Use this for initialization
	//Marks the tile to signify that it is within range 
	void highLight(){
		//visual.GetComponent<SpriteRenderer> ().color 
	}
	void select(){
		selected = true;
	}
	void deselect(){
		selected = false;
	}
	void awake(){
		
	}
	void Start () {
	
	}

	
	// Update is called once per frame
	void Update () {
	
	}

}
