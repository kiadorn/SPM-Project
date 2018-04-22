using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
    public PlayerStats stats;

	void OnTriggerEnter2D(Collider2D col) {
        //stats.getKey();
        if (col.gameObject.CompareTag("Player")) {
            this.gameObject.SetActive(false);
        }
    }


}
