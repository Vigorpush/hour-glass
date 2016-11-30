using UnityEngine;


public class Room
{

	public int xPos;
	public int yPos;
	public int width;
	public int height;
	public Direction enteringHallway;

	//Used for setting up the first room
	public void SetupRoom (Range widthRange, Range heightRange, int floorWidth, int floorHeight)
	{
		// Set a random width and height.
		width = widthRange.getRandRange();
		height = heightRange.getRandRange();

		// Set the x and y coordinates so the room is roughly in the middle of the board.
		xPos = Mathf.RoundToInt(floorWidth / 2f - width / 2f);
		yPos = Mathf.RoundToInt(floorHeight / 2f - height / 2f);
	}

	public void SetupRoom (Range widthRange, Range heightRange, int floorWidth, int floorHeight, Hallway hallway)
	{
		// Set the entering corridor direction.
		enteringHallway = hallway.direction;

		// Set random values for width and height.
		width = widthRange.getRandRange();
		height = heightRange.getRandRange();

		//Switch Based on the entering Hallway
		switch (hallway.direction)
		{
	
		case Direction.North:

			//ensure it doesn't go too far north
			height = Mathf.Clamp(height, 1, floorHeight - hallway.endPositionY); 
			yPos = hallway.endPositionY;

			//The x coordinate must match the incoming hallway and not go off board
			xPos = Random.Range (hallway.endPositionX - width + 1, hallway.endPositionX);
			xPos = Mathf.Clamp (xPos, 0, floorWidth - width);
			break;

		case Direction.East:
			//ensure it doesn't go too far east 
			width = Mathf.Clamp(width, 1, floorWidth - hallway.endPositionX);
			xPos = hallway.endPositionX;

			// y coordinate must match incoming hallway and not go off board
			yPos = Random.Range (hallway.endPositionY - height + 1, hallway.endPositionY);
			yPos = Mathf.Clamp (yPos, 0, floorHeight - height);

			break;

		case Direction.South:
			//ensure it doesn't go too far south
			height = Mathf.Clamp (height, 1, hallway.endPositionY);
			yPos = hallway.endPositionY - height + 1;

			//x coordinate must match hallway and not go off the board
			xPos = Random.Range (hallway.endPositionX - width + 1, hallway.endPositionX);
			xPos = Mathf.Clamp (xPos, 0, floorWidth - width);

			break;

		case Direction.West:
			//ensure it doesn't go too far west
			width = Mathf.Clamp (width, 1, hallway.endPositionX);
			xPos = hallway.endPositionX - width + 1;

			//y coordinate must match and not go off the board
			yPos = Random.Range (hallway.endPositionY - height + 1, hallway.endPositionY);
			yPos = Mathf.Clamp (yPos, 0, floorHeight - height);

			break;
		}
	}
}


