using UnityEngine;
using System.Collections;

public class RoomBuilderAI {
	enum mods{
		BULKY = 0,
		RAGING = 1,
		FEARLESS =2
	}
	//VARIABLES PUBLIC FOR TESTING
	public float difficultyFactor;
	public 
	int dungeonDepth;
	TileGrid currentRoom;
	float maxCredits;
	float curCredits;

	public RoomBuilderAI(int startlevel,TileGrid room){
		currentRoom = room;
		dungeonDepth = startlevel;
	}
	void Start () {
	
	}

	void replaceRoom(TileGrid newRoom){


	}
	float calculateInitCredits(){
		return difficultyFactor
	}
	TileGrid provideFinishedRoom(){


	}
	//void placeRandom(){
		
	//}	
	public void 
	// Update is called once per frame

}
