using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnTextBubble : MonoBehaviour
{

    public float curHP;
    public float maxHP;
	public Unit myStats;
	public string[] hitList = new string[6];
	public string suicideText;
    // public Text[] bubbleTextArray;
    public Text bubbleText;
    public GameObject unit;
    public Transform bubbleTf;

    //put this into field
    public string crytext;



    public GameObject mycanvasBubbleText;
    // Use this for initialization
    void Start()
    {
        unit = gameObject.transform.root.gameObject;
		myStats = unit.GetComponent<Unit> ();
        
       
        bubbleText = transform.FindChild("EnemyCanvas/bubbleText").GetComponent<Text>();
       
    }



    public void gotHit(int damage)
    {
        curHP -= damage;

        //put this code in update
      //  Debug.Log(" I am " + unit.name + " and my hp is:" + curHP);
		if ((curHP > 0) && (myStats.curhp < myStats.maxhp))
        {
            int intelligence = Random.Range(0, 5);
//
//            switch (intelligence)
//            {
//                case 5:
//                    StartCoroutine(ShowMessage("Ow!", 2));
//                    break;
//                case 4:
//                    StartCoroutine(ShowMessage("Argh!", 2));
//                    break;
//                case 3:
//                    StartCoroutine(ShowMessage("Gah!", 2));
//                    break;
//                case 2:
//                    StartCoroutine(ShowMessage("*indistinguishable noises*", 2));
//                    break;
//                case 1:
//                    StartCoroutine(ShowMessage("Erk", 2));
//                    break;
//                default:
//                    StartCoroutine(ShowMessage("Ugh!", 2));
//                    break;
//            }


			StartCoroutine(ShowMessage(hitList[intelligence], 2));

			}

        
		else if ((curHP < maxHP * 0.3f) )
        {
			StartCoroutine(ShowMessage(crytext, 2));
        }




    }

	public void suicide(){
		ShowMessage(suicideText,3f);
	}

    //put this into what ever
    IEnumerator ShowMessage(string message, float delay)
    {
        bubbleText.text = message;
        bubbleText.enabled = true;
        yield return new WaitForSeconds(delay);
        bubbleText.enabled = false;
    }


}
