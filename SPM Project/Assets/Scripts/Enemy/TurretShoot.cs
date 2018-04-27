using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour {


    public TurretController Controller;
	


    void Awake() {
        Controller = transform.parent.GetComponent<TurretController>();
    }



    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            Controller.EnteredZone();
			//audio
			AI.source.clip = AI.Alerted;
			AI.source.Play ();

            _shooting = true;
            AI.awake = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            Controller.ExitedZone();
			//audio
			AI.source.clip = AI.Retract;
			AI.source.Play ();

            _shooting = false;
            AI.awake = false;
        }
    }


}
