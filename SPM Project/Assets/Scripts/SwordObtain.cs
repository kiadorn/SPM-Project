using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordObtain : MonoBehaviour {

	private GameObject obtain;
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Player"))
        {
			obtain.GetComponent<ObjectObtain> ().PickUpSword ();
            col.gameObject.GetComponent<PlayerStats>().ObtainSword();
            this.gameObject.SetActive(false);
        }
    }

	void Start(){
		obtain = GameObject.Find("ObjectObtain");
	}
}
