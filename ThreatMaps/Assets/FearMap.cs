using UnityEngine;
using System.Collections;
using System;

public class FearMap : MonoBehaviour {
	int[,] tiles;
	int MAP_H,MAP_W;
	FearSource[] sources;
	//TODO: IF PROCESSING THIS LAGS THE GAME, ADD MEMORY FOR THE THREAT SOURCES ON THE MAP AND REMOVE THEM USING AN INVERSE OPERATIONs
	public FearMap(int mapW,int mapH){
		MAP_H=mapH;
		MAP_W = mapW;
		tiles = new int[mapW, mapH];
	}

	public FearMap(int mapW,int mapH,FearSource[] fearSources ){
		sources = fearSources;
		tiles = new int[mapW,mapH];
	}

	public void initialize(){
		for (int i = 0; i < MAP_W; i++) {
			for (int j = 0; j < MAP_H; j++) {
				tiles [i,j] = 0;
			}
		}

	}

	public void displayMap(){
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

		Debug.Log(Display);


	}
	//Returns tree if the provided point is in bounds
	bool checkBounds(int Yval,int Xval){
		if (Xval < 0 || Xval > MAP_W - 1 || Yval < 0 || Yval > MAP_H - 1) {
			return false;
		}
		return true;

		

	}
	//Add fear values that do not radiate  (MAGES,ARCHERS)
	public void addFixedSource(FearSource source,int x, int y){
		int powerVert = 1;
		int powerHori = 1;
		int printhelp = 0;
		int r = source.radius;
		int str = source.strength;
		//Offset to be change if +- 1 is easier 
		int offset = 1;
		int adjustedX = x - offset;
		int adjustedY = y - offset;
		int jTime = 0;
		int curNumberOfRows; 
		//FOR FEAR POINTS (TILES THAT ARE SCARY)
		if (r == 0) {
			if(checkBounds(y,x)){
				tiles[y,x] +=str;
			}
			tiles[y,x] +=str;
			return;
		}
		//OUTER LOOP RUNS 2x range + 1 times
		for (int i = 1; i != source.radius * 2+2; i++) {
			
		
			//(2 + 2 * r - Math.Abs (r - (2 * i - 3 - r) - 1))    Is the formula for those sexy diamonds
			curNumberOfRows = (2 + 2 * r - Math.Abs (r - (2 * i - 3 - r) - 1));
			for (int j = 1; j != (curNumberOfRows); j++) {
				//Locations depend on i
				int newX = x - (r - Math.Abs (r - i + 1))+j;
				int newY = (y + r - i + 1);

				jTime++;

				//The proximity of the tile affects the fear
				//powerVert = r + 1 - i;
				//powerHori =;
				/*	
				if (i < source.radius + 1) {
				} else {
				}
				//Increase for first half
				if (j < i+1) {
				}
				//Past the halfway point start reducing
				else {
				}
				*/			
				//THE FORM INEED  IS x needed -r(r
				if(checkBounds(newX,newY)){
					printhelp++;
					tiles[newY,newX] +=str;
				}
			}
		}
		Debug.Log (jTime + "count J");
		Debug.Log (printhelp + "Tiles affected");
		//Restart row power
		powerHori = 0;
	}

	public void addDecayingSource(FearSource source,int x, int y){
		//For trouble shooting amount of tiles hit
		if (!checkBounds (x, y)) {
			throw new IndexOutOfRangeException ("You can't spawn a source outside of the map!");
		}
		int printhelp = 0;
		int r = source.radius;
		int str = source.strength;
		//Multipliers for proximity
		//Full threat is applied to the center  1/2 is applied since there are 2 dimensions and a maximum fear must add to 1
		float powerY,powerX;
		float increment = 1f/(float)(r);
		//Offset to be change if +- 1 is easier 
		int offset = 1;
		int adjustedX = x - offset;
		int adjustedY = y - offset;
		Debug.Log ("increment is" + increment);
		int jTime = 0;
		int curNumberOfRows; 
		//FOR FEAR POINTS (Individual TILES THAT ARE SCARY)
		if (r == 0) {
			if(checkBounds(y,x)){
				tiles[adjustedY,adjustedX] +=str;
			}
			tiles[y,x] +=str;
			return;
		}
		//OUTER LOOP RUNS 2x range + 1 times

		for (int i = 1; i != iLoopCount(r); i++) {
			

			//(2 + 2 * r - Math.Abs (r - (2 * i - 3 - r) - 1))    Is the formula for those sexy diamonds
			curNumberOfRows = jLoopCount(r,i);
			for (int j = 1; j != (curNumberOfRows); j++) {
				
			


				//A debugging variable
				jTime++;

		
				//Locations depend on i

				//int newX = adjustedX - (r - Math.Abs (r - i + 1))+j;
				int newX = adjustedX - (r - Math.Abs (r - i + 1))+j;
				int newY = (adjustedY + r - i + 1);
				if(checkBounds(newX,newY)){
					//The proximity of the tile affects the fear
					//The powers are a function of the distance from the center
					int differenceX = Math.Abs((adjustedX+1)-newX);
					int differenceY = Math.Abs (adjustedY- newY);

					powerY = 1f - differenceY*increment;
					powerX = 1f - differenceX*increment;

					Debug.Log("The difference in x is:" + differenceX + "this results in an x power of:" + powerX.ToString("0.00") );
					Debug.Log("The difference in y is:" + differenceY + "this results in a y power of:" + powerY.ToString("0.00") );
					printhelp++;
					//Rounds the value to an integer for testing... may use floats in the real game for more accurate fear measurements 
					int alteredStrength = (int)Math.Round(((str)*(powerY+powerX)));
					//Makes a 0 strength zone become 1 for testing, possibly valid for the game itself
					if (alteredStrength == 0) {
						alteredStrength = 1; 
					}

					tiles [newY, newX] += alteredStrength;
					Debug.Log ("Adding a point with str: " + alteredStrength);

					this.displayMap ();
				}
			}
		}
		Debug.Log (jTime + "count J");
		Debug.Log (printhelp + "Tiles affected");
		//Restart row power
	}


	//Returns the number of times the inner loop for,adding sources, will run.
	private int jLoopCount(int r, int i){
		return 2 + 2 * r - Math.Abs (r - (2 * i - 3 - r) - 1);

	}
	//Returns the number of times the outer loop for,adding sources, will run.
	private int iLoopCount(int r){
		return r* 2+2;

	}
	// Use this for initialization
		void Start () {
		

		FearSource Mage22 = new FearSource ();
		Mage22.strength = 2;
		Mage22.radius = 2;
		FearSource Mage53 = new FearSource ();
		FearSource Mage70 = new FearSource ();
		Mage70.radius = 0;
		Mage70.strength = 7;
		Mage53.strength = 9;
		Mage53.radius = 2;
		FearMap testMap = new FearMap (3, 3);
		FearMap testMap2 = new FearMap (10, 10);

		testMap2.initialize ();
		testMap2.displayMap ();
		Debug.Log("Adding 5str,3rad mage to 1,1");
		testMap2.addFixedSource (Mage53, 1,1 );
		testMap2.displayMap ();
		Debug.Log("Adding 2str,2rad mage to 6,6 , that decays");
			testMap2.addDecayingSource (Mage22, 6, 6);
		Debug.Log("Adding 2str,2rad mage to 9,9");
		testMap2.addDecayingSource(Mage22, 9,9 );
		testMap2.displayMap ();



		//testMap2.displayMap ();



	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
