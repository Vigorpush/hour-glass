using UnityEngine;
using System.Collections;

public class ShamanAI : MonoBehaviour, ISupporter, IRangeAttack {
	#region IRangeAttack implementation
	public GameObject bestFriend;
	public GameObject hurtFriend;
	public GameObject[] allFriends;
	public float myKindness;
	public DecisionTree mySupportTree;

	public DecisionTree supportTree {
		get {
			return mySupportTree;
		}
		set {
			mySupportTree = value;
		}
	}

	void Start(){
		
		friends = GameObject.FindGameObjectsWithTag ("Baddy");
	}
	public DecisionTree buildRangedTree ()
	{
		throw new System.NotImplementedException ();
	}
	public bool kindnessRoll (){
		float roll = Random.Range(0f, 1f);
		if (myKindness < roll) {
			return true;
		} else
			return false;
	}
	#endregion

	#region ISupporter implementation




	public DecisionTree buildSupportTree ()
	{
		if ( supportTree != null) {
			return supportTree;

		}
		DecisionTree healNeeded = supportTree;
		//If lowestHpUnitFullHp... return value from the target find??
		//healNeeded.setDecision ();
		DecisionTree healFriendly = new DecisionTree ();
		DecisionTree areaAttack = new DecisionTree ();

		//tryAttack.setAction (tryMeleeAttackDumb);
		DecisionTree doNothing = new DecisionTree ();
		//doNothing.setAction (StayStill);
		//canMove.addChild (doNothing); // First child = offset 0 = false

		//canMove.addChild (tryAttack); // True//



		return  healNeeded;
	}

	public void castHeal ()
	{
		
	}


	public float kindness {
		get {
			return myKindness;
		}
		set {
			myKindness = value;
		}
	}

	public DecisionTree AreaAttack(){
		return new DecisionTree ();

	}
	public GameObject lowestHpFriend {
		get {
			return hurtFriend;
		}
		set {
			int lowestHealth = int.MaxValue;
			foreach(GameObject tar in friends){
				if (tar.GetComponent<Unit> ().getcurhp() < lowestHealth) {
					hurtFriend= tar;
					lowestHealth = tar.GetComponent<Unit> ().getcurhp();
				}
			}
				
		}
	}
	public int healNeeded(){
		return 0;
	}
	public GameObject mostDeservingFriend {
		get {
			return bestFriend;
		}
		set {
			float creditVal = 0;
			foreach(GameObject tar in friends){
				if (tar.GetComponent<EnemyUnit> ().creditValue > creditVal) {
					bestFriend= tar;
					creditVal = tar.GetComponent<EnemyUnit>().creditValue;
				}
			}

		}
	}

	public GameObject[] friends {
		get {
			return allFriends;
		}
		set {
			allFriends = GameObject.FindGameObjectsWithTag ("Baddy");
		}
	}
}
	#endregion