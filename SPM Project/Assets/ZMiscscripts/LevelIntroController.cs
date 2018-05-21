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

        StartCoroutine(FadeOut());
        StartCoroutine(Cinematic());
        StartCoroutine(SkipTextAnimation());
	}

    void Update() {
        if (Input.GetButtonDown("Pause")) {
            StartCoroutine(LoadScene());
        }
    }
	
	IEnumerator Cinematic() {
        asyncLoad = SceneManager.LoadSceneAsync(SceneToLoad);
        asyncLoad.allowSceneActivation = false;
        for(int i = 0; i<Screens.Length; i++) {
            yield return new WaitForSeconds(ScreenTime);
            StartCoroutine(FadeInAndOut(i));
        }
        StartCoroutine(LoadScene());
        yield return 0;
    }

    IEnumerator LoadScene()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            BlackScreen.color = new Color(0, 0, 0, i);
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
        yield return 0;
    }

    IEnumerator FadeInAndOut(int image)
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            BlackScreen.color = new Color(0, 0, 0, i);
            yield return null;
        }
        if ((image + 1 < Screens.Length))
        {
            Screens[image].SetActive(false);
            Screens[image + 1].SetActive(true);
        }
        StartCoroutine(FadeOut());
        yield return 0;
    }

    IEnumerator FadeOut()
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            BlackScreen.color = new Color(0, 0, 0, i);
            yield return null;
        }
        yield return 0;
    }

    public IEnumerator SkipTextAnimation() {
        for (float i = 1; i >= 0; i -= Time.deltaTime) {
            SkipText.color = new Color(1, 1, 1, i);
            yield return null;
        }
        for (float i = 0; i <= 1; i += Time.deltaTime) {
            SkipText.color = new Color(1, 1, 1, i);
            yield return null;
        }
        StartCoroutine(SkipTextAnimation());
    }
}
