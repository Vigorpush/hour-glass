using UnityEngine;
using System.Collections;
//using AssemblyCSharp;


public class floorMaker : MonoBehaviour {

	//The size of the floor
	public int floorWidth = 100;
	public int floorHight = 100;

	//The number of rooms to make;
	public int maxRooms = 4;
	public int minRooms = 4;
	protected Range numRooms;

	//The max and min size of a room
	public int maxRoomWidth = 5 ;
	public int minRoomWidth = 5 ;
	protected Range roomWidth;

	public int maxRoomHeight = 5;
	public int minRoomHeight = 5;
	protected Range roomHeight;

	//The max and min size of a hallway
	public int maxHallwayWidth = 3;
	public int minHallwayWidth = 3;
	protected Range hallwayWidth;

	public int maxHallwayLenght = 6;
	public int minHallwayLenght = 6;
	protected Range hallwayLenght;

	public int enemyDensity = 10;
	public int obsticleDendity = 5;

	//arrays and board holder
	private TileType[][] floorTiles;
	private Room[] rooms;
	private Hallway[] hallways;
	private GameObject board;

	//arrays of the prefabs
	public GameObject[] fTiles;
	public GameObject[] wTiles;
	public GameObject[] eTiles;
	public GameObject[] oTiles;

    public GameObject Player1;

	public GameObject[][] tileArr;

	// Use this for initialization
	void Start () {

		board = new GameObject ("board");
		numRooms = new Range (minRooms, maxRooms);
		roomWidth = new Range (minRoomWidth, maxRoomWidth);
		roomHeight = new Range (minRoomHeight, maxRoomHeight);
		hallwayWidth = new Range (minHallwayWidth, maxHallwayWidth);
		hallwayLenght = new Range (minHallwayLenght, maxHallwayLenght);

		setUpFloorTilesArr ();

		createRoomsAndHallways ();

		setTilesValuesForRooms ();
		setTilesValuesForHallways ();

		instantiateTiles ();

		buildGraphConnections ();

        setExplorerStart();
	}

	// Set Up the Array
	void setUpFloorTilesArr (){

		//Set Hight of array
		floorTiles = new TileType[floorHight][];
		tileArr = new GameObject[floorHight][];


		//Set a tile Enum array in each array
		for (int i = 0; i < floorHight; i++) {

			floorTiles [i] = new TileType[floorWidth];
			tileArr[i] = new GameObject[floorWidth];

		}
	}

	//create a each room and hallway
	void createRoomsAndHallways (){

		//create a array to hold the rooms
		rooms = new Room[numRooms.getRandRange()];

		//create an array to hold the Halways
		hallways = new Hallway[rooms.Length - 1];

		//create the inital room and coridor
		rooms[0] = new Room(); 
		hallways [0] = new Hallway (); 


		// Setup the first room
		rooms[0].SetupRoom(roomWidth, roomHeight, floorWidth, floorHight);

		// Setup the first hallway using the first room.
		hallways[0].SetupHallway(rooms[0], hallwayLenght, hallwayWidth, roomWidth, roomHeight, floorWidth, floorHight, true);

		for (int i = 1; i < rooms.Length; i++)
		{
			// Create a room.
			rooms[i] = new Room ();

			// Setup the room based on the previous corridor.
			rooms[i].SetupRoom (roomWidth, roomHeight, floorWidth, floorHight, hallways[i - 1]);

			//don't need a hallway for the last room
			if (i < hallways.Length)
			{
				hallways[i] = new Hallway ();

				// Setup the hallway based on the room that was just created.
				hallways[i].SetupHallway(rooms[i], hallwayLenght, hallwayWidth, roomWidth, roomHeight, floorWidth, floorHight, false);
			}
		}
	}


