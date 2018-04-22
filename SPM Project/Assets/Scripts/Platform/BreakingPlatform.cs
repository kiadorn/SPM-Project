using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour {
    public int ColapseTime = 3;
    public int ResetTime = 5;


    private MeshRenderer _renderer;
    private Collider2D _collider;
    private bool _colapsing = false;


	// Use this for initialization
	void Start () {
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Colapse(int colapseTime, int resetTime) {
        Debug.Log("Hello");
        _colapsing = true;
        yield return new WaitForSeconds(colapseTime);
        _collider.enabled = false;
        _renderer.enabled = false;
        yield return new WaitForSeconds(resetTime);
        _renderer.enabled = true;
        _collider.enabled = true;
        _colapsing = false; 
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Player" && !_colapsing){
            StartCoroutine(Colapse(ColapseTime, ResetTime));
        }
    }
}
