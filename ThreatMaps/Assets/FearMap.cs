using UnityEngine;
using System.Collections;
using System;

public class FearMap : MonoBehaviour
{
	const int PLACEMENT_OFFSET = 1;	
	int[,] tiles;
	int MAP_H, MAP_W;
	FearSource[] sources;
	//TODO: IF PROCESSING THIS LAGS THE GAME, ADD MEMORY FOR THE THREAT SOURCES ON THE MAP AND REMOVE THEM USING AN INVERSE OPERATIONs
	//Construct the array
	public FearMap (int mapW, int mapH)
	{
		MAP_H = mapH;
		MAP_W = mapW;
		tiles = new int[mapW, mapH];
	}

	public FearMap (int mapW, int mapH, FearSource[] fearSources)
	{
		sources = fearSources;
		tiles = new int[mapW, mapH];
	}
	//Run once at the beginning of a fight 
	public void initialize ()
	{
		for (int i = 0; i < MAP_W; i++) {
			for (int j = 0; j < MAP_H; j++) {
				tiles [i, j] = 0;
			}
		}
	}

	public void displayMap ()
	{
		string Display = "";
		int jC = 0;
		for (int i = 0; i < MAP_W; i++) {
			Display += "\n";

			for (int j = 0; j < MAP_H; j++) {
				
				Display += tiles [j, i];
				//Debug.Log((tiles [i,j].ToString()));
			}
			jC++;
			Display += "X row: " + jC;
		}

		Debug.Log (Display);


	}
	//Returns tree if the provided point is in bounds
	bool checkBounds (int Yval, int Xval)
	{
		if (Xval < 0 || Xval > MAP_W - 1 || Yval < 0 || Yval > MAP_H - 1) {
			return false;
		}
		return true;
	}
	//Add fear values that do not radiate  (MAGES,ARCHERS)
	public void addFixedSource (FearSource source, int x, int y)
	{	
		/*
		int powerVert = 1;
		int powerHori = 1;
		*/
		int printhelp = 0;
		int r = source.radius;
		int str = source.strength;
		//Offset to be change if +- 1 is easier 
		int adjustedX = x - PLACEMENT_OFFSET;
		int adjustedY = y - PLACEMENT_OFFSET;
		int jTime = 0;
		int curNumberOfRows; 
		//FOR FEAR POINTS (TILES THAT ARE SCARY)
		if (r == 0) {
			if (checkBounds (y, x)) {
				tiles [adjustedY, adjustedX] += str;
				return;
			}


		}
		//OUTER LOOP RUNS 2x range + 1 times
		for (int i = 1; i != source.radius * 2 + 2; i++) {
			
			//(2 + 2 * r - Math.Abs (r - (2 * i - 3 - r) - 1))    Is the formula for those sexy diamonds
			curNumberOfRows = (2 + 2 * r - Math.Abs (r - (2 * i - 3 - r) - 1));
			for (int j = 1; j != (curNumberOfRows); j++) {
				//Locations depend on i
				int newX = x - (r - Math.Abs (r - i + 1)) + j;
				int newY = (y + r - i + 1);

				jTime++;

				//THE FORM INEED  IS x needed -r(r
				if (checkBounds (newX, newY)) {
					printhelp++;
					tiles [newY, newX] += str;
				}
			}
		}
		Debug.Log ("The inner loop ran this many times: " + jTime);
		Debug.Log (printhelp + "Tiles affected");
		//Restart row power

	}
	//source -> The source of fear
	// x, y -> coordinates of the center point
	//decayFactor ->  How fast the fear falls off
	public void addDecayingSource (DecayingFearSource source, int x, int y)
	{
		//For trouble shooting amount of tiles hit
		if (!checkBounds (x, y)) {
			throw new IndexOutOfRangeException ("You can't spawn a source outside of the map!");
		}
		int printhelp = 0;
		int r = source.radius;
		int str = source.strength;
		//Multipliers for proximity
		//Full threat is applied to the center  1/2 is applied since there are 2 dimensions and a maximum fear must add to 1
		float powerY, powerX;
		float decayFactor = source.decayFactor;
		float increment = decayFactor / (float)(r);
		//Offset to be change if +- 1 is easier 
		int offset = 1;
		int adjustedX = x - PLACEMENT_OFFSET;
		int adjustedY = y - PLACEMENT_OFFSET;
		Debug.Log ("increment is" + increment);
		int jTime = 0;
		int curNumberOfRows; 
		//FOR FEAR POINTS (Individual TILES THAT ARE SCARY)
		if (r == 0) {
			if (checkBounds (y, x)) {
				tiles [adjustedY, adjustedX] += str;
				return;
			}

		}
		//OUTER LOOP RUNS 2x range + 1 times

		for (int i = 1; i != iLoopCount (r); i++) {
			//(2 + 2 * r - Math.Abs (r - (2 * i - 3 - r) - 1))    Is the formula for those sexy diamonds
			curNumberOfRows = jLoopCount (r, i);
			for (int j = 1; j != (curNumberOfRows); j++) {
				
				//A debugging variable
				jTime++;

		
				//Locations depend on i

				//int newX = adjustedX - (r - Math.Abs (r - i + 1))+j;
				int newX = adjustedX - (r - Math.Abs (r - i + 1)) + j;
				int newY = (adjustedY + r - i + 1);
				if (checkBounds (newX, newY)) {
					//The proximity of the tile affects the fear
					//The powers are a function of the distance from the center
					int differenceX = Math.Abs ((newX - 1 - adjustedX));
					int differenceY = Math.Abs (newY - adjustedY);
					powerY = 1f - differenceY * increment;
					powerX = 1f - differenceX * increment;

					//Debug.Log("The difference in x is:" + differenceX + "this results in an x power of:" + powerX.ToString("0.00") );
					//Debug.Log("The difference in y is:" + differenceY + "this results in a y power of:" + powerY.ToString("0.00") );
					printhelp++;
					//Rounds the value to an integer for testing... may use floats in the real game for more accurate fear measurements 
					int alteredStrength = (int)Math.Round (((str) * (powerY + powerX)));
					//Makes a 0 strength zone become 1 for testing, possibly valid for the game itself
					if (alteredStrength <= 0) {
						alteredStrength = 1; 
					}

					tiles [newY, newX] += alteredStrength;
					//Debug.Log ("Adding a point with str: " + alteredStrength);
						
					//this.displayMap ();
				}
			}

		}
		Debug.Log ("Final Result: ");
		Debug.Log (jTime + "count J");
		Debug.Log (printhelp + "Tiles affected");
		//Restart row power
	}


