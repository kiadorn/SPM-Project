using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBySpike : MonoBehaviour {

	private AudioSource source;
	[Header("Audio")]
	public AudioClip [] trigger;

	public MovePlatformAuto[] platformScripts;

	private GameObject obtain;

	public void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Hazard")) {
			source.clip = trigger [Random.Range (0, trigger.Length)];
			source.Play ();
			transform.parent.gameObject.GetComponent<MeshRenderer>().enabled = false;
			transform.parent.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			foreach(MovePlatformAuto mo in platformScripts) {
				mo.enabled = true;
				obtain.GetComponent<ObjectObtain> ().StartTrigger ();
			}

        }
    }

	public void Start(){
		source = GetComponent<AudioSource> ();
		obtain = GameObject.Find ("ObjectObtain");
	}
}
