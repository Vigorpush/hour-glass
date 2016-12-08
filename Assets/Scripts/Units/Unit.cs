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
	public int tempAttackMod;
	public int tempDefenseMod;
	public int tempHPMod;

	public Weapon weap;//{ get; set; }
	public Armour armr;//{ get; set; }
	//public GameObject targ;//{ get; set; }//testing gameobject for damage
	public GameObject theTurnManager;
	public GameObject CBTprefab;
	public GameObject HealFXPrefab;
	public GameObject BuffFXPrefab;
	private Collider2D col;

	public Animator anim;

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
		tempAttackMod = 0;
		tempDefenseMod = 0;
		tempHPMod = 0;
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
		return attack+tempAttackMod;
	}
	public string getName()
	{
		return Name;
	}


	public int getcurhp()
	{
		return curhp+tempHPMod;
	}

	public int getDamageMin()
	{
		return weap.damageMin;
	}
	public int getDamageMax()
	{
		return weap.damageMax;
	}
	public int getArmour()
	{
		return armr.armour+tempDefenseMod;
	}

	//End of Getter methods


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

	public void resetBuffs()
	{
		tempAttackMod = 0;
		tempDefenseMod = 0;
		tempHPMod = 0;
	}

	public void Buff(string stat)
	{
		switch (stat)
		{
		case "Attack": tempAttackMod = attack * 2;
			InitBuffCBT("ATK UP");
			break;
		case "Defense": break;
		case "HP": break;
		}
	}



	public int CalculateDamageReduction(int preMitigatedDamage) {
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


	public virtual void takeDamage(int attackAmount, GameObject damageDealer){
		anim.SetTrigger("GotHit");
		unitThatHitMeThisTurn = damageDealer;
		if (!dying)
		{
			int result = CalculateDamageReduction(attackAmount);
			InitCBT(result.ToString());
			AnnounceDamage(result, damageDealer);
			curhp -= result;
			GetComponent<SpawnTextBubble>().gotHit(result);
		}
		if (curhp <= 0) {
			// damageDealer.SendMessage("removeFromComboTargets", this.gameObject);
			Die ();
		}
	}

	public virtual void takeCriticalDamage(int critAmount, GameObject damageDealer)
	{
		//critShaker.DoShake();
		anim.SetTrigger("GotHit");
		unitThatHitMeThisTurn = damageDealer;
		if (!dying)
		{
			int result = CalculateDamageReduction(critAmount);
			InitCBT(result.ToString());
			AnnounceDamage(result, damageDealer);
			curhp -= result;
			GetComponent<SpawnTextBubble>().gotHit(result);
		}
		if (curhp <= 0)
		{
			//damageDealer.SendMessage("removeFromComboTargets", this.gameObject);
			Die();
		}


	}

	public void disableCollider()
	{
		col.enabled = false;
	}

	private void Die(){
		setDying();
		LayerMask.NameToLayer("Ignore Raycast");
		anim.SetBool("Dead", true);
		// Debug.Log(gameObject.name + " says: \"Woe, for I am slain!\n ");
		this.SendMessage("playDeathCry");      
		theTurnManager.SendMessage("removeFromInitiativeQueue", this.gameObject);   
		disableCollider();

		//This used to sort of work but stuff targetted dying units
		Invoke("DieAfterDeathEffects",3f);
		// DieAfterDeathEffects();
	}

	private void DieAfterDeathEffects()
	{

		// Debug.Log("I died");
		//TODO: handle player death/res


		//This blows up the player pretty bad, can they be resurected?
		// Destroy(gameObject);
		this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		// this.gameObject.GetComponent<Collider2D>().enabled = false;

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
			getcurhp() + "/" +
			getmaxhp() + "."
		);
	}


	public void InitCBT(string textIn)
	{
		GameObject temp = Instantiate(CBTprefab) as GameObject;
		temp.GetComponent<Animator>().SetTrigger("Hit");
		RectTransform tempRect = temp.GetComponent<RectTransform>();
		tempRect.transform.SetParent(transform.FindChild("EnemyCanvas"));
		tempRect.transform.localPosition = CBTprefab.transform.localPosition;
		tempRect.transform.localScale = CBTprefab.transform.localScale;
		temp.transform.rotation = Quaternion.identity;

		if (dying)
		{
			temp.GetComponent<Text>().fontSize += 15;
			temp.GetComponent<Text>().color = Color.magenta;
		}
		temp.GetComponent<Text>().text = textIn;
		Destroy(temp.gameObject,2);
	}

	public void InitCBTCrit(string critIn){
		GameObject temp = Instantiate(CBTprefab) as GameObject;
		temp.GetComponent<Animator>().SetTrigger("Hit");
		RectTransform tempRect = temp.GetComponent<RectTransform>();
		tempRect.transform.SetParent(transform.FindChild("EnemyCanvas"));
		tempRect.transform.localPosition = CBTprefab.transform.localPosition;
		tempRect.transform.localScale = CBTprefab.transform.localScale;
		temp.transform.rotation = Quaternion.identity;

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


	public void InitHealCBT(string textIn)
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

		temp.transform.rotation = Quaternion.identity;
		temp.GetComponent<Text>().fontSize += 5;
		temp.GetComponent<Text>().color = Color.cyan;
		temp.GetComponent<Text>().text = textIn;
		Destroy(temp.gameObject, 2);
	}

	public void InitBuffCBT(string StatText)
	{
		GameObject buffFX = Instantiate(BuffFXPrefab) as GameObject;
		RectTransform tempRect = buffFX.GetComponent<RectTransform>();
		tempRect.transform.SetParent(transform.FindChild("EnemyCanvas"));
		tempRect.transform.localPosition = CBTprefab.transform.localPosition;
		tempRect.transform.localScale = new Vector3(0.6f, 1, 1);
		//Destroy(healFX,3);

		GameObject temp = Instantiate(CBTprefab) as GameObject;
		temp.GetComponent<Animator>().SetTrigger("Hit");
		tempRect = temp.GetComponent<RectTransform>();
		tempRect.transform.SetParent(transform.FindChild("EnemyCanvas"));
		tempRect.transform.localPosition = CBTprefab.transform.localPosition;
		tempRect.transform.localScale = CBTprefab.transform.localScale;

		temp.transform.rotation = Quaternion.identity;
		temp.GetComponent<Text>().fontSize += 5;
		temp.GetComponent<Text>().color = Color.green;
		temp.GetComponent<Text>().text = StatText;
		Destroy(temp.gameObject, 2);
	}

}