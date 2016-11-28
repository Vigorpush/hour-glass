using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MissileBarrageBehavior : MonoBehaviour {

    private GameObject theTarget;
    private GameObject theCaster;
    private float MISSILE_INITIAL_VELOCITY = 0.2f;
    private float MISSILE_ACTUAL_VELOCITY = 1f;
    private SpriteRenderer sprite;
    private Animator anim;
    private bool impacting;
    private int damageToDeal;
    private bool enableProjectile;
    private bool delay;
    Vector2 hangPosition;
    private bool floatUp;
    private ParticleSystem ps;
    private AudioSource impactSound;

    Vector2 P0;
    Vector2 P1;
    Vector2 P2;
    Vector2 P3;

    private float t;


    // Use this for initialization
    void Start () {
	
	}

    public void unFreeze()
    {
        delay = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (floatUp)
        {
            transform.Translate(Vector2.up * Time.deltaTime * 0.5f);
        }

        else if (enableProjectile && delay)
        {
            //Debug.Log(t + " moving towards" +hangPosition);
            if (t < 1) { t += 0.1f; }
            hangPosition = PointOnCurve(P0, P1, P2, P3, t);
            transform.position = Vector3.MoveTowards(this.transform.position, hangPosition,MISSILE_INITIAL_VELOCITY);
        }
        else if(!delay)
        {
           // Debug.Log("GO!");
            transform.position = Vector3.MoveTowards(this.transform.position, theTarget.GetComponent<Transform>().position, MISSILE_INITIAL_VELOCITY+=0.3f);
            if(this.transform.position == theTarget.GetComponent<Transform>().position && !impacting){
                impacting = true;
                hitTarget();
            }
        }
       
    }

    private void calculatePoints()
    {
        P0 = (Vector2)theCaster.GetComponent<Transform>().position + new Vector2(3,-3);
        P1 = (Vector2)theCaster.GetComponent<Transform>().position; //Caster Position
        P2 = (Vector2)theCaster.GetComponent<Transform>().position + Vector2.up * 3; //Hang position
        P3 = P2 + new Vector2(3,3);
    }

    public void Initialize(int damage, GameObject target, GameObject casterIn)
    {
        impactSound = GetComponent<AudioSource>();
        ps = GetComponent<ParticleSystem>();
        floatUp = false;
        theTarget = target;
        damageToDeal = damage;
        theCaster = casterIn;
        GetComponent<Transform>().position = casterIn.GetComponent<Transform>().position; //P1
        sprite = GetComponent<SpriteRenderer>();
        enableProjectile = false;
        impacting = false;
        delay = true;
        t = 0;
        calculatePoints();
        LaunchUp();
        Invoke("unFreeze", 1.5f);
    }

    private void LaunchUp()
    {
        
        enableProjectile =true;
    }

    //Standard Catmull-Rom spline
    static public Vector2 PointOnCurve(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
    {
        Vector2 ret = new Vector2();

        float t2 = t * t;
        float t3 = t2 * t;

        ret.x = 0.5f * ((2.0f * p1.x) +
        (-p0.x + p2.x) * t +
        (2.0f * p0.x - 5.0f * p1.x + 4 * p2.x - p3.x) * t2 +
        (-p0.x + 3.0f * p1.x - 3.0f * p2.x + p3.x) * t3);

        ret.y = 0.5f * ((2.0f * p1.y) +
        (-p0.y + p2.y) * t +
        (2.0f * p0.y - 5.0f * p1.y + 4 * p2.y - p3.y) * t2 +
        (-p0.y + 3.0f * p1.y - 3.0f * p2.y + p3.y) * t3);

        return ret;
    }

    private void hitTarget()
    {
        impactSound.Play();
       // ps.enableEmission = false;
        ps.Stop();
        floatUp = true;
        theTarget.GetComponent<Unit>().takeDamage(damageToDeal, theCaster);
        sprite.enabled = true;
        Invoke("MissileHit", 3f);
    }

    private void MissileHit()
    {
        Destroy(this.gameObject);
    }

}
