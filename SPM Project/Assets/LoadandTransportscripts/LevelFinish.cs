using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelFinish : MonoBehaviour {
    public string leveltoload;

    private void OnTriggerEnter2D(Collider2D lookforplayer)
    {
        if (lookforplayer.gameObject.tag == "Player")
        {

            SceneManager.LoadScene(leveltoload);



        }
        Debug.Log("Colliding");
    }
}
