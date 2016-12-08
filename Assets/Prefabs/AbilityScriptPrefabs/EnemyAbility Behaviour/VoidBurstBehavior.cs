using UnityEngine;
using System.Collections;

public class VoidBurstBehavior : MonoBehaviour {

    private Light thisAura;
    private bool animating;
    private float intensityCurrent;
    private float MAX_INTENSITY_VALUE = 8f;
    public int Range = 3;
    public ArrayList hitList;
    private int damageToDeal;
    private GameObject caster;

	// Use this for initialization
	void Start () {
        thisAura = GetComponent<Light>();
        intensityCurrent = 1f;
        animating = false; 
	}
	
	// Update is called once per frame
	void Update () {
	    if (animating && intensityCurrent < MAX_INTENSITY_VALUE){
            intensityCurrent += 1f;
        } 
        if (intensityCurrent == MAX_INTENSITY_VALUE){
            foreach (HeroUnit unit in hitList)
            {
                unit.takeDamage(damageToDeal, caster);
            }
            Debug.Log("Void Burst dealing damage!!!");
            Destroy(this.gameObject);
        }
	}

    public void InitializeBurst(GameObject theMainTarget, GameObject[] possibleTargets, GameObject theCaster, int damage)
    {
        this.transform.position = theMainTarget.transform.position;
        caster = theCaster;
        damageToDeal = damage;
        if (hitList == null)
        {
            hitList = new ArrayList();
        }
        foreach(GameObject tar in possibleTargets){
            if(Mathf.CeilToInt(Vector3.Distance(tar.transform.position,transform.position))<= Range){
                hitList.Add(tar.GetComponent<HeroUnit>());
            }
        }

        transform.position += new Vector3(0, 0, -1);
        animating = true;
    }



}
