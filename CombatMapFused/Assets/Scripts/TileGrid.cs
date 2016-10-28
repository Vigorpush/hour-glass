using UnityEngine;
using System.Collections;
//A 2D Map Grid
public class TileGrid : MonoBehaviour {
	public Tile[] tileTypes;
	int[,] tiles;
	const int MAP_H=50;
	const int MAP_W=100;
	Vector2 selectedTile; //Only to be used if we use a cursor
	// Use this for initialization
	void Start () {
		
		Vector2 selectedTile = new Vector2 (0, 0);
		initialize ();
		randomize ();
		createBorder ();
		draw ();
				

		//CreateBorder ();
	}
	//TODO
	void setSelected(int x,int y){
		selectedTile.Set (x, y);	
	}
	//TODO
	void setSelected(Vector2 pos){
		selectedTile = pos;
	}
	//Initializes bitmap to all 0s
	void initialize(){
		//- ALLOCATION - ALL TILES ARE INITIALIZED TO 0 
		tiles = new int[MAP_W, MAP_H];
		for (int i = 0; i < MAP_W; i++) {
			for (int j = 0; j < MAP_H; j++) {

			}
		}
	}
	//Border for map with colliders
	void createBorder(){
		for (int i = -1; i < MAP_W+1; i++) {
			for (int j = -1; j < MAP_H+1; j++) {
				if (j == -1 || j == MAP_H || i == -1 || i == MAP_W) {
					
					GameObject tile = (GameObject)Instantiate (tileTypes [1].visual, new Vector3 ((i ), (j), 0f), Quaternion.identity);
					tile.name = "Tile: " + i + ", " + j;
				

					tile.GetComponent<Transform>().parent=(this.GetComponent<Transform> ());
					 
				}
			}
		}

	}
		//Randomizes the tiles
	void randomize(){
		for (int i = 0; i < MAP_W; i++) {
			for (int j = 0; j < MAP_H; j++) {
				
				tiles [i, j] = Random.Range (0,tileTypes.Length);
			}
		}
	}

	//Instantiates the boardTiles
	void draw(){
		for (int i = 0; i < MAP_W; i++) {
			for (int j = 0; j < MAP_H; j++) {
				GameObject tile = (GameObject) Instantiate (tileTypes [2].visual, new Vector3 ((i ), (j), 0f), Quaternion.identity);
				tile.GetComponent<Transform> ().parent =(this.GetComponent<Transform> ());
				tile.name = "Tile: " + i + ", " + j;


				//Any firsttime code for a generated tile goes here
			}
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}

