using UnityEngine;
using System.Collections;

public class RangerArrowFactory : MonoBehaviour {

    protected static RangerArrowFactory instance;

    public GameObject arrowPrefab;

	// Use this for initialization
	void Start () {
        instance = this;
	}

    public static GameObject CreateArrow(int damage, GameObject target, GameObject caster, bool criticalHit)
    {
       // Debug.Log("Shoot an arrow at "+target.name);
        GameObject arrow = (GameObject)Object.Instantiate(instance.arrowPrefab, Vector3.zero, Quaternion.identity);
        arrow.GetComponent<ArrowFlight>().Initialize(damage, target, caster, criticalHit);
        return arrow;
    }
}
