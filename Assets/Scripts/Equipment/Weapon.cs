using UnityEngine;
using System.Collections;

public class Weapon : Equipment {

    //setup damage range
    public int damageMin;
    public int damageMax;


    public void upgradeDamageByAmount(int amount){
        damageMin += amount;
        damageMax  +=amount;
    }

    void Start()
    {
        damageMin = 5;
        damageMax = 10;
    }


    public int getDamage()
    {
        int damage = Random.Range(damageMin, damageMax); // chooses a value within damage range for 1 attack
        return damage;
    }
}
