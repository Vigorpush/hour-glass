using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	public float CAMERA_SPEED=10000000000f;
    public GameObject player;
    public Vector3 offset;
	public bool turnActive;
    public bool inCombat;
    public bool endCombat;
    private bool invokeBuffer;
    private bool targettingCam;
    private GameObject target;

    // Use this for initialization
    void Start()
    {
        invokeBuffer = false;
        targettingCam = false;
        endCombat = true;
		setActive (false);
		//Initially main character
        player = GameObject.FindGameObjectWithTag("Player1");
        offset = transform.position - player.transform.position;
    }

	void Update(){
        if (!targettingCam  && player != null)
        {
            if (turnActive && !inCombat && !invokeBuffer)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0, 0, -1), Time.deltaTime * CAMERA_SPEED);
            }
            if (inCombat && transform.position.z < -0.7f && !endCombat)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.3f), Time.deltaTime * .5f);
            }
            if (inCombat && endCombat && transform.position.z > -1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.3f), Time.deltaTime * .5f);
            }
        } else if(player!=null)//target selection camera
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position + new Vector3(0, 0, -1), Time.deltaTime * CAMERA_SPEED);
        }

    }

    public void SetCombatZoom()
    {
       // Debug.Log("Camera says message received");
        inCombat = true;
        endCombat = false;
        //Debug.Log("Camera zooming in");
        //transform.Translate(0f,0f,.2f); 
       
    }


    public void StartHere(int xIn, int yIn)
    {
        this.gameObject.transform.position = new Vector3(xIn,yIn,0);
    }

    public void UnsetCombatZoom()
    {
        endCombat = true;
        invokeBuffer = true;
        //transform.Translate(0f, 0f, -.2f);
        Invoke("NewTurnBuffer",1f);
        //Debug.Log("Camera zooming out");
    }

    public void NewTurnBuffer()
    {
        invokeBuffer = false;
        inCombat = false;
       // Debug.Log("OUt of COmbat");
    }

	public void SetCameraFollow(GameObject targetPlayer){
        inCombat = false;
        //Debug.Log("OUt of COmbat");
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

    public void SetTargettingCam(GameObject targetUnit)
    {
        targettingCam = true;
        target = targetUnit;
    }

    public void UnsetTargettingCam()
    {
        targettingCam = false;
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