using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Enrage : AttackTemplate {

    private Transform startTransform;
    // private GameObject[] enemiesInRange;
    private GameObject theSelectedTarget;
    private HeroUnit myHero;
   // private List<KeyCode> theCombo;
    private bool cycleTargetLock;
    private int sizeOfTargetArray;
    private int currentSelectedIndex;
    private GameObject thePlayer;
    public AudioSource tarSound;
    public GameObject theMaestro;

    void Start()
    {
        cycleTargetLock = true;
        theMaestro = GameObject.FindGameObjectWithTag("Maestro");
    }

    public override void informOfParent(GameObject playerIn)
    {
        thePlayer = playerIn;
        startTransform = thePlayer.GetComponent<Transform>();
        myHero = thePlayer.GetComponent<HeroUnit>();

    }

    void Update()
    {
        if (!cycleTargetLock)
        {

            if (Input.GetKeyDown("space"))
            {
                cycleTargetLock = true;
                theSelectedTarget.SendMessage("Untargetted");
                myHero.takeDamage(15,theSelectedTarget);
                thePlayer.gameObject.GetComponent<PlayerAttackController>().setBuffTarget(theSelectedTarget,"Attack");
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                theMaestro.SendMessage("TargetPing");
                theSelectedTarget.SendMessage("BeingTargetted");
            }
        }
    }

    public override void CheckLine()
    {
        theMaestro.SendMessage("TargetPing");
        theSelectedTarget = thePlayer;
        theSelectedTarget.SendMessage("BeingTargetted");
        
        cycleTargetLock = false;
        
    }

    public override int[] GetDamageSteps()
    {
        int[] blank = new int[1];
        return blank;
    }

    public override string getMyName()
    {
        return "Enrage";
    }

    public override List<KeyCode> GetComboInputSequence()
    {
        List<KeyCode> blankList = new List<KeyCode>();
        return blankList;
    }
}
