using UnityEngine;
using System.Collections;

public class TheHourglass : MonoBehaviour
{

    // public ArrayList spawners = new ArrayList();
    public GameObject theExplorer;

    // Use this for initialization
    void Start()
    {
        Invoke("tellPlayerToStart", 2f);  //begin exploring
    }

    void GetSpawners()
    {
        // spawners.Clear();
        // spawners.AddRange(GameObject.FindGameObjectsWithTag("Spawner"));

    }

    private void tellPlayerToStart()
    {
        //later
        theExplorer.SendMessage("EnterExplorationMode");
    }

}

