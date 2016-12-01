using UnityEngine;
using System.Collections;

public class Armour : Equipment {


    public int armour;// Armor currently works as flat damge reduction accessed within doDamage method inside Unit Script

    void Start()
    {
        armour = 20;
    }


    public int getArmour()
    {
        return armour;
    }
}
