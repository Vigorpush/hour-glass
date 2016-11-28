using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnTextBubble : MonoBehaviour
{

    public float curHP;
    public float maxHP;

    // public Text[] bubbleTextArray;
    public Text bubbleText;
    public GameObject unit;
    public Transform bubbleTf;

    //put this into field
    string crytext;



    public GameObject mycanvasBubbleText;
    // Use this for initialization
    void Start()
    {
        unit = gameObject.transform.root.gameObject;
        curHP = unit.GetComponent<Unit>().getcurhp();
        maxHP = unit.GetComponent<Unit>().getmaxhp();
       
        bubbleText = transform.FindChild("EnemyCanvas/bubbleText").GetComponent<Text>();
       
    }



    public void gotHit(int damage)
    {
        curHP -= damage;

        //put this code in update
        Debug.Log(" I am " + unit.name + " and my hp is:" + curHP);
        if ((curHP > 0) && (curHP < maxHP))
        {
            int intelligence = Random.Range(1, 6);
            switch (intelligence)
            {
                case 5:
                    StartCoroutine(ShowMessage("Ow!", 2));
                    break;
                case 4:
                    StartCoroutine(ShowMessage("Nngh.", 2));
                    break;
                case 3:
                    StartCoroutine(ShowMessage("Gah!", 2));
                    break;
                case 2:
                    StartCoroutine(ShowMessage("*indistinguishable noises*", 2));
                    break;
                case 1:
                    StartCoroutine(ShowMessage("Erk", 2));
                    break;
                default:
                    StartCoroutine(ShowMessage("Ugh!", 2));
                    break;
            }

        }
        if ((curHP < maxHP * 0.5) && (curHP >= maxHP * 0.2))
        {
            StartCoroutine(ShowMessage("Oh, That was hurt!", 2));
        }



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
