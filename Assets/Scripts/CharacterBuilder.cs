using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterBuilder : MonoBehaviour {

    public PlayerAttackController attackCtrlr;
    public List<GameObject> activeAbilities;

    // Use this for initialization
    void Start () {
        attackCtrlr = GetComponent<PlayerAttackController>();
        setAttackControls();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
   

    private void setAttackControls(){
        attackCtrlr.buildActiveAbilites(activeAbilities);

   }



}
