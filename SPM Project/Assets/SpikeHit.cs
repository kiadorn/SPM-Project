using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHit : MonoBehaviour {

	private GameObject Player;
	public bool played;
	[HideInInspector]public AudioSource source;
	[Header ("Audio Clips")]
	public AudioClip [] Collision;
    [ReadOnly] public AudioClip CollisionLastPlayed;

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
            int length = Collision.Length;
            int replace = Random.Range(0, (length - 1));
            source.clip = Collision[replace];
            source.Play();
            CollisionLastPlayed = Collision[replace];
            Collision[replace] = Collision[length - 1];
            Collision[length - 1] = CollisionLastPlayed;
		}
	}
}
