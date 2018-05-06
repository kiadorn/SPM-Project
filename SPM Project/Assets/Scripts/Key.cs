using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

	private GameObject obtain;
    void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.CompareTag("Player")) {
			obtain.GetComponent<ObjectObtain> ().PickUpKey ();
            col.gameObject.GetComponent<PlayerStats>().ChangeKeyStatus(true);
            this.gameObject.SetActive(false);
        }
    }

	void Start(){
		obtain = GameObject.Find("ObjectObtain");
	}

}
