using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour {
    public float CollapseTime;
    public float ResetTime;
    public float fadeOutTime;
    public float fadeInTime;

    private SpriteRenderer _renderer;
    private Collider2D _collider;
    private bool _collapsing = false;

	private AudioSource [] source;
	[Header("Audio")]
	public AudioClip [] breaking;
    [ReadOnly] public AudioClip BreakingLastPlayed;
	public AudioClip rebuilding;

    private Vector3 OGPos;

    void Awake() {
        //_renderer = GetComponent<SpriteRenderer>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        OGPos = transform.position;
        source = GetComponents<AudioSource>();
    }

    void OnEnable () {
        transform.position = OGPos;
        _renderer.color = GetComponentInChildren<SpriteRenderer>().color;
        _collider.enabled = true;
        _collapsing = false;
    }

    private IEnumerator Collapse(float collapseTime, float resetTime) {
        _collapsing = true;
        yield return new WaitForSeconds(collapseTime);
        Color c = _renderer.color;
        Debug.Log(_renderer.ToString());
        for (float i = 1; i >= 0; i -= (1/ fadeOutTime) * Time.deltaTime)
        {
            _renderer.color = new Color(c.r, c.b, c.g, i);
            if (i < 0.1f)
            {
                _renderer.color = new Color(c.r, c.b, c.g, 0);
                break;
            }
            yield return null;
        }
        _collider.enabled = false;
        _renderer.enabled = false;
        int length = breaking.Length;
        int replace = UnityEngine.Random.Range(0, (length - 1));
        source[0].clip = breaking[replace];
        source[0].Play();
        BreakingLastPlayed = breaking[replace];
        breaking[replace] = breaking[length - 1];
        breaking[length - 1] = BreakingLastPlayed;
        source[1].clip = rebuilding;
        source[1].PlayDelayed(resetTime - 1);
        yield return new WaitForSeconds(resetTime);
        for (float i = 0; i <= 1; i += (1 / fadeInTime) * Time.deltaTime)
        {
            _renderer.color = new Color(c.r, c.b, c.g, i);
            yield return null;
        }
        _collider.enabled = true;
        _collapsing = false;

    }

    private void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Player" && !_collapsing) {
            StartCoroutine(Collapse(CollapseTime, ResetTime));
        }
    }
}
