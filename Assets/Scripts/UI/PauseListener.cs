using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void RestartGame()
    {
        SceneManager.LoadScene("s1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp("escape"))
        {
            this.gameObject.SetActive(false);
        }

    
	}
}
