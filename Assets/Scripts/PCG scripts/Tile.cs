using UnityEngine;
using System.Collections;
//using AssemblyCSharp;

public class Tile : MonoBehaviour {

	public TileType type;

	public Tile north;
	public Tile east;
	public Tile south;
	public Tile west;

    public enum TileTypes
    {
        LAVA = 0,
        GRASS = 1,
        SAND = 2,
    }

    public int code;//{ get; set;}
    public string terrain;//{ get; set;}
  //  public bool occupied;//{ get; set;} // Can the tile be walked onto?
    public GameObject occupant;//{ get; set;}
    public GameObject visual;// { get; set;}
    //Marks the tile to signify that it is within range 
    void highLight()
    {
        ;//Does nothing
    }

	private ArrayList adj;

	private bool occupied;

	void Start(){
		
	}

	public TileType getType(){
		return this.type;
	}

	public void setType(TileType ty){
		this.type = ty;
	}

	public Tile getNorth(){
		return this.north;
	}

	public void setNorth (Tile x){
		this.north = x;
	}

	public void setEast (Tile x){
		this.east = x;
	}

	public Tile getEast(){
		return this.east;
	}

	public void setWest (Tile x){
		this.west = x;
	}

	public Tile getWest(){
		return this.west;
	}

	public void setSouth (Tile x){
		this.south = x;
	}

	public Tile getSouth(){
		return this.south;
	}




  







}

