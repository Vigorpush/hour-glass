using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Unit : MonoBehaviour {
	public float defaultDefenseModifier = 1;
	public bool isFortified;//{ get; set; }
	public float fortifyMultiplier;//{ get; set; }
    //Start of fields
	public int maxhp;//{ get; set; }
	public int initiative;//{ get; set; }
	public int speed;//{ get; set; }
	public int range;//{ get; set; }
	public int attack;//{ get; set; }
	public string Name;//{ get; set; }
	public int curhp;//{ get; set; }
    private bool dying;

	public Weapon weap;//{ get; set; }
	public Armour armr;//{ get; set; }
                       //public GameObject targ;//{ get; set; }//testing gameobject for damage
    public GameObject theTurnManager;
    public GameObject CBTprefab;
    public GameObject HealFXPrefab;
    private Collider2D col;

    private Animator anim;

    public GameObject unitThatHitMeThisTurn;

    public GameObject theMainCamera;
    private CameraShake critShaker;
    private Transform transformForText;

    //End of fields
    //Reset multiplier
    public void unFortify(){
		isFortified = false;
	}
    void Start()
    {
        transformForText = this.gameObject.transform;
        theMainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        critShaker = theMainCamera.GetComponent<CameraShake>();
        theTurnManager = GameObject.FindGameObjectWithTag("Manager");
        anim = this.gameObject.GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        dying = false;
        curhp = getmaxhp();//setting up the start HP
       // floatingCombatText = GameObject.FindWithTag("FloatingCombatText").GetComponent<Text>()as Text;
		//weap = //temp weapon
		//armr = //temp armor
    }

    //Start of Setter methods
    public void setinitiative(int newinitiative)
    {
        initiative = newinitiative;
    }

    public void setmaxhp(int newmaxhp)
    {
        maxhp = newmaxhp;
    }

    public void setspeed(int newspeed)
    {
        speed = newspeed;
    }

    public void setrange(int newrange)
    {
        range = newrange;
    }
    public void setattack(int newattack)
    {
        attack = newattack;
    }
    //End of Setter methods

    //Start of Getter methods 
    public int getmaxhp()
    {
        return maxhp;
    }

    public int getinitiative()
    {
        return initiative;
    }

    public int getspeed()
    {
        return speed;
    }

    public int getrange()
    {
        return range;
    }

    public int getattack()
    {
        return attack;
    }
    public string getName()
    {
        return Name;
    }


    public int getcurhp()
    {
        return curhp;
    }
    //End of Getter methods

	// Update is called once per frame
	void Update () {

	}

    public bool WillDieFromDamage(int attackAmount)
    {
		if (hasArmour()) {
			if (curhp <= attackAmount - armr.getArmour ()) {
				return true;
			} else
				return false;
		} else {
			if (curhp <= attackAmount) {
				return true;
			} else
				return false;
		}
			
    }
	public bool hasArmour(){
		return armr != null;
	}
    public void Heal(int healAmount)
    {       
        curhp += healAmount;
        int overHeal =0;
        Debug.Log("healing for " +healAmount);
        if(curhp > maxhp){
            overHeal = curhp - maxhp;
            curhp = maxhp;
        }
        healAmount -= overHeal;//If overhealed, clip off amount over maxhp
        InitHealCBT(healAmount.ToString());
    }

    public void BeingTargetted()
    {
        anim.SetBool("isATarget", true);
    }

    public void Untargetted()
    {
        anim.SetBool("isATarget", false);
    }

    private int CalculateDamageReduction(int preMitigatedDamage) {
      //  Debug.Log("-----------------------------------------------------------preMitigatedDamage = "+ preMitigatedDamage);
        float damagePercentage = 0;
        float f = (float)preMitigatedDamage;    //this was needed to clarify arg to Mathf
        float damagePostMitigation = 0;
        damagePercentage = (float)100/( 100 + armr.armour);
            // damage reduction formula
        damagePostMitigation = Mathf.Ceil( (f * damagePercentage));
      //  Debug.Log("-----------------------------------------------------------damagePercentage = " + damagePercentage);
       // Debug.Log("-----------------------------------------------------------damagePostMitigation = " + damagePostMitigation);
        int conv = Mathf.RoundToInt(damagePostMitigation);
      //  Debug.Log("-----------------------------------------------------------damage after converting to int = " + conv);
        return conv;

    }

    public void takeDamage(int attackAmount, GameObject damageDealer){
        anim.SetTrigger("GotHit");
        unitThatHitMeThisTurn = damageDealer;
        int result = CalculateDamageReduction(attackAmount);
        curhp -= result;
        GetComponent<SpawnTextBubble>().gotHit(result);
        AnnounceDamage(result, damageDealer);
		InitCBT (result.ToString ());
			if (curhp <= 0) {
				Die ();
			}
    }

    public void takeCriticalDamage(int critAmount, GameObject damageDealer)
    {
        critShaker.DoShake();
        anim.SetTrigger("GotHit");
        unitThatHitMeThisTurn = damageDealer;
        int result = CalculateDamageReduction(critAmount);
        curhp -= result;
        GetComponent<SpawnTextBubble>().gotHit(result);
        AnnounceDamage(result, damageDealer);
        InitCBT(result.ToString());
        if (curhp <= 0)
        {
            Die();
        }
		

    }

   /* public void DoDamage()
    {
        //need a way to dynamically target a GameObject, currently manually assigned
        targ.SendMessage("takeDamage", weap.getDamage() + getattack());//testing gameobject for damage

     }
    */

    public void disableCollider()
    {
        col.enabled = false;
    }

    void Die(){
        LayerMask.NameToLayer("Ignore Raycast");
        anim.SetBool("Dead", true);
       // Debug.Log(gameObject.name + " says: \"Woe, for I am slain!\n ");
        this.SendMessage("playDeathCry");      
        theTurnManager.SendMessage("removeFromInitiativeQueue", this.gameObject);   
        disableCollider();
        setDying();
        //This used to sort of work but stuff targetted dying units
        //Invoke("DieAfterDeathEffects",3f);
        DieAfterDeathEffects();
    }

   private void DieAfterDeathEffects()
    {
        
       // Debug.Log("I died");
       //TODO: handle player death/res
       
           
           //This blows up the player pretty bad, can they be resurected?
        Destroy(gameObject);

       //the resurect friendly version
       // this.gameObject.SetActive(false);

    }

    public void playDeathCry()
    {
        //stub to catch hero deaths
    }

    public void setDying()
    {
        
        dying = true;
    }


    public bool getDying()
    {
        return dying;
    }


     public void AnnounceDamage(int attackDamage, GameObject theAgressor) {

         // GameObject temp = Instantiate(FloatingDmgNum) as GameObject;
         // RectTransform temRect = temp.GetComponent<RectTransform>();
         // temp.transform.SetParent(transform.FindChild("EnemyCanvas"));
         // tempRect.transform.local
          LogDamage(attackDamage,theAgressor);
         // floatingCombatText.text = attackDamage.ToString();

      }

      private void LogDamage(int damageDealt,GameObject theAggressor)
      {
          //TODO inform game master
          Debug.Log(theAggressor.name + "Dealing " + this.gameObject.name + " " + damageDealt + " damage.  Remaining HP: " +
                                  this.GetComponent<Unit>().getcurhp() + "/" +
                                  this.GetComponent<Unit>().getmaxhp() + "."
                                  );
      }


    void InitCBT(string textIn)
    {
        GameObject temp = Instantiate(CBTprefab) as GameObject;
        temp.GetComponent<Animator>().SetTrigger("Hit");
        RectTransform tempRect = temp.GetComponent<RectTransform>();
        tempRect.transform.SetParent(transform.FindChild("EnemyCanvas"));
        tempRect.transform.localPosition = CBTprefab.transform.localPosition;
        tempRect.transform.localScale = CBTprefab.transform.localScale;
       

        if (dying)
        {
            temp.GetComponent<Text>().fontSize += 15;
            temp.GetComponent<Text>().color = Color.magenta;
        }
        temp.GetComponent<Text>().text = textIn;
        Destroy(temp.gameObject,2);
    }

    void InitCBTCrit(string critIn){
        GameObject temp = Instantiate(CBTprefab) as GameObject;
        temp.GetComponent<Animator>().SetTrigger("Hit");
        RectTransform tempRect = temp.GetComponent<RectTransform>();
        tempRect.transform.SetParent(transform.FindChild("EnemyCanvas"));
        tempRect.transform.localPosition = CBTprefab.transform.localPosition;
        tempRect.transform.localScale = CBTprefab.transform.localScale;


        if (dying)
        {
            temp.GetComponent<Text>().fontSize += 25;
            temp.GetComponent<Text>().color = Color.grey;
        }
        temp.GetComponent<Text>().text = critIn;
        temp.GetComponent<Text>().fontSize += 25;
        temp.GetComponent<Text>().color = Color.yellow;
        Destroy(temp.gameObject, 4);
    }
    

    void InitHealCBT(string textIn)
    {
        GameObject healFX = Instantiate(HealFXPrefab) as GameObject;
        RectTransform tempRect = healFX.GetComponent<RectTransform>();
        tempRect.transform.SetParent(transform.FindChild("EnemyCanvas"));
        tempRect.transform.localPosition = CBTprefab.transform.localPosition;
        tempRect.transform.localScale = new Vector3(0.6f, 1,1);
        //Destroy(healFX,3);

        GameObject temp = Instantiate(CBTprefab) as GameObject;
        temp.GetComponent<Animator>().SetTrigger("Hit");
        tempRect = temp.GetComponent<RectTransform>();
        tempRect.transform.SetParent(transform.FindChild("EnemyCanvas"));
        tempRect.transform.localPosition = CBTprefab.transform.localPosition;
        tempRect.transform.localScale = CBTprefab.transform.localScale;
        temp.GetComponent<Text>().fontSize += 5;
        temp.GetComponent<Text>().color = Color.cyan;
        temp.GetComponent<Text>().text = textIn;
        Destroy(temp.gameObject, 2);
    }

}
