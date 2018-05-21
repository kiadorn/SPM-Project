using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class StartGame : MonoBehaviour {
    public bool loadgame;
    public FadeBlackScreen BlackScreen;

   public void loadOnClick()
    {
        if (loadgame == true)
        {
            GameManager.LoadPlayer();
        }
        GameManager.SavePlayer();
        BlackScreen.StartFadeIn("Hub");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
