using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour {

    public ShootingSystem AI;
    private bool _shooting;

	/*
	void Awake () {
        AI = GetComponentInParent<ShootingSystem>();
	}*/

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            _shooting = true;
            AI.awake = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            _shooting = false;
            AI.awake = false;
        }
    }

    void Update () {
        if (_shooting && AI.CanShoot) {
            AI.Shoot();
        }
    }
}
