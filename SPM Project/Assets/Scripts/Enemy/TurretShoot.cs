using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour {

    private ShootingSystem AI;
    private bool _shooting;

	
	void Start () {
        AI = transform.parent.GetChild(0).GetComponent<ShootingSystem>();
	}

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
			//audio
			AI.source.clip = AI.Alerted;
			AI.source.Play ();

            _shooting = true;
            AI.awake = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
			//audio
			AI.source.clip = AI.Retract;
			AI.source.Play ();

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
