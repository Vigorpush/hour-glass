using UnityEngine;
using System.Collections;

public class ArrowFlight : MonoBehaviour {

    protected float ARROW_VELOCITY=1.5f;
    protected GameObject theTarget;
    protected GameObject cam;
    protected bool enableProjectile;
    protected SpriteRenderer sprite;
   // protected Animator anim;
    protected bool impactingTarget;
    // protected Rigidbody2D rb;
    protected int damageToDeal;
    protected GameObject theCaster;
    protected bool crit;

	// Use this for initialization
	void Start () {
	
	}

    public void Initialize(int damage, GameObject target, GameObject caster,bool critIn)
    {
        crit = critIn;
        //rb = GetComponent<Rigidbody2D>();
        damageToDeal = damage;
        theCaster = caster;
        theTarget = target;
        //anim = this.GetComponent<Animator>();
        enableProjectile = false;
        sprite = this.GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        cam.SendMessage("UnsetCombatZoom");
        this.transform.position = caster.GetComponent<Transform>().position;
        impactingTarget = false;
        //transform.LookAt(transform.position + theTarget.transform.position);
        //transform.RotateAround(transform.position, new Vector3(0, 0, 1), 90);

        Vector3 vectorToTarget = theTarget.GetComponent<Transform>().position - transform.position;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1000000000f);


        Invoke("DelayTravel", 0.3f); //arrow nnock animation
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (theTarget == null)
        {
            BreakArrow();
        }
        else
        {

            if (this.transform.position == theTarget.GetComponent<Transform>().position && enableProjectile)
            {
                //Debug.Log("Arrow arrived");
                hit();
                enableProjectile = false;
            }


            if (enableProjectile)
            {

                transform.position = Vector3.MoveTowards(this.transform.position, theTarget.GetComponent<Transform>().position, ARROW_VELOCITY);



                // Debug.Log("Fireball at: " + this.GetComponent<Transform>().position + " and moving to: "+ theTarget.GetComponent<Transform>().position);
                // Debug.DrawRay(this.transform.position, theTarget.GetComponent<Transform>().position - this.transform.position,Color.red, 3f);
                /*   RaycastHit2D myRay = Physics2D.Raycast(this.transform.position, theTarget.GetComponent<Transform>().position - this.transform.position, 1f);
                   if (myRay.collider != null)
                   {
                       if (myRay.collider.gameObject.name.Equals(theTarget.GetComponent<Collider2D>().gameObject.name) && !impactingTarget)
                       {
                           hit();
                       }
                   }*/
            }
        }
	}

    protected void hit()
    {
        impactingTarget = true;
        if (crit)
        {
            theTarget.GetComponent<Unit>().takeCriticalDamage(damageToDeal, theCaster);
        }
        else
        {
            theTarget.GetComponent<Unit>().takeDamage(damageToDeal, theCaster);
        }
        
        Debug.Log("Ranger Arrow hit the target for: " + damageToDeal);
        // anim.SetTrigger("HitTarget");
        sprite.enabled = false;
        Invoke("BreakArrow", 0.1f);
    }

    protected void DelayTravel()
    {
        sprite.enabled = true;
        enableProjectile = true;
    }

    protected void BreakArrow()
    {
        Destroy(this.gameObject);
    }
}
