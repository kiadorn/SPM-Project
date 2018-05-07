using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private GameObject BulletHitSound;

    void OnCollisionEnter2D(Collision2D coll) {

        if (coll.gameObject.CompareTag("Player")) {
			BulletHitSound.GetComponent<BulletHit>().PlayImpact();
            coll.gameObject.GetComponent<PlayerStats>().ChangeHealth(-1);

        }
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
		BulletHitSound = GameObject.Find ("BulletHitSound");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
