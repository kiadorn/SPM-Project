using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.CompareTag("Player")) {
            col.gameObject.GetComponent<PlayerStats>().ChangeKeyStatus(true);
            this.gameObject.SetActive(false);
        }
    }


}
