using UnityEngine;
using System.Collections;

public interface FriendlyAware  {
	
	GameObject lowestHpFriend{ get; set;}
	ArrayList friends{ get; set;}
	FearMap safetyMap{ get; set;}
	void setLowestHealthFriend (); 
	void EmitPositiveMorale(); // if hasmorale sendMessage(HealMorale,amount)
}
