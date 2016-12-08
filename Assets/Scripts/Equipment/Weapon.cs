using UnityEngine;
using System.Collections;

public class Weapon : Equipment {

    //setup damage range
    public int damageMin;
    public int damageMax;

   // protected Unit myUnit;


    public void upgradeDamageByAmount(int amount){
        damageMin += amount;
        damageMax  +=amount;
       // myUnit.InitCBT(this.gameObject.name + ": upgraded weapon by: "+amount);
    }

    void Start()
    {
        damageMin = 5;
        damageMax = 10;
       // myUnit = this.gameObject.GetComponent<Unit>();
    }


    public int getDamage()
    {
        int damage = Random.Range(damageMin, damageMax); // chooses a value within damage range for 1 attack
        return damage;
    }
}
