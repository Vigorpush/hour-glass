using UnityEngine;
using System.Collections;

public class SpellFactory : MonoBehaviour {

    protected static SpellFactory instance;

    public GameObject fireballPrefab;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
    public static GameObject CreateFireball(int damage, GameObject target, GameObject caster)
    {
        GameObject spell = (GameObject)Object.Instantiate(instance.fireballPrefab, Vector3.zero, Quaternion.identity);
        spell.GetComponent<FireballFlight>().Initialize(damage,target,caster);
        return spell;
    }
}
