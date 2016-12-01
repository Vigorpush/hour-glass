#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
//using C5;




/// <summary>
/// WHAT AIs need to know... threat list of targets, concernList of 
/// 
/// </summary>
public class BattleMasterAI : MonoBehaviour {
	 //KNOWLEDGE
	public enum ROLE{
		 WARRIOR =1,
	     MAGE    =2,
		 ARCHER  =3

	}
	//
	ArrayList enemies; 
	TileGrid MAP_TILE = new TileGrid();
	GameObject[][] tempMap;
	FearMap fearTable = new FearMap (50,50);
	FearMap proximityTable = new FearMap (50,50);

	ArrayList players;
	public int[] damageDone;
	public int[] healingDone;
	//GraphMap map;
	// Use this for initialization
	//POPULATING THE MAP
	void Start () {
	
	}

	public ROLE ClassifyHeroClasses(ArrayList target){

		return ROLE.WARRIOR;
	}
	public void AnalyzeRoom(){
		players.Add(GameObject.FindGameObjectWithTag ("Player 1").GetComponent<HeroUnit>());
		players.Add(GameObject.FindGameObjectWithTag ("Player 2").GetComponent<HeroUnit>());
		players.Add(GameObject.FindGameObjectWithTag ("Player 3").GetComponent<HeroUnit>());
		ClassifyHeroClasses (players);
	}

		
	//Calculates the 
	void GenerateThreatMap(){
		//fearTable;


	}
	void GenerateFriendlyProximityMap(){

	}

	//Create a table of a monster's dmg and heal output
	void initMonsterStats(){
		damageDone = new int[enemies.Count];
		healingDone = new int[enemies.Count];
		for (int i = 0; i < enemies.Count; i++) {
			damageDone [i] = 0;
			healingDone [i] = 0;
		}
		Debug.Log ("Initializing all monster stats to 0.");

	}
	//Send a message here?? 
	void checkFightOver(){
	}
	void logMonsterDamage(EnemyUnit u,int amount){
		damageDone [enemies.IndexOf (u)] += amount;
	}
	void logMonsterHealing(EnemyUnit u,int amount){
		healingDone [enemies.IndexOf (u)] += amount;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
#endif