using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeBlackScreen : MonoBehaviour {

    private Image BlackScreen;

	void Start () {
        BlackScreen = GetComponent<Image>();
        BlackScreen.color = new Color(0, 0, 0, 1);
        StartCoroutine(FadeOut());
	}


    public void StartFadeIn(string levelName)
    {
        StartCoroutine(FadeIn(levelName));
    }

    IEnumerator FadeIn(string levelName)
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            BlackScreen.color = new Color(0, 0, 0, i);
            yield return null;
        }
        SceneManager.LoadScene(levelName);
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
}
