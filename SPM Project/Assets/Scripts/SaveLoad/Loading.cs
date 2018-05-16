using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {
    public string leveltoload;
    public string portalName;
    public FadeBlackScreen BlackScreen;
    private void Start()
    {
        if (GameManager.instance.Level1Done == false && portalName == "portal2") {
            this.gameObject.SetActive(false);
        }

        if (GameManager.instance.Level2Done == false && portalName == "portal3")
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D lookforplayer)
    {
        if(lookforplayer.gameObject.tag == "Player")
        {
            BlackScreen.StartFadeIn(leveltoload);
            GameManager.instance.HealthPoints = 2;
        }
    }
 
}
