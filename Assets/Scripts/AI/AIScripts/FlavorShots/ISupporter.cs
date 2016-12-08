using UnityEngine;
using System.Collections;

public interface ISupporter {
	float kindness{ get; set;}  // Likelyhood of performing a selfless action
	GameObject lowestHpFriend{ get; set;}
	GameObject mostDeservingFriend{ get; set;}
	GameObject[] friends{ get; set;}
	bool kindnessRoll(); // Returns if will be kind
	void castHeal();
	DecisionTree supportTree{ get; set;}
	DecisionTree buildSupportTree();






}
