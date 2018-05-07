using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardReset : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Hazard")) {
            col.gameObject.transform.parent.gameObject.SetActive(false);
            col.gameObject.transform.parent.gameObject.SetActive(true);
            CameraShake.AddIntensity(0.2f);
        }
    }
}
