using UnityEngine;
using System.Collections;

public class VoidEaterAI : ZombieAI {
	

	new protected void DecideAttackTarget(){
		attackTarget =getWeakestPlayer ();
		//While cannot target unit
		//if no closes
		if (attackTarget == null) {
			attackTarget = (GameObject)players [Random.Range (0, 2)];
			Debug.Log ("-----------------------Choosing a random target");
		}


	}

	new void FixedUpdate()
	{   
		//   root.Search(root);
		//Debug.Log ("Movvement cd : " +movementCooldown.ToString());
		//Debug.Log ("Void eater move target : " +moveTarget.ToString());
		//  Debug.Log(this.gameObject.name + " I have this many moves left: " + movesLeft);
		//Debug.Log(desiredCoord.ToString());
		if(isStillTurn && !base.movementCooldown){
			//Debug.Log ("I want to go to " + desiredCoord.ToString ());
			//Debug.Log ("I am at: " + myTf.position);
			myTf.position = Vector2.MoveTowards(myTf.position, desiredCoord, Time.fixedDeltaTime * speed);

		}
		if (((Vector2)myTf.position).Equals(desiredCoord)) {
			//Debug.Log ("GOOOOOOOOOOOOOOOOOOOD");
			if (movesLeft > 0) {
				base.StepTowardsTarget ();
			} else if (attacksLeft > 0 && base.atMoveTarget()) {

				ExecuteMeleeAttackOnTarget();

				SendMessage ("Suicide");
				//Invoke ("Suicide",4f);
			}
			else if (movesLeft <= 0 && isStillTurn)
			{

				isStillTurn = false;

				base.EndMyTurn();

			}


		} 

	}

	protected void Suicide(){
		//Debug.Log ("Killing myself for a greater cause.");
		isStillTurn = false;
		myStats.takeDamage (int.MaxValue, this.gameObject);
        base.EndMyTurn();


    }
	new protected void DecideMoveTarget (){
		moveTarget = attackTarget.transform.position;


	}


	protected GameObject getWeakestPlayer(){
		int bestIndex = 0;
		for(int i=0;i<3;i++){
			int lowestHp = int.MaxValue;
			int cur =((GameObject)base.players.ToArray()[i]).GetComponent<HeroUnit>().curhp;
			if((cur < lowestHp && cur > 0)){
				lowestHp = cur;
				bestIndex = i;
			}

		}
		if ((GameObject)players.ToArray () [bestIndex] == null) {
			Debug.Log ("target is null");
		}
		return (GameObject)players.ToArray() [bestIndex];
		
	}


}
