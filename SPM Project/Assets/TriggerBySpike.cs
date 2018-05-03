using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBySpike : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Hazard")) {
            transform.parent.gameObject.SetActive(false);
        }


    }
}
