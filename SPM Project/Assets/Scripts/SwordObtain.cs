using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordObtain : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerStats>().ObtainSword();
            this.gameObject.SetActive(false);
        }
    }
}
