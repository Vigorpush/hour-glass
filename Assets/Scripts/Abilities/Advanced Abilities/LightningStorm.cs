using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningStorm : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Initialize(int damage, GameObject target, GameObject caster)
    {

        this.transform.Rotate(new Vector3(0, 0, 90));
        this.transform.position = target.transform.position;
        Vector3 temp = new Vector3(0, 6.1f, 0);
        this.transform.position += temp;
        target.GetComponent<Unit>().takeDamage(damage,caster);
        Invoke("hideSprite",0.2f);
        //TODO spell FX and animations
        Destroy(this.gameObject, 5f);

       // Debug.Log("Lightning Bolt Spawned dealing " + damage+ " damage to "+ target.name);

    }

    private void hideSprite()
    {
        //this.GetComponent<Animator>().SetTrigger("Smoke");
        this.GetComponent<SpriteRenderer>().enabled = false;
    }
}
