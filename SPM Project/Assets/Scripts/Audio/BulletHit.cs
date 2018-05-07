using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour {

	private GameObject Player;
 
	public AudioSource source;
	[Header ("Audio Clips")]
	public AudioClip [] Impact;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		Player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayImpact(){
		if (!Player.GetComponent<PlayerStats>()._invulnerable) {
			source.clip = Impact [Random.Range (0, Impact.Length)];
			source.Play ();
		}
	}
}
