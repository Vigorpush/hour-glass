using UnityEngine;
using System.Collections;
using C5;

public class BattleMasterAI : MonoBehaviour {
	 //KNOWLEDGE
	public enum ROLE{
		 WARRIOR =1,
	     MAGE    =2,
		 ARCHER  =3

	}
	TileGrid MAP_TILE = new TileGrid();
	FearMap fearTable = new FearMap (50,50);

	ArrayList<HeroUnit> players;
	//GraphMap map;
	// Use this for initialization
	//POPULATING THE MAP
	void Start () {
	
	}

	public ROLE ClassifyHeroClasses(HeroUnit[] target){

		return ROLE.WARRIOR;
	}
	public void AnalyzeRoom(){
		players.Add(GameObject.FindGameObjectWithTag ("Player 1").GetComponent<HeroUnit>());
		players.Add(GameObject.FindGameObjectWithTag ("Player 2").GetComponent<HeroUnit>());
		players.Add(GameObject.FindGameObjectWithTag ("Player 3").GetComponent<HeroUnit>());
		ClassifyHeroClasses (players.ToArray());
	}
	//Calculates the 
	void GenerateThreatMap(){
		

	}
	// Update is called once per frame
	void Update () {
		
	}
}
