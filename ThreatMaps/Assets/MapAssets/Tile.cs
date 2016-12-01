using UnityEngine;
using System.Collections;

//using TileTypes;
//Tiles for the map
namespace TileInfo
{
	[System.Serializable] 
	public class Tile
	{
		private int typeCode;
		private bool occupied;
		// Can the tile be walked onto?
		public GameObject occupant;
		//MAYBE USE TEXTURE HERE
		public GameObject visual;

		// Use this for initialization
		//Marks the tile to signify that it is within range
		void highLight ()
		{
			//visual.GetComponent<SpriteRenderer> ().color 
		}
		void awake ()
		{
		

		}

		void Start ()
		{
	
		}

	
		// Update is called once per frame
		void Update ()
		{
	
		}

	}

}