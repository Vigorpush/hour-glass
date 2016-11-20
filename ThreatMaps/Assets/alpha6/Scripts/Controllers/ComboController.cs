using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ComboController : MonoBehaviour {

    public bool listenForComboInput;
    public KeyCode validInputButton;
    public int stepsIntoCombo;
    public float timerWindow;
    public bool pauseHourglass;
    public int totalStepsForCombo;
    private List<KeyCode> theInputButtons;
    private PlayerAttackController attackController;
    List<RaycastHit2D> thisAttackTargets;
    private Animator anim;
    public bool targetIsDead;
    public bool discreteInputWindow;
    private GameObject cam;
    bool playSpellAnimation;
    private GameObject tar;
    private bool castFirstFail;
    private int spellDamageToDeal;

    private int step1Damage;  
    private int step2Damage; 
    private  int step3Damage;
    private int attackDamage;
    private HeroUnit myHero;
    private bool rangedAbilityFlag;
    

    //The UI
    public Text turnTimeElapsed;
    public Text inputInstruction;
    public Text floatingCombatText;

    void Start () {
        myHero = this.gameObject.GetComponent<HeroUnit>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        listenForComboInput = false;    
        discreteInputWindow = false;    //Used to prevent multiple presses within a short timeframe
        pauseHourglass = true;
        anim = this.gameObject.GetComponent<Animator>();
        attackController = this.gameObject.GetComponent<PlayerAttackController>();
        step1Damage = 1; 
        step1Damage = 2;
        step1Damage = 3;
        spellDamageToDeal=0;
        castFirstFail = true; //if first button missed, need a way to not auto cast lvl1 spell
        rangedAbilityFlag = false;
	}

	void Update () {
        if (!pauseHourglass)    //This is to pause for fancy animations during combat
        {
            timerWindow -= Time.deltaTime;  //this is the input timer for combo buttons
        }

        if (listenForComboInput)    //While listening for the correct button, won't listen if in some sort of transition
        {
            if (timerWindow < 0)            //If player took too long pressing correct button
            {
                anim.SetTrigger("ComboFail");
                listenForComboInput = false;
                Invoke("EndCombat", 0.5f);
            }

            //Update UI with instructions on what button is valid
            if (System.Math.Round(timerWindow, 2) > 2)
            {
                turnTimeElapsed.color = Color.green;
                turnTimeElapsed.fontSize = 20;
            }
            else if (System.Math.Round(timerWindow, 2) > 1)
            {
                turnTimeElapsed.color = Color.yellow;
                turnTimeElapsed.fontSize = 22;
            }
            else if (System.Math.Round(timerWindow, 2) > 0)
            {
                turnTimeElapsed.color = Color.red;
                turnTimeElapsed.fontSize = 24;
            }

            turnTimeElapsed.text = "Time left: " + System.Math.Round(timerWindow,2);
            inputInstruction.text = "Press: " + validInputButton.ToString();

            if (Input.GetKeyDown(validInputButton) && discreteInputWindow)  //If the correct button is pressed (only pressed once!)
            {
                discreteInputWindow = false;
                castFirstFail = false;  //made atleast one success
                listenForComboInput = false;

               // if(tar != null){
                    AttackTarget();
              //  }
              //  else
              //  {
              //      Debug.Log("Tried to attack no target?" +tar.name);
              //      pauseHourglass = true;
              //      Invoke("EndCombat", 0.5f);
              //  }
                
                stepsIntoCombo++;
                //currentStepDamage++;
                if (stepsIntoCombo == totalStepsForCombo)   //If that was the last hit of the combo, End turn
                {
                    pauseHourglass = true;
                    Invoke("EndCombat", 0.5f);
                }              
                if (!pauseHourglass && stepsIntoCombo < totalStepsForCombo) //If there is more to the combo, go to the next part
                {
                    pauseHourglass = true;
                    setCombo(theInputButtons[stepsIntoCombo],stepsIntoCombo);
                }
            }
        }
	}

    private void AttackTarget()
    {
        //For each target when attack queued up
        foreach (RaycastHit2D target in thisAttackTargets)
        {
            if (!target.Equals(null))
            {
               tar = target.collider.gameObject;
               // Debug.Log("Step " + stepsIntoCombo);
                switch (stepsIntoCombo) //Trigger appropriate combo animation
                {
                    case 0:
                        anim.SetTrigger("Attack1");
                        if (playSpellAnimation) { spellDamageToDeal += step1Damage; } else { attackDamage = step1Damage; }
                       // Debug.Log("Spell will deal" + spellDamageToDeal);
                        break;
                    case 1:
                        anim.SetTrigger("Attack2");
                        if (playSpellAnimation) { spellDamageToDeal += step2Damage; } else { attackDamage = step2Damage; }
                        // Debug.Log("Spell will deal" + spellDamageToDeal);
                        break;
                    case 2:
                        anim.SetTrigger("Attack3");
                        if (playSpellAnimation) { spellDamageToDeal += step3Damage; } else { attackDamage = step3Damage; }
                        // Debug.Log("Spell will deal" + spellDamageToDeal);
                        break;
                }

                if (
                    tar.gameObject.GetComponent<Unit>().willDieFromDamage(attackDamage) || 
                    tar.gameObject.GetComponent<Unit>().willDieFromDamage(spellDamageToDeal)
                    ) //if lethal damage
                {
                    listenForComboInput = false;
                    pauseHourglass = true;
                    anim.SetTrigger("ComboFail"); //Isn't actually a fail, just exit to idle because enemy dead

                    if (!playSpellAnimation) //if melee do immediate damage
                    {
                        if (rangedAbilityFlag) //ranged
                        {
                            AnnounceDamage(attackDamage, tar);
                            tar.GetComponent<Unit>().takeDamage(attackDamage);
                            GameObject arrow = RangerArrowFactory.CreateArrow(1, tar, this.gameObject);
                            Debug.Log("Final blow to  " + tar.gameObject.name + "dealing " + attackDamage);
                            Invoke("EndCombat", 0.5f);
                        }
                        else //melee
                        {
                            AnnounceDamage(attackDamage, tar);
                            tar.GetComponent<Unit>().takeDamage(attackDamage);

                            Debug.Log("Final blow to  " + tar.gameObject.name + "dealing " + attackDamage);
                            Invoke("EndCombat", 0.5f);
                        }
                    }
                    else //if spell, accumulate damage automatically
                    {
                       // discreteInputWindow = false;
                        Invoke("EndCombat", 0.5f);
                    }
                }
                else //if hit is not lethal
                {
                    if (!playSpellAnimation)//if a melee, do damage immediately
                    {

                        if (rangedAbilityFlag) //ranged
                        {
                            AnnounceDamage(attackDamage, tar);
                            tar.GetComponent<Unit>().takeDamage(attackDamage);
                            GameObject arrow = RangerArrowFactory.CreateArrow(1, tar, this.gameObject);
                        }
                        else //melee
                        {


                            AnnounceDamage(attackDamage, tar);
                            tar.GetComponent<Unit>().takeDamage(attackDamage);
                            LogDamage(attackDamage);
                        }
                    }
                    else //if spell accumulate damage
                    {

                    }
                    //tar.SendMessage ("DoDamage",ability.damage);
                }
            }
        }
    }

    private void LogDamage(int damageDealt){
        //TODO inform game master
        Debug.Log("Dealing " + tar.gameObject.name + " " + damageDealt + " damage.  Enemy HP: " +
                                tar.GetComponent<Unit>().getcurhp() + "/" +
                                tar.GetComponent<Unit>().getmaxhp() + "."
                                );
    }

    private void AnnounceDamage(int attackDamage, GameObject target) {

        /*GameObject temp = Instantiate(FloatingDmgNum) as GameObject;
        RectTransform temRect = temp.GetComponent<RectTransform>();
        temp.transform.SetParent(transform.FindChild("EnemyCanvas"));
        tempRect.transform.local*/

        floatingCombatText.text = attackDamage.ToString();
        
    }

    public void BeginComboInput(List<KeyCode> buttonSequence, List<RaycastHit2D> thisAttackTargetsIn, int[] damageSteps,bool hasCastingExitAnimation,bool isRangedPhysical)
    {
        Debug.Log("Targetting "+ thisAttackTargetsIn[0].collider.gameObject.name);
        if(isRangedPhysical){
            rangedAbilityFlag = true;
        }
        else
        {
            rangedAbilityFlag = false;
        }
        
        //Debug.Log(tar.name);
        spellDamageToDeal = 0;
        turnTimeElapsed.enabled = true;
        playSpellAnimation = hasCastingExitAnimation;
        stepsIntoCombo = 0;
        //damageScaler = damageScalerIn;
        thisAttackTargets = thisAttackTargetsIn;
        if (thisAttackTargets.Count == 0)
        {
            Invoke("EndCombat", 0.5f);
            //Just end turn if no targets
        }
        else
        {
            cam.GetComponent<CameraFollow>().SetCameraFollow(this.gameObject);
            cam.SendMessage("SetCombatZoom");
            theInputButtons = buttonSequence;
            totalStepsForCombo = theInputButtons.Capacity - 1;
            validInputButton = theInputButtons[0];
            step1Damage = damageSteps[0];
            step2Damage = damageSteps[1];
            step3Damage = damageSteps[2];
            setCombo(validInputButton, stepsIntoCombo);
        }
    }

    public void setCombo(KeyCode targetButton, int comboStep)
    {
        Debug.Log(targetButton + "set as target button as targetButton:"+comboStep);
        validInputButton = targetButton;
       // baseDamage =  myHero.weap.getDamage() +myHero.attack;
        stepsIntoCombo = comboStep;
        timerWindow = 3f;
        pauseHourglass = false;
        Invoke("EnableInput",.5f);
    }


    public void EnableInput()
    {
        listenForComboInput = true;
        discreteInputWindow = true;
        //Debug.Log("Enabling INput");
    }

  /*  public void TargetDiedMidCombo()
    {
        targetIsDead = true;
    }*/

    public void EndCombat()
    {
        turnTimeElapsed.enabled = false;

        if (playSpellAnimation &&!castFirstFail) //if a spell attack
        {
           // Debug.Log("~~~Cast a spell!");
            tar.GetComponent<Unit>().takeDamage(spellDamageToDeal);
           LogDamage(spellDamageToDeal);
            GameObject spell = SpellFactory.CreateFireball(1, tar, this.gameObject);
            Invoke("EndTurn", 2.5f);
        }
        else //if a melee attack
        {
            Debug.Log("~~~~Melee combo completed");
            this.gameObject.GetComponent<BasicRayAttack>().clearTarget();
            cam.SendMessage("UnsetCombatZoom");
            pauseHourglass = true;
            listenForComboInput = false;
            discreteInputWindow = false;
            Invoke("EndTurn", 1f);
        }
    }

    public void EndTurn()  
    {
        this.gameObject.GetComponent<PlayerMovement>().EndTurn();
    }

}
