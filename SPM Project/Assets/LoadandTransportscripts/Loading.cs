using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {
    public string leveltoload;
    public string portalName;
    private void Update()
    {
        if (GameManager.instance.Level1Done == false && portalName == "portal2") {
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
