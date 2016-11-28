using UnityEngine;
using System.Collections;

public class FireballFlight : MonoBehaviour {

    private GameObject theTarget;
    private GameObject cam;
    private bool enableProjectile;
    private SpriteRenderer sprite;
    private Animator anim;
    private bool floatUp;
    private bool exploding;

	// Use this for initialization
	void Start () {

	}

    public void Initialize(int damage, GameObject target, GameObject caster)
    {
        theTarget = target;
        anim = this.GetComponent<Animator>();
        enableProjectile = false;
        sprite = this.GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        cam.SendMessage("UnsetCombatZoom");
        this.transform.position = caster.GetComponent<Transform>().position;
        floatUp = false;
        exploding = false;
        Invoke("DelayTravel",0.3f);
       // transform.LookAt(theTarget.transform.position);
       // transform.RotateAround(transform.position, new Vector3(0, 0, 1), 90);

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (enableProjectile) {
           // transform.position = Vector3.MoveTowards((Vector2)this.transform.position, (Vector2)theTarget.GetComponent<Transform>().position, 0.5f);
          transform.Translate(((Vector2)theTarget.GetComponent<Transform>().position - (Vector2)this.transform.position) * Time.deltaTime * 10);

            // Debug.Log("Fireball at: " + this.GetComponent<Transform>().position + " and moving to: "+ theTarget.GetComponent<Transform>().position);
             Debug.DrawRay(this.transform.position, theTarget.GetComponent<Transform>().position - this.transform.position,Color.red, 0.1f);
            RaycastHit2D myRay = Physics2D.Raycast(this.transform.position, theTarget.GetComponent<Transform>().position - this.transform.position, 0.2f);
            if (myRay.collider != null)
            {
                if (myRay.collider.Equals(theTarget.GetComponent<Collider2D>()) && !exploding)
                {
                    exploding = true;
                    //Debug.Log("Fireball hit the target");
                    anim.SetTrigger("HitTarget");
                    Invoke("Smoking",0.1f);
                }
            }
        }

        if(floatUp){
            transform.Translate(Vector2.up * Time.deltaTime *0.5f);
        }

    }

    private void DelayTravel()
    {
        sprite.enabled = true;
        enableProjectile = true;
    }

    private void Smoking()
    {
        enableProjectile = false;
 
        anim.SetTrigger("Smoking");
        Invoke("EndSpell",3f);
    }

    //The animator calls this function!
    public void FloatUp()
    {
        floatUp = true;
    }

    private void EndSpell()
    {
        Destroy(this.gameObject);
    }
}
