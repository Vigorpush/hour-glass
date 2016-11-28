using UnityEngine;
using System.Collections;

public interface IRanged  {
	int attackDamage{ get; set; }
	int range{ get; set;}
	void rangedAttack();
	 
}
