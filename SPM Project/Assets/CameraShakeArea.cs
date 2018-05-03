using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeArea : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            CameraShake.AddIntensity(20);
            col.gameObject.GetComponent<PlayerController>().TransitionTo<HurtState>();
        }
    }
}
