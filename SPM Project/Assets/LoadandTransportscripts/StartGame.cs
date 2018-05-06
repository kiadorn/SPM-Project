using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
    public bool loadgame;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		
	}
   public void loadOnClick()
    {
        Debug.Log("Pressing");
        if (loadgame == true)
        {
            SceneManager.LoadScene("Hub");
            GameManager.LoadPlayer();
        }
        else SceneManager.LoadScene("Hub");
    }
}
