using UnityEngine;


public class Hallway
{

	public int startXPos;         // The x coordinate for the start of the corridor.
	public int startYPos;         // The y coordinate for the start of the corridor.
	public int hallwayLength;            // How many units long the corridor is.
	public int hallwayWidth; 			// How many units wide the hallway is. 
	public Direction direction;   // Which direction the corridor is heading from it's room.

	//Find end coordinates based on the start coordinates and the size
	public int endPositionX
	{
		get
		{
			if (direction == Direction.North || direction == Direction.South)
				return startXPos;
			if (direction == Direction.East)
				return startXPos + hallwayLength - 1;
			return startXPos - hallwayLength + 1;
		}
	}


	public int endPositionY
	{
		get
		{
			if (direction == Direction.East || direction == Direction.West)
				return startYPos;
			if (direction == Direction.North)
				return startYPos + hallwayLength - 1;
			return startYPos - hallwayLength + 1;
		}
	}


	public void SetupHallway (Room room, Range length, Range width, Range roomWidth, Range roomHeight, int floorWidth, int floorHeight, bool firstHallway)
	{
		// Set a random direction
		direction = (Direction) Random.Range(0, 4);

		// Set a random length.
		hallwayLength = length.getRandRange();

		//set a random width.
		hallwayWidth =  width.getRandRange();

		// Create a cap for how long the length can be
		int maxLength = length.getMax();

		switch (direction)
		{

		case Direction.North:
			// starting x must be within the range of the size of the room
			startXPos = Random.Range (room.xPos, room.xPos + room.width - 1);

			// The starting position in the y must be top of room
			startYPos = room.yPos + room.height;

			// cant go off the top
			maxLength = floorHeight - startYPos - roomHeight.getMin();

			break;
		
		case Direction.East:
			//starting x must be right of room
			startXPos = room.xPos + room.width;

			//Starting y must be in the right of the room 
			startYPos = Random.Range(room.yPos, room.yPos + room.height - 1);

			//can't go off the right side 
			maxLength = floorWidth - startXPos - roomWidth.getMin();

			break;

		case Direction.South:
			//start x must be on the bottom of the room 
			startXPos = Random.Range (room.xPos, room.xPos + room.width);

			//starging y must be bottom of room
			startYPos = room.yPos;

			//cant go off the bottom
			maxLength = startYPos - roomHeight.getMin();

			break;

		case Direction.West:
			//starging x must be on the left side of the room
			startXPos = room.xPos;

			//starting y must be on the left side of ther room
			startYPos = Random.Range (room.yPos, room.yPos + room.height);

			//can't go off the left
			maxLength = startXPos - roomWidth.getMin();

			break;
		}

		// We clamp the length of the hallway to make sure it doesn't go off the board.
		hallwayLength = Mathf.Clamp (hallwayLength, 1, maxLength);
	}
}

