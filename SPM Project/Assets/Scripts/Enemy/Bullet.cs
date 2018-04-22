using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


    void OnCollisionEnter2D(Collision2D coll) {

        if (coll.gameObject.CompareTag("Player")) {
            PlayerStats stats = GameObject.Find("UI").GetComponent<PlayerStats>();
            stats.ChangeHealth(-1);
        }
        Destroy(gameObject);
        
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
