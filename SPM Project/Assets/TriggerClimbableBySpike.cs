using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerClimbableBySpike : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Hazard")) {
			GameObject.Find("ChangeToClimb").tag = "Untagged";
		}


	}
}

