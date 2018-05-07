using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelFinish : MonoBehaviour {
    public string leveltoload;
    public bool level1finish;
    public bool level2finish;
    private void OnTriggerEnter2D(Collider2D lookforplayer)
    {
        if (lookforplayer.gameObject.tag == "Player")
        {

            if (level1finish == true)
            {

                GameManager.instance.Level1Done = true;
            }
            if (level2finish == true) { 
                GameManager.instance.Level2Done = true;

        }
            SceneManager.LoadScene(leveltoload);


        }
        Debug.Log("Colliding");
    }
}
