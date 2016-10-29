using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	public float defaultDefenseModifier = 1;
	public bool isFortified { get; set; }
	public float fortifyMultiplier{ get; set; }
    //Start of fields
	public int maxhp{ get; set; }
	public int initiative{ get; set; }
	public int speed{ get; set; }
	public int range{ get; set; }
	public int attack{ get; set; }
	public string Name{ get; set; }
	public int curhp{ get; set; }

	public Weapon weap{ get; set; }
	public Armour armr{ get; set; }
	public GameObject targ{ get; set; }//testing gameobject for damage

    //End of fields
	//Reset multiplier
	public void unFortify(){

		isFortified = false;


	}
    void Start()
    {
        curhp = getmaxhp();//setting up the start HP
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


    public int getcurrentHP()
    {
        return curhp;
    }
    //End of Getter methods

	// Update is called once per frame
	void Update () {

	}
	
	public void takeDamage(int attackAmount){
        if (attackAmount - armr.getArmour()<=0)
        {
            return;
        }
        curhp -= attackAmount - armr.getArmour();
        if (curhp <= 0)
        {
            SendMessage("Die"); //Returns if dead
        }
		Debug.Log ("Oww");
	}

    public void doDamage()
    {
        //need a way to dynamically target a GameObject, currently manually assigned
        targ.SendMessage("takeDamage", weap.getDamge() + getattack());//testing gameobject for damage

    }

	void Die(){
        Destroy(gameObject);
        Debug.Log ("I died");
    }
}
