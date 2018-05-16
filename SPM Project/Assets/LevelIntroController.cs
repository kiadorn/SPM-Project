using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelIntroController : MonoBehaviour {
    //public int Screen1Time;  //Hårdkodning
    //public int Screen2Time;
    //public int Screen3Time;
    //public GameObject Screen1Object;
    //public GameObject Screen2Object;
    public float ScreenTime;
    public GameObject[] Screens;
    public string SceneToLoad;
    public Text SkipText;
    public Image BlackScreen;
    AsyncOperation asyncLoad;

    void Start () {
        StartCoroutine(Cinematic());
        StartCoroutine(SkipTextAnimation());
	}

    void Update() {
        if (Input.GetButtonDown("Pause")) {
            asyncLoad.allowSceneActivation = true;
        }
    }
	
	IEnumerator Cinematic() {
        asyncLoad = SceneManager.LoadSceneAsync(SceneToLoad);
        asyncLoad.allowSceneActivation = false;
        for(int i = 0; i<Screens.Length; i++) {
            yield return new WaitForSeconds(ScreenTime);
            if ((i+1 < Screens.Length)) {
                Screens[i].SetActive(false);
                Screens[i + 1].SetActive(true);
            }
        }
        for (float i = 0; i <= 1; i += Time.deltaTime) {
            BlackScreen.color = new Color(0, 0, 0, i);
            yield return null;
        }


        //yield return new WaitForSeconds(Screen1Time);         //Hårdkodning
        //Screen1Object.SetActive(false);
        //yield return new WaitForSeconds(Screen2Time);
        //Screen2Object.SetActive(false);
        //yield return new WaitForSeconds(Screen3Time);
        asyncLoad.allowSceneActivation = true;
        yield return 0;
    }

    public IEnumerator SkipTextAnimation() {
        for (float i = 1; i >= 0; i -= Time.deltaTime) {
            SkipText.color = new Color(0, 0, 0, i);
            yield return null;
        }
        for (float i = 0; i <= 1; i += Time.deltaTime) {
            SkipText.color = new Color(0, 0, 0, i);
            yield return null;
        }
        StartCoroutine(SkipTextAnimation());
    }
}