	//Returns the number of times the inner loop for,adding sources, will run.
	private int jLoopCount (int r, int i)
	{
		//return 2 + 2 * r - Math.Abs (r - (2 * i - 3 - r) - 1);
		return 2 + 2 * r - Math.Abs (r - (2 * i  - r)+2);

	}
	//Returns the number of times the outer loop for,adding sources, will run.
	private int iLoopCount (int r)
	{
		return r * 2 + 2;

	}
	// Use this for initialization
	void Start ()
	{
		

		DecayingFearSource Mage44DecayHigh = new DecayingFearSource (4,4,1.5f);

		DecayingFearSource Mage44DecayLow = new DecayingFearSource (4,4,.5f);
		FearSource Mage53= new FearSource (5,3);
		FearSource Mage70 = new FearSource (0,7);
		FearMap testMap = new FearMap (3, 3);
		FearMap testMap2 = new FearMap (10, 10);
		//Regular sources
		testMap2.initialize ();
		testMap2.displayMap ();
		Debug.Log ("Adding 3str,5rad mage to 4,4, NO DECAY");
		testMap2.addFixedSource (Mage53,4,4);
		testMap2.displayMap ();
		testMap2.initialize ();
		//Testing Decaying sources
		Debug.Log ("Adding 4str,4rad mage to 5,5 , that decays a lot");
		testMap2.addDecayingSource (Mage44DecayHigh, 5, 5);
		testMap2.displayMap ();
		testMap2.initialize ();
		Debug.Log ("Adding 4str,4rad mage to 5,5 , that decays a little");
		testMap2.addDecayingSource (Mage44DecayLow, 5, 5);
		testMap2.displayMap ();
		testMap2.initialize ();

		Debug.Log ("Adding a fear tile of strength 7 to 1,1 NO DECAY");
		testMap2.addFixedSource (Mage70, 1, 1);
		testMap2.displayMap ();



		//testMap2.displayMap ();



	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
