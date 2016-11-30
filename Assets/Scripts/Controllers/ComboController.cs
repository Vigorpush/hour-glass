using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ComboController : MonoBehaviour {

    //default 20% chance to crit, min 0 max 10
    public int CRIT_THRESHOLD=8; 

    //activates the controller
    public bool listenForComboInput;
    //how many steps into the combo are we
    public int stepsIntoCombo;

    //The window for input
    public float timerWindow;

    //how long to wait before input allowed
    public float waitTimeBeforeInput;

    //pause is used to not penalize time during flashy animations
    public bool pauseHourglass;

    //number of steps in the combo
    public int totalStepsForCombo;

    //the list of correct buttons in order
    private List<KeyCode> theInputButtons;

    //the current correct button
    public KeyCode validInputButton;

    //the controller that invoked the combo
    private PlayerAttackController attackController;

    //Targets that will be hit as combo stepped through
    List<GameObject> thisAttackTargets;

    //The player animator, to trigger frames in the combo
    private Animator anim;

    //If the target dies mid combo, we should stop 
    //hitting it, flag this bool
    public bool targetIsDead;

    //Locks controls so valid input can't be pressed 
    //multiple times for multiple hits in the same step
    public bool discreteInputWindow;

    //The camera, used for zoom in/out and targetting effects
    private GameObject cam;

    //Is the ability a spell
    bool playSpellAnimation;

    //Is the ability a ranged attack
    private bool rangedAbilityFlag;

    //The current target
    private GameObject tar;

    //Catch special case of no correct input at all
    private bool castFirstFail;

    //The spell damage to accumulate
    private int spellDamageToDeal;

    //The damage at step 1/3
    private int step1Damage;  

    //The damage at step 2/3
    private int step2Damage; 

    //The damage at step 3/3
    private  int step3Damage;

    //Special damage scaler
    private int attackDamage;
   
    //Sounds to play at each step in hit
    public AudioSource[] hitSounds;
    public AudioSource swing1;
    public AudioSource swing2;
    public AudioSource swing3;

    //Character audio
    public AudioSource castStep;
    public AudioSource castDone;

    //helper text on/off
    public bool showHelpText;

    //If a spell, it has a name to key behavior
    string spellName;

    //The UI
    public Text turnTimeElapsed;
    public Text inputInstruction;
    public Text floatingCombatText;

    public GameObject InputUIPrefab;
    private GameObject theFloatingInputText;
    private Animator inputAnim;

    public GameObject aggressor;

    private int buttonMash;

    public GameObject MageFX1;
    public GameObject MageFX2;
    public GameObject MageFX3;

    void Start () {
        buttonMash = 0;
        spellName = "";
        showHelpText = true;
        hitSounds = GetComponents<AudioSource>();
        swing1 = hitSounds[0];
        swing2 = hitSounds[1];
        swing3 = hitSounds[2];
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        listenForComboInput = false;    
        discreteInputWindow = false;    //Used to prevent multiple presses within a short timeframe
        pauseHourglass = true;
        anim = this.gameObject.GetComponent<Animator>();
        attackController = this.gameObject.GetComponent<PlayerAttackController>();
        step1Damage = 1; 
        step1Damage = 2;
        step1Damage = 3;
        spellDamageToDeal=3;
        castFirstFail = true; //if first button missed, need a way to not auto cast lvl1 spell
        rangedAbilityFlag = false;
	}

    public void disableHelpText()
    {
        showHelpText = false;
    }

    void FixedUpdate () {
        if (!pauseHourglass)    //This is to pause for fancy animations during combat
        {
            timerWindow -= Time.deltaTime;  //this is the input timer for combo buttons
        }

        if (listenForComboInput)    //While listening for the correct button, won't listen if in some sort of transition
        {
            if (timerWindow < 0)            //If player took too long pressing correct button
            {
                Destroy(theFloatingInputText.gameObject, 3);
                anim.SetTrigger("ComboFail");
                listenForComboInput = false;
                Invoke("EndCombat", 0.5f);
            }
            else
            {

                //Update UI timer
                updateTimer();

                //temp button masher spell
                /*
                if (playSpellAnimation && Input.GetKeyDown(validInputButton))
                {
                    Destroy(theFloatingInputText.gameObject);
                    buttonMash++;
                    inputAnim.SetTrigger("InputWindowOpen");
                    anim.SetTrigger("Attack1");
                    spellDamageToDeal++;
                    validInputButton = theInputButtons[buttonMash % theInputButtons.Count];
                    InitInputText(validInputButton.ToString());

                    List<GameObject> spells;
                    List<GameObject> spellTargets;
                    foreach (RaycastHit2D aliveTarget in thisAttackTargets)
                    {
                        if (aliveTarget.collider.gameObject != null)
                        {
                            tar = aliveTarget.collider.gameObject;
                        }                           
                    }
                    
                    // Debug.Log(buttonMash + " cast a " + spellName + " at " + tar.name + " for "+ spellDamageToDeal);
                    if (thisAttackTargets.Count > 1)
                    {
                        spellTargets = new List<GameObject>();
                    }
                    if (tar != null)
                    {
                        if (!tar.GetComponent<Unit>().getDying())
                        {
                            switch (spellName)
                            {
                                case "Fireball":
                                    GameObject spell = GetComponent<SpellFactory>().CreateFireball(spellDamageToDeal, tar, this.gameObject);
                                    break;
                                case "LightningStorm":
                                    spellTargets = buildSpellTargetList();
                                    spells = GetComponent<SpellFactory>().CreateLightningStorm(spellDamageToDeal, spellTargets, this.gameObject);
                                    break;
                                case "MissileStorm":
                                    spellTargets = buildSpellTargetList();
                                    spells = GetComponent<SpellFactory>().CreateMissileBarrage(spellDamageToDeal, spellTargets, this.gameObject);
                                    break;
                                case "": break;
                            }
                        }
                        
                    }

                }
                else
                {*/
            
                    if (timerWindow > waitTimeBeforeInput) //if button was pressed before wait Time
                    {
                        // Debug.Log("Input is NOT valid yet!");
                        if (Input.GetKeyDown(validInputButton) && discreteInputWindow)
                        {
                            inputAnim.SetTrigger("InputFail");
                            discreteInputWindow = false;
                            anim.SetTrigger("ComboFail");
                            listenForComboInput = false;
                            Invoke("EndCombat", 0.5f);
                        }
                    }
                    else
                    {
                        // Debug.Log("InputNowValid!");
                        inputAnim.SetTrigger("InputWindowOpen");
                    }
                    //inputInstruction.text = "Press: " + validInputButton.ToString();


                    if (Input.GetKeyDown(validInputButton) && discreteInputWindow)  //If the correct button is pressed (only pressed once!)
                    {
                        discreteInputWindow = false;
                        castFirstFail = false;  //made atleast one success
                        listenForComboInput = false;
                        Destroy(theFloatingInputText);
                        if (stepsIntoCombo < totalStepsForCombo && playSpellAnimation)
                        {
                            hitSounds[5].Play();
                            cam.SendMessage("UnsetCombatZoom");
                        }
                        else if (playSpellAnimation)
                        {
                            hitSounds[6].Play();
                        }

                        

                        AttackTarget();

                        stepsIntoCombo++;

                        if (stepsIntoCombo == totalStepsForCombo)   //If that was the last hit of the combo, End turn
                        {
                            pauseHourglass = true;
                            Invoke("EndCombat", 0.5f);
                        }
                        if (!pauseHourglass && stepsIntoCombo < totalStepsForCombo) //If there is more to the combo, go to the next part
                        {
                            pauseHourglass = true;
                            setCombo(theInputButtons[stepsIntoCombo], stepsIntoCombo);
                        }
                    }
                }
            }
        
	}

    private bool rollCritChance()
    {
        int chance = Random.Range(0, 11);
        if (chance > CRIT_THRESHOLD)
        {
            Debug.Log(chance + " Attack Critical Hit!");
            return true;
        }
        else {
            Debug.Log(chance+ " Attack normal Hit.");
            return false;
        }
    }

    private void updateTimer()
    {
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
        turnTimeElapsed.text = "Time left: " + System.Math.Round(timerWindow, 2);
    }

    private void AttackTarget()
    {
        bool criticalHit = false;
        //For each target when attack queued up
        foreach (GameObject target in thisAttackTargets)
        {
            if (!target.Equals(null))
            {
                tar = target;
                // Debug.Log("Step " + stepsIntoCombo);
                switch (stepsIntoCombo) //Trigger appropriate combo animation
                {
                    case 0:
                        anim.SetTrigger("Attack1");
                        if (!rangedAbilityFlag && !playSpellAnimation) { swing1.Play(); }
                        if (playSpellAnimation) {
                            spellDamageToDeal += step1Damage;
                            MageFX1.gameObject.SetActive(true);
                        } else {
                            if (rollCritChance())
                            {
                                attackDamage = step1Damage*2;
                                criticalHit = true;
                            }
                            else
                            {
                                attackDamage = step1Damage/thisAttackTargets.Count;
                                criticalHit = false;
                            }                          
                        }
                        break;
                    case 1:
                        anim.SetTrigger("Attack2");
                        if (!rangedAbilityFlag && !playSpellAnimation) { swing2.Play(); }
                        if (playSpellAnimation) {
                            spellDamageToDeal += step2Damage;
                           //MageFX1.gameObject.SetActive(false);
                            MageFX2.gameObject.SetActive(true);
                        } else {
                            if (rollCritChance())
                            {
                                attackDamage = step1Damage * 2;
                                criticalHit = true;
                            }
                            else
                            {
                                attackDamage = step1Damage / thisAttackTargets.Count;
                                criticalHit = false;
                            }
                        }
                        break;
                    case 2:
                        anim.SetTrigger("Attack3");
                        if (!rangedAbilityFlag && !playSpellAnimation) { swing3.Play(); }
                        if (playSpellAnimation) {
                            spellDamageToDeal += step3Damage;
                            //MageFX2.gameObject.SetActive(false);
                            MageFX3.gameObject.SetActive(true);
                        } else {
                            if (rollCritChance())
                            {
                                attackDamage = step1Damage * 2;
                                criticalHit = true;
                            }
                            else
                            {
                                attackDamage = step1Damage;
                                criticalHit = false;
                            }
                        }
                        break;
                }

                if (
                    tar.gameObject.GetComponent<Unit>().WillDieFromDamage(attackDamage) ||
                    tar.gameObject.GetComponent<Unit>().WillDieFromDamage(spellDamageToDeal)
                    ) //if lethal damage
                {
                    if (thisAttackTargets.Count == 1)
                    {
                        listenForComboInput = false;
                        Destroy(theFloatingInputText);
                        anim.SetTrigger("ComboFail"); //Isn't actually a fail, just exit to idle because enemy dead
                        pauseHourglass = true;
                    }
                    Destroy(theFloatingInputText);
                    if (!playSpellAnimation) //if melee do immediate damage
                    {
                        if (rangedAbilityFlag) //ranged
                        {
                            GameObject arrow = RangerArrowFactory.CreateArrow(attackDamage/thisAttackTargets.Count, tar, this.gameObject, criticalHit);
                           // Debug.Log("Final blow to  " + tar.gameObject.name + "dealing " + attackDamage);
                            if (thisAttackTargets.Count == 1)
                            {
                                Invoke("EndCombat", 0.5f);
                            }
                            else
                            {
                                //someone take target out of list
                            }

                        }
                        else //melee
                        {
                            if (criticalHit)
                            {
                                tar.GetComponent<Unit>().takeCriticalDamage(attackDamage, aggressor);
                            }
                            else
                            {
                                tar.GetComponent<Unit>().takeDamage(attackDamage, aggressor);
                            }

                           // Debug.Log("Final blow to  " + tar.gameObject.name + "dealing " + attackDamage);
                            if (thisAttackTargets.Count == 1)
                            {
                                Invoke("EndCombat", 0.5f);
                            }
                            else
                            {
                                //someone take target out of list
                            }
                        }
                    }
                    else //if spell, accumulate damage automatically
                    {
                        if (thisAttackTargets.Count == 1)
                        {
                            Invoke("EndCombat", 0.5f);
                        }
                    }
                }
                else //if hit is not lethal
                {
                    if (!playSpellAnimation)//if a melee, do damage immediately
                    {
                        if (rangedAbilityFlag && tar != null)   //ranged
                        {
                            GameObject arrow = RangerArrowFactory.CreateArrow(attackDamage, tar, this.gameObject,criticalHit);
                        }
                        else                                    //melee
                        {
                            if (stepsIntoCombo == 1) {
                                tar.GetComponent<Unit>().takeCriticalDamage(attackDamage, aggressor);
                            }
                            else
                            {
                                if (criticalHit)
                                {
                                    tar.GetComponent<Unit>().takeCriticalDamage(attackDamage, aggressor);
                                }else
                                {
                                    tar.GetComponent<Unit>().takeDamage(attackDamage, aggressor);
                                }
                                
                            }
                            

                        }
                    }
                    else //if spell accumulate damage
                    {

                    }
                }
            }
        }
    }

    public void setAbilityActivator(GameObject theCaster)
    {
        aggressor = theCaster;
    }

    public void BeginComboInput(List<KeyCode> buttonSequence, List<GameObject> thisAttackTargetsIn, int[] damageSteps,bool hasCastingExitAnimation,bool isRangedPhysical)
    {
       // Debug.Log("Targetting "+ thisAttackTargetsIn[0].collider.gameObject.name);
        if(isRangedPhysical){
            rangedAbilityFlag = true;
        }
        else
        {
            rangedAbilityFlag = false;
        }
        
        //Debug.Log(tar.name);
        spellDamageToDeal = 3;
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

    //Default combo windows are a generous 3 seconds
    public void setCombo(KeyCode targetButton, int comboStep)
    {
        // Debug.Log(targetButton + "set as target button as targetButton:"+comboStep);
        validInputButton = targetButton;
        InitInputText(validInputButton.ToString());
        // baseDamage =  myHero.weap.getDamage() +myHero.attack;
        stepsIntoCombo = comboStep;
        timerWindow = 3f;
        waitTimeBeforeInput = 2.9999999f;
        pauseHourglass = false;
        Invoke("EnableInput",.5f);
    }

    //Combo with modified start/end windows
    //The closer to 0 wait this long is, the smaller the window, max 2f
    public void setCombo(KeyCode targetButton, int comboStep, float totalInputWindow, float waitThisLong)
    {
        // Debug.Log(targetButton + "set as target button as targetButton:"+comboStep);
        validInputButton = targetButton;
        // baseDamage =  myHero.weap.getDamage() +myHero.attack;
        stepsIntoCombo = comboStep;
        timerWindow = totalInputWindow;
        waitTimeBeforeInput = waitThisLong;
        pauseHourglass = false;
        Invoke("EnableInput", .5f);
    }

    public void setThisSpellName(string spellNameIn)
    {
        spellName = spellNameIn;
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
        MageFX3.gameObject.SetActive(false);
        MageFX2.gameObject.SetActive(false);
        MageFX1.gameObject.SetActive(false);
        turnTimeElapsed.enabled = false;

        if (playSpellAnimation &&!castFirstFail) //if a spell attack
        {
            // Debug.Log("~~~Cast a spell!");
            //LogDamage(spellDamageToDeal);            // Debug.Log("~~~Cast a spell!");
            //LogDamage(spellDamageToDeal);
            List<GameObject> spells;
            List<GameObject> spellTargets;

            if (thisAttackTargets.Count >1 )
            {
                spellTargets = new List<GameObject>();
            }
            if (tar != null)
            {
                Debug.Log("Combo finished and starting to cast: "+spellName);
                switch (spellName)
                {
                    case "Fireball":
                        GameObject spell = GetComponent<SpellFactory>().CreateFireball(spellDamageToDeal, tar, this.gameObject);
                        break;
                    case "LightningStorm":
                        spellDamageToDeal = splitDamageBetweenTargets();
                        spells = GetComponent<SpellFactory>().CreateLightningStorm(spellDamageToDeal, thisAttackTargets, this.gameObject);
                        break;
                    case "MissileStorm":
                        spellDamageToDeal = splitDamageBetweenTargets();
                        spells = GetComponent<SpellFactory>().CreateMissileBarrage(spellDamageToDeal, thisAttackTargets, this.gameObject);
                        break;
                    case "": break;
                }
            }

            Invoke("EndTurn", 1f);
           /* GameObject spell = SpellFactory.CreateFireball(spellDamageToDeal, tar, this.gameObject);
            Invoke("EndTurn", 1f);*/
        }
        else //if a melee attack
        {
           // Debug.Log("~~~~Physical combo completed");
            //this.gameObject.GetComponent<BasicRayAttack>().clearTarget();
            cam.SendMessage("UnsetCombatZoom");
            pauseHourglass = true;
            listenForComboInput = false;
            discreteInputWindow = false;
            Invoke("EndTurn", 1f);
        }
    }

    private void InitInputText(string textIn)
    {
        theFloatingInputText = Instantiate(InputUIPrefab) as GameObject;
        inputAnim = theFloatingInputText.GetComponent<Animator>();
        
        RectTransform tempRect = theFloatingInputText.GetComponent<RectTransform>();
        tempRect.transform.SetParent(cam.transform.FindChild("MainCanvas"));
        tempRect.transform.localPosition = InputUIPrefab.transform.localPosition;
        tempRect.transform.localScale = InputUIPrefab.transform.localScale;
        tempRect.transform.localRotation = InputUIPrefab.transform.localRotation;
        // Debug.Log("!!!tried to UI " + inputAnim.gameObject.name);
        inputAnim.SetTrigger("ShowInput");
        //theFloatingInputText.GetComponent<Text>().enabled = true;
        theFloatingInputText.GetComponent<Text>().text = textIn;
       // Destroy(theFloatingInputText.gameObject,3);
    }

    private void UIWindowOpen()
    {

    }

    private int splitDamageBetweenTargets()
    {
		if (thisAttackTargets.Count==0) {
			return spellDamageToDeal;
		}
        return spellDamageToDeal/thisAttackTargets.Count;
    }


    public void EndTurn()  
    {
        anim.SetTrigger("ComboFail");
        // this.gameObject.GetComponent<BasicRayAttack>().clearTarget();
        this.gameObject.GetComponent<PlayerMovement>().EndTurn();
    }

}
