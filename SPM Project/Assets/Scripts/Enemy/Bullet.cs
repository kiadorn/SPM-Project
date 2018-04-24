using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	//audio
	private AudioSource source;
	public AudioClip Impact;

    void OnCollisionEnter2D(Collision2D coll) {

        if (coll.gameObject.CompareTag("Player")) {
			//audio
			source.Play();

            PlayerStats stats = GameObject.Find("UI").GetComponent<PlayerStats>();
            stats.ChangeHealth(-1);
        }
        Destroy(gameObject);
        
    }

	// Use this for initialization
	void Start () {
		//audio
		source = GetComponent<AudioSource> ();
		source.clip = Impact;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
