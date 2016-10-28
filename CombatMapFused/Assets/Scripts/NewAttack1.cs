using UnityEngine;
using System.Collections;

public class NewAttack1 : MonoBehaviour {

    public Animator anim;
    public bool canAttack;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        Debug.Log("Attack new script was even called");
        canAttack = false;
	}

    public void AllowAttack()
    {
        canAttack = true;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (Input.GetButtonDown("Fire1")&&canAttack)
        {
            anim.SetTrigger("Attack1");
            Debug.Log("Attacking");
        }
	}
}
