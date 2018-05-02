﻿using System.Collections;
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
		source = GetComponent<AudioSource> ();
		source.clip = Impact;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
