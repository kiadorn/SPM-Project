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

	private AudioSource source;
	[Header("Audio")]
	public AudioClip breaking;
	public AudioClip rebuilding;

    private Vector3 OGPos;

    void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        OGPos = transform.position;
    }

    // Update is called once per frame
    void OnEnable () {
        transform.position = OGPos;
        _renderer.color = new Color(1, 1, 1, 1);
        _collider.enabled = true;
        _collapsing = false;
		source = GetComponent<AudioSource> ();
    }

    private IEnumerator Collapse(float collapseTime, float resetTime) {
        _collapsing = true;
        yield return new WaitForSeconds(collapseTime);
        Debug.Log("Börjar breaking");
        source.clip = breaking;
        source.Play();
        for (float i = 1; i >= 0; i -= (1/ fadeOutTime) * Time.deltaTime)
        {
            _renderer.color = new Color(1, 1, 1, i);
            Debug.Log(i);
            if (i < 0.1f)
            {
                _renderer.color = new Color(1, 1, 1, 0);
                break;
            }
            yield return null;
        }
        _collider.enabled = false;
        yield return new WaitForSeconds(resetTime);
        source.clip = rebuilding;
        source.Play();
        for (float i = 0; i <= 1; i += (1 / fadeInTime) * Time.deltaTime)
        {
            _renderer.color = new Color(1, 1, 1, i);
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
