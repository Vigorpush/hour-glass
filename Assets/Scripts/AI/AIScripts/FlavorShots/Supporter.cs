using UnityEngine;
using System.Collections;

public interface Supporter {
	float kindness{ get; set;}  // Likelyhood of performing a selfless action
	GameObject lowestHpFriend{ get; set;}
	GameObject mostDeservingFriend{ get; set;}
	ArrayList friends{ get; set;}
	void setLowestHealthFriend ();
	void setMostDeserving();
	DecisionTree buildSupportTree();





}
