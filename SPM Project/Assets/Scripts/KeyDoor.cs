using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerStats>().hasKey)
        {
            collision.gameObject.GetComponent<PlayerStats>().ChangeKeyStatus(false);
            this.gameObject.SetActive(false);
        }
    }

}
