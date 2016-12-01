using UnityEngine;
using System.Collections;

public class EquipmentUpgradeGen : MonoBehaviour {

    //public GameObject FighterPlayer;
    public HeroUnit Fighter;
    public HeroUnit Caster;
    public HeroUnit Archer;

    //random numbers to determine what upgrade to make
    private int upgradeType;
    private int classNum;
    private int equipmentType;
    private int rarity;

    public HeroUnit heroToUpgrade;
    private Equipment equipmentToUpgrade;
    private int upgradeAmount;

   void Start()
    {
        UpgradeEquipment();
       // Fighter = FighterPlayer.GetComponent<HeroUnit>();
    }
	
	void UpgradeEquipment()
    {
        upgradeType = Random.Range(1, 5); //This is to determine upgrade type (Either Single(1), WeaponChoice(2), ArmourChoice(3), or AllChoice(4))
        // placeholder until UI is implemented for choices
        upgradeType = 1;
        switch (upgradeType)
        {
            case 1:
                classNum = Random.Range(1, 4); //This Determines what class the Upgrade is for when it is a single upgrade
                equipmentType = Random.Range(1, 3); //This determines if the upgrade is for Weapon(1) or Armour(2) when it is a single upgrade
                break;
        /* case 2:
                classNum = getWeaponChoice();
                equipmentType = 1;
                break;
           case 3:
                classNum = getArmourChoice();
                equipmentType = 2;
                break;
           case 4:
                getAllChoice()
                break;
        */
        }

        switch (classNum)        // Set heroToUpgrade to the randomized or selected class
        {
            case 1:
                heroToUpgrade = Fighter;
               // Debug.Log("Upgrading Fighter");
                break;
            case 2:
                heroToUpgrade = Caster;
               // Debug.Log("Upgrading Caster");
                break;
            case 3:
                heroToUpgrade = Archer;
               // Debug.Log("Upgrading Archer");
                break;
        }

        switch (equipmentType)
        {
            case 1:
                equipmentToUpgrade = heroToUpgrade.weap;
               // Debug.Log("Upgrading Weapon");
                break;
            case 2:
                equipmentToUpgrade = heroToUpgrade.armr;
               // Debug.Log("Upgrading Armour");
                break;
        }

        /*
            THIS SECTION WILL HAVE TO BE BALANCED
        */
        rarity = Random.Range(1, 101); //This will determine rarity/strength of the upgrade
        if (rarity <= 60)       // 60% chance
        {
            equipmentToUpgrade.rarity = "Common";
           // Debug.Log("Rarity is now Common");
            upgradeAmount = 1;                          
        }
        else if (rarity <= 90)      // 30% chance
        {
            equipmentToUpgrade.rarity = "Rare";
         //   Debug.Log("Rarity is now Rare");
            upgradeAmount = 3;                          
        }
        else if (rarity <= 98)      // 8% chance
        {
            equipmentToUpgrade.rarity = "Epic";
          //  Debug.Log("Rarity is now Epic");
            upgradeAmount = 6;                          
        }
        else
        {
            equipmentToUpgrade.rarity = "Legendary";
           // Debug.Log("Rarity is now Common");
            upgradeAmount = 8;
        }

        switch (equipmentType)
        {
            case 1:
                Weapon weaponToUpgrade = equipmentToUpgrade as Weapon;
                weaponToUpgrade.damageMin += upgradeAmount;
                weaponToUpgrade.damageMax += upgradeAmount;
                break;
            case 2:
                Armour armourToUpgrade = equipmentToUpgrade as Armour;
                armourToUpgrade.armour += upgradeAmount;
                break;
        }
    }
}
