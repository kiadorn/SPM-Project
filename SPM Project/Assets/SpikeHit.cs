using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHit : MonoBehaviour {

	private GameObject Player;
	public bool played;
	public AudioSource source;
	[Header ("Audio Clips")]
	public AudioClip [] Collision;

	// Use this for initialization
	public void Start () {
		source = GetComponent<AudioSource> ();
		Player = GameObject.Find ("Player");
		played = false;
	}

	// Update is called once per frame
	public void Update () {

	}

	public void PlayImpact(){
		if (!Player.GetComponent<PlayerStats>()._invulnerable) {
			source.clip = Collision [Random.Range (0, Collision.Length)];
			source.Play ();
			played = true;
		}
	}
}
