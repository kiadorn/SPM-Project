using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
    public PlayerStats stats;

	void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.CompareTag("Player")) {
            stats.hasKey = true;
            this.gameObject.SetActive(false);
        }
    }


}
