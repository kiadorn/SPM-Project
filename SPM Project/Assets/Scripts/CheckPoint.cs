using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

	private AudioSource source;
	[Header("Audio")]
	public AudioClip Checkpoint;

    Vector2 position;
    public bool Latest = true;

    public PlayerStats Stats;

    public GameObject[] ObjectsToReset;

	// Use this for initialization
	void Awake () {
        Stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
		source = GetComponent<AudioSource> ();
		source.clip = Checkpoint;
	}


    public void EnableEnemies() {
        foreach(GameObject resetObject in ObjectsToReset) {
            resetObject.SetActive(false);
            resetObject.SetActive(true);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (Latest && !other.gameObject.GetComponent<PlayerStats>().dead) {
                if (Stats.CurrentCheckPoint != null) Stats.CurrentCheckPoint.transform.GetChild(0).gameObject.SetActive(false);
                Stats.CurrentCheckPoint = this;
                Stats.SavedCurrency = Stats.Currency;
                Latest = false;
                transform.GetChild(0).gameObject.SetActive(true);
				source.Play ();
            }
        }
    }
}
