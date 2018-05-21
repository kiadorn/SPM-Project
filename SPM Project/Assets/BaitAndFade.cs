using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitAndFade : MonoBehaviour {

    public SpriteRenderer[] fakes;
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
            foreach (SpriteRenderer r in fakes)
            {
                StartCoroutine(FadeOut(r));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            foreach (SpriteRenderer r in fakes)
            {
                StartCoroutine(FadeIn(r));
            }
            
        }
    }

    IEnumerator FadeIn(SpriteRenderer r)
    {
        float cAlpha = r.color.a;
        for (float i = cAlpha; i <= 1; i += Time.deltaTime)
        {
            r.color = new Color(1, 1, 1, i);
            yield return null;
        }
        yield return 0;
    }

    IEnumerator FadeOut(SpriteRenderer r)
    {
        float cAlpha = r.color.a;
        for (float i = cAlpha; i >= 0; i -= Time.deltaTime)
        {
            r.color = new Color(1, 1, 1, i);
            yield return null;
        }
        yield return 0;
    }


}
