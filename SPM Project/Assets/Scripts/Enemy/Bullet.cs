using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private PlayerStats Stats;
    void OnCollisionEnter2D(Collision2D coll) {

        if (coll.gameObject.CompareTag("Player")) {
			//audio
			Stats.BulletHit();
            coll.gameObject.GetComponent<PlayerStats>().ChangeHealth(-1);
        }
        //if (!coll.gameObject.CompareTag("Bullet"))
        //{
            Destroy(gameObject);
        //}
        
    }

	// Use this for initialization
	void Start () {
		//audio
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
