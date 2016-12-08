using UnityEngine;
using System.Collections;

public class FireballFlight : MonoBehaviour {

    protected GameObject theTarget;
    protected GameObject cam;
    protected float FIREBALL_VELOCITY = 0.5f;
    protected bool enableProjectile;
    protected SpriteRenderer sprite;
    protected Animator anim;
    protected bool floatUp;
    protected bool exploding;
    protected int damageToDeal;
    protected GameObject comboCtrl;
    protected AudioSource[] hitSounds;
    public AudioSource castSound;
    public AudioSource hitSound;
    public GameObject theCaster;

	// Use this for initialization
	void Start () {

	}

    public void Initialize(int damage, GameObject target, GameObject caster)
    {
        hitSounds = GetComponents<AudioSource>();
        theCaster = caster;
        castSound = hitSounds[0];
        hitSound = hitSounds[1];
        damageToDeal = damage;
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
        Invoke("DelayTravel",0.1f);

        Vector3 vectorToTarget = theTarget.GetComponent<Transform>().position - transform.position;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1000000000f);

        // transform.LookAt(theTarget.transform.position);
        // transform.RotateAround(transform.position, new Vector3(0, 0, 1), 90);

    }

    // Update is called once per frame
    void FixedUpdate () {

        if (theTarget == null)
        {
            enableProjectile = false;
            EndSpell();          
        }

        if (enableProjectile && this.transform.position == theTarget.GetComponent<Transform>().position)
        {
            enableProjectile = false;
            //Debug.Log("fireball arrived");
            hit();

        }
        if (enableProjectile) {
            transform.position = Vector3.MoveTowards(this.transform.position, theTarget.GetComponent<Transform>().position, FIREBALL_VELOCITY);
        }

        if (floatUp){
            transform.Translate(Vector2.up * Time.deltaTime *0.5f);
        }
    }

    protected void hit()
    {
        hitSound.Play();
        exploding = true;
        GetComponent<Collider2D>().enabled = false;
        if (theTarget.tag.Equals("Baddy"))
        {
            theTarget.GetComponent<Unit>().takeDamage(damageToDeal, theCaster);
        }      
      //  AnnounceDamage();
        anim.SetTrigger("HitTarget");
        FIREBALL_VELOCITY = 0.1f;
        Invoke("Smoking", 0.1f); //give a moment to play explosion
    }

    protected void DelayTravel()
    {
        castSound.Play();
        sprite.enabled = true;
        enableProjectile = true;
    }

    protected void Smoking()
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

    protected void EndSpell()
    {
        Destroy(this.gameObject);
    }

    protected void AnnounceDamage()
    {
        //TODO inform game master
       // Debug.Log("Casted Fireball");
    }

}
