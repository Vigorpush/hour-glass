using UnityEngine;
using System.Collections;

public class ArrowFlight : MonoBehaviour {

    private GameObject theTarget;
    private GameObject cam;
    private bool enableProjectile;
    private SpriteRenderer sprite;
   // private Animator anim;
    private bool impactingTarget;

	// Use this for initialization
	void Start () {
	
	}

    public void Initialize(int damage, GameObject target, GameObject caster)
    {
        theTarget = target;
        //anim = this.GetComponent<Animator>();
        enableProjectile = false;
        sprite = this.GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        cam.SendMessage("UnsetCombatZoom");
        this.transform.position = caster.GetComponent<Transform>().position;
        impactingTarget = false;
        transform.LookAt(transform.position + theTarget.transform.position);
        transform.RotateAround(transform.position, new Vector3(0, 0, 1), 90);
        Invoke("DelayTravel", 0.2f);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (enableProjectile)
        {
            transform.LookAt(transform.position + theTarget.transform.position);
            transform.RotateAround(transform.position, new Vector3(0, 0, 1), 90);
            transform.position = Vector3.MoveTowards(this.transform.position, theTarget.GetComponent<Transform>().position, 0.1f);

            // Debug.Log("Fireball at: " + this.GetComponent<Transform>().position + " and moving to: "+ theTarget.GetComponent<Transform>().position);
            // Debug.DrawRay(this.transform.position, theTarget.GetComponent<Transform>().position - this.transform.position,Color.red, 3f);
            RaycastHit2D myRay = Physics2D.Raycast(this.transform.position, theTarget.GetComponent<Transform>().position - this.transform.position, 0.2f);
            if (myRay.collider != null)
            {
                if (myRay.collider.Equals(theTarget.GetComponent<Collider2D>()) && !impactingTarget)
                {
                    impactingTarget = true;
                    Debug.Log("Ranger Arrow hit the target");
                   // anim.SetTrigger("HitTarget");
                    sprite.enabled = false;
                    Invoke("BreakArrow", 0.1f);
                }
            }
        }
	
	}

    private void DelayTravel()
    {
        sprite.enabled = true;
        enableProjectile = true;
    }

    private void BreakArrow()
    {
        Destroy(this.gameObject);
    }
}
