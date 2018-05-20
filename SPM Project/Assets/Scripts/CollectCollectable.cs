using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCollectable : MonoBehaviour {

	private AudioSource source;

	[Header("Audio")]
	public AudioClip []PickUpSounds;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            col.gameObject.GetComponent<PlayerStats>().ChangeCurrency(1);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(WaitForSound());
        }
    }

    private IEnumerator WaitForSound() {
		source.clip = PickUpSounds [Random.Range (0, PickUpSounds.Length)];
		source.Play ();
        yield return new WaitForSeconds(3f); //Ersätt tid med längd för ljudklipp
        gameObject.SetActive(false);
        yield return 0;
    }

    private void OnEnable() {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
		source = GetComponent<AudioSource> ();
    }
}