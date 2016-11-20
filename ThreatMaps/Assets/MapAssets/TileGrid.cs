using UnityEngine;
using System.Collections;
using TileInfo;


//A 2D Map Grid
public class TileGrid : MonoBehaviour {
	//public ArrayList<Occupant> allThings;

	public Tile[] tileTypes;
	public int[,] tiles;
	//public Occupant[,] occupants;
	public const int MAP_H=50;
	public const int MAP_W=50;
	private Vector2 selected = new Vector2 (0, 0);

	// Use this for initialization
	void Start () {
		
		Vector2 selectedTile = new Vector2 (0, 0);
		initialize ();
		randomize ();
		draw ();
	}
			void setSelected(int x,int y){
				selected = new Vector2 (x, y);	
			}
			
			void setSelected(Vector2 pos){
				selected = pos;
			}
	//Initializes bitmap to all 0s
	void initialize(){
		//- ALLOCATION - ALL TILES ARE INITIALIZED TO 0 
		tiles = new int[MAP_W, MAP_H];
		//occupants = new Occupant[MAP_W, MAP_H];
		for (int i = 0; i < MAP_W; i++) {
			for (int j = 0; j < MAP_H; j++) {

			}
		}
	}
	//Randomizes the tiles
	void randomize(){
		for (int i = 0; i < MAP_W; i++) {
			for (int j = 0; j < MAP_H; j++) {
				//
				tiles [i, j] = Random.Range (0,System.Enum.GetValues(typeof(TileTypes)).Length);
			}
		}
	}
	//Instantiates the boardTiles
	void draw(){
		for (int i = 0; i < MAP_W; i++) {
			for (int j = 0; j < MAP_H; j++) {
				Instantiate (tileTypes [tiles [i,j]].visual, new Vector3 (i*.16f, j*.16f, 0f), Quaternion.identity);
			}
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}

