using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitSound : MonoBehaviour {

	private AudioSource source;
	[Header("Audio")]
	public AudioClip[] TakeDamageSound;

	public void Start () {
		source = GetComponent<AudioSource> ();
	}

	public void TakeDamage(){
		source.clip = TakeDamageSound [Random.Range (0, TakeDamageSound.Length)];
		source.Play ();
	}
}
