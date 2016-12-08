using UnityEngine;
using System.Collections;

public class Magnet : MonoBehaviour {
	public float str;
	public Vector2 playerDirection;
	private TurnManager manager;
	private Rigidbody2D rb;
	public float triggerRange = 1;


	// Use this for initialization
	void Start () {
		if (str == 0) {
			str = triggerRange;
		}
		manager =(TurnManager) GameObject.FindGameObjectWithTag ("Manager").GetComponent<TurnManager>();
		rb = gameObject.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		if (manager.theExplorer != null) {
			

			float Xdif = manager.theExplorer.transform.position.x - transform.position.x;
			float Ydif = manager.theExplorer.transform.position.y - transform.position.y;
			playerDirection= new Vector2 (Xdif, Ydif);
			//Debug.Log ("Pushing");
			if (playerDirection.magnitude < triggerRange) {
				SendMessage ("rollAnUpgrade");
				Debug.Log ("Close enough for looting.");
			}
			rb.AddForce (playerDirection.normalized * str);
		}
	}
}
