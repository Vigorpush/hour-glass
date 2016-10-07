using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private Transform tf;
    private float speed;
    private Vector3 pos;
    public float h;
    private Animator anim;

	// Use this for initialization
	void Start () {
        tf = GetComponent<Transform>();
        speed = 4;
        pos = transform.position;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //bool shoot = Input.GetButtonDown("Jump");

        h = Input.GetAxis("Horizontal");
        //Vector2 vectorHorizontal = new Vector2(h, 0);

        if (Input.GetButtonDown("Horizontal")) {         
            if (Input.GetKeyDown("right")) {
                Debug.Log("Move right");
                pos += Vector3.right;
                anim.SetTrigger("Walk Right");
            }
            else if (Input.GetKeyDown("left"))
            {
                Debug.Log("Move left");
                pos += Vector3.left;
                anim.SetTrigger("Walk Left");
            }
        }
        else if (Input.GetButtonDown("Vertical")) {
            if (Input.GetKeyDown("up"))
            {
                Debug.Log("Move Up");
                pos += Vector3.up;
                anim.SetTrigger("Walk Up");
            }
            else if (Input.GetKeyDown("down"))
            {
                Debug.Log("Move down");
                pos += Vector3.down;
                anim.SetTrigger("Walk Down");
            }
        }
        transform.position = Vector3.MoveTowards(tf.position, pos*2, Time.deltaTime * speed);
    }
}