	//sets the tiles enum in each room to apropreate value
	void setTilesValuesForRooms ()
	{
		// Go through all the rooms
		for (int i = 0; i < rooms.Length; i++)
		{
			Room currentRoom = rooms[i];

			//go through the room's width
			for (int j = 0; j < currentRoom.width; j++)
			{
				int xCoord = currentRoom.xPos + j;

				//go through the rooms height
				for (int k = 0; k < currentRoom.height; k++)
				{
					int yCoord = currentRoom.yPos + k;

					// The coordinates in the jagged array are based on the room's position and it's width and height.
					floorTiles[xCoord][yCoord] = TileType.floor;

					int oCheck;

					oCheck = Random.Range(1, 1000);

					if (oCheck < enemyDensity) {
						floorTiles [xCoord] [yCoord] = TileType.obsticle;
					}


					int eCheck;

					eCheck = Random.Range(1, 1000);

					if (eCheck < enemyDensity) {
						floorTiles [xCoord] [yCoord] = TileType.enemy;
					}


				}
			}
		}
	}


	void setTilesValuesForHallways ()
	{
		// Go through every hallway
		for (int i = 0; i < hallways.Length; i++)
		{
			Hallway currentHall = hallways[i];

			//go through it's length
			for (int j = 0; j < currentHall.hallwayLength; j++)
			{
				// Start the coordinates at the start of the corridor.
				int xCoord = currentHall.startXPos;
				int yCoord = currentHall.startYPos;

				// Depending on the direction, add or subtract from the appropriate
				// coordinate based on how far through the length the loop is.
				switch (currentHall.direction)
				{
				case Direction.North:
					yCoord += j;
					break;
				case Direction.East:
					xCoord += j;
					break;
				case Direction.South:
					yCoord -= j;
					break;
				case Direction.West:
					xCoord -= j;
					break;
				}

				// Set the tile at these coordinates to Floor.
				floorTiles[xCoord][yCoord] = TileType.floor;
			}
		}
	}

	void instantiateTiles ()
	{
		// Go through all the tiles in the array
		for (int i = 0; i <  floorHight ; i++)
		{
			for (int j = 0; j < floorWidth; j++)
			{
				switch (floorTiles[i][j]) 
				{
				case TileType.wall:
					instantiateFromArray (wTiles, i, j, TileType.wall);
					break;

				case TileType.floor:
					instantiateFromArray (fTiles, i, j, TileType.floor);
					break;

				case TileType.enemy:
					instantiateFromArray (eTiles, i, j, TileType.enemy);
					break;

				case TileType.obsticle:
					instantiateFromArray (oTiles, i, j, TileType.obsticle);
					break;


				}

			}
		}
	}

	public void buildGraphConnections(){

		for (int i = 0; i < floorHight; i++) {

			for (int j = 0; j < floorWidth; j++) {

				Tile currTile = tileArr [i] [j].GetComponent<Tile>();

				if (i+1 < floorHight){
					currTile.setNorth (tileArr[i+1][j].GetComponent<Tile>());
				}

				if (j+1 < floorWidth) {
					currTile.setEast (tileArr[i][j+1].GetComponent<Tile>());
				}

				if (i-1 > 0) {
					currTile.setSouth (tileArr[i-1][j].GetComponent<Tile>());
				}

				if (j - 1 > 0) {
					currTile.setWest (tileArr[i][j-1].GetComponent<Tile>());
				}
			}

		}

		
	}


	void instantiateFromArray (GameObject[] prefabs, int xCoord, int yCoord, TileType ty)
	{
		// Create a random index for the array.
		int randomIndex = Random.Range(0, prefabs.Length);

		// The position to be instantiated at is based on the coordinates.
		Vector3 position = new Vector3((float) xCoord, (float) yCoord, 0f);

		// Create an instance of the prefab from the random index of the array.
		GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

		// Set the tile's parent to the board holder.
		tileInstance.transform.parent = board.transform;

		tileInstance.GetComponent<Tile> ().setType(ty);
		tileArr [xCoord] [yCoord] = tileInstance;

	}

    private void setExplorerStart(){
        int x = rooms[0].xPos;
        int y = rooms[0].yPos;
        
        GameObject theCam = GameObject.FindGameObjectWithTag("MainCamera");
        theCam.GetComponent<CameraFollow>().StartHere(x,y);

        

        Vector2 toStart = new Vector2(x,y);
        Player1.GetComponent<PlayerMovement>().SetStartingPosition(toStart);
    }
 

}