using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour {
    public float CollapseTime = 3f;
    public float ResetTime = 5f;

    private MeshRenderer _renderer;
    private Collider2D _collider;
    private bool _collapsing = false;

	private AudioSource source;
	[Header("Audio")]
	public AudioClip breaking;
	public AudioClip rebuilding;

    private Vector3 OGPos;

    void Awake() {
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider2D>();
        OGPos = transform.position;
    }

    // Update is called once per frame
    void OnEnable () {
        transform.position = OGPos;
        _renderer.enabled = true;
        _collider.enabled = true;
        _collapsing = false;
		source = GetComponent<AudioSource> ();
    }

    private IEnumerator Collapse(float collapseTime, float resetTime) {
        _collapsing = true;
        yield return new WaitForSeconds(collapseTime);
        _collider.enabled = false;
        _renderer.enabled = false;
		source.clip = breaking;
		source.Play ();
        yield return new WaitForSeconds(resetTime);
        _renderer.enabled = true;
        _collider.enabled = true;
        _collapsing = false; 
		source.clip = rebuilding;
		source.Play ();
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Player" && !_collapsing) {
            StartCoroutine(Collapse(CollapseTime, ResetTime));
        }
    }
}
