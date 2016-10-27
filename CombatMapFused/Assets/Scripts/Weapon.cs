using UnityEngine;
using System.Collections;

public class Weapon : Equipment {

    //setup damage range
    public int damageMin;
    public int damageMax;


    public int getDamge()
    {
        int damage = Random.Range(damageMin, damageMax); // chooses a value within damage range for 1 attack
        return damage;
    }
}
