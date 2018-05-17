using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitAndFade : MonoBehaviour {

    private Color c;

    private void Awake()
    {
        c = GetComponent<SpriteRenderer>().color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeOut());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        float cAlpha = GetComponent<SpriteRenderer>().color.a;
        for (float i = cAlpha; i <= 1; i += Time.deltaTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, i);
            yield return null;
        }
        yield return 0;
    }

    IEnumerator FadeOut()
    {
        float cAlpha = GetComponent<SpriteRenderer>().color.a;
        for (float i = cAlpha; i >= 0; i -= Time.deltaTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, i);
            yield return null;
        }
        yield return 0;
    }
}
