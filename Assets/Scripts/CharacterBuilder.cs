using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterBuilder : MonoBehaviour {

    public PlayerAttackController attackCtrlr;
    public List<GameObject> activeAbilities;

    // Use this for initialization
    void Start () {
        attackCtrlr = GetComponent<PlayerAttackController>();
        Invoke("setAttackControls",1f);
	}	 

    private void setAttackControls(){
        attackCtrlr.buildActiveAbilites(activeAbilities);

   }



}
