using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {

	private GameObject SpikeHit;

    [Range(0, 10)]
    public int damageValue;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SpikeHit.GetComponent<SpikeHit>().played = false;
            if (!SpikeHit.GetComponent<SpikeHit> ().played) {
				SpikeHit.GetComponent<SpikeHit> ().PlayImpact ();
                SpikeHit.GetComponent<SpikeHit>().played = true;
            }
			collision.gameObject.GetComponent<PlayerStats>().ChangeHealth(-1 * damageValue);
        }
    }

	public void Start(){
		SpikeHit = GameObject.Find ("SpikeHitSound");
    }

}
