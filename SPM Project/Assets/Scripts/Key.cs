using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player") ) {
            this.gameObject.SetActive(false);
            if (GameManager.instance.Level1Done == false)
            {
                GameManager.instance.Level1Done = true;
            }
            if (GameManager.instance.Level1Done == true && GameManager.instance.Level2Done == false)
            {
                GameManager.instance.Level2Done = true;
            }
        }
    }


}
