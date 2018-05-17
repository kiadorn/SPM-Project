using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour {
    public float ColapseTime = 3f;
    public float ResetTime = 5f;

    private MeshRenderer _renderer;
    private Collider2D _collider;
    private bool _collapsing = false;

	private AudioSource [] source;
	[Header("Audio")]
	public AudioClip [] breaking;
    [ReadOnly] public AudioClip BreakingLastPlayed;
	public AudioClip rebuilding;

    private Vector3 OGPos;

    void Awake() {
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider2D>();
        OGPos = transform.position;
        source = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void OnEnable () {
        transform.position = OGPos;
        _renderer.enabled = true;
        _collider.enabled = true;
        _collapsing = false;
    }

    private IEnumerator Colapse(float colapseTime, float resetTime) {
        _collapsing = true;
        yield return new WaitForSeconds(colapseTime);
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
        _renderer.enabled = true;
        _collider.enabled = true;
        _collapsing = false; 

    }

    private void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Player" && !_collapsing) {
            StartCoroutine(Colapse(ColapseTime, ResetTime));
        }
    }
}
