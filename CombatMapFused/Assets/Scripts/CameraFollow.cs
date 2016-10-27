using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	public float CAMERA_SPEED=10000000000f;
    private GameObject player;
    private Vector3 offset;
	public bool turnActive;


    // Use this for initialization
    void Start()
    {
		setActive (false);
		//NEED TO MAKE THIS SET WHEN the calculate turn is called
        player = GameObject.FindGameObjectWithTag("Player1");
        offset = transform.position - player.transform.position;
    }

	void Update(){
		if (turnActive) {
			
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position+ new Vector3(0,0,-1), Time.deltaTime * CAMERA_SPEED);
		
		}
	}
	public void SetCameraFollow(GameObject targetPlayer){
		if (targetPlayer == null) {
			player = targetPlayer;
		}
		if (targetPlayer != player) {
			player = targetPlayer;
			setActive (true);
		} else {

			setActive (false);
			Invoke ("activate", 1f);
		}
		transform.position = new Vector3 (transform.position.x, transform.position.y, -1);
	}

    void LateUpdate()
    {
		
    }
	void setActive(bool status){
		turnActive = status;
	}
	void activate(){
		turnActive = true;
	}

}