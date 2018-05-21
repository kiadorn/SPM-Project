using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHazardTrigger : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            transform.parent.GetComponentInChildren<MovePlatform>().shouldIMove = true;
        }

    }
}
