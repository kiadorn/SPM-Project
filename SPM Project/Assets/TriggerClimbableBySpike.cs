using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerClimbableBySpike : MonoBehaviour {

	private AudioSource source;
	[Header("Audio")]
	public AudioClip [] trigger;

	public void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Hazard")) {
			source.clip = trigger [Random.Range (0, trigger.Length)];
			source.Play ();
			GameObject.Find("ChangeToClimb").tag = "Untagged";
		}
	}

	public void Start(){
		source = GetComponent<AudioSource> ();
	}
}

