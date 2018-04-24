using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {
    public int portalNum;
    public string leveltoload;

    private void Update()
    {
        if (portalNum != GameManager.instance.LevelsDone) {
            this.gameObject.SetActive(false);
                }
    }



    private void OnTriggerEnter2D(Collider2D lookforplayer)
    {
        if(lookforplayer.gameObject.tag == "Player")
            {

                SceneManager.LoadScene(leveltoload);

            }
        Debug.Log("Colliding");
    }
 
}
