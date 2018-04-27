using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {

    [Range(0, 2)]
    public int damageValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
			collision.gameObject.GetComponent<PlayerStats>().ChangeHealth(-1 * damageValue);
        }
    }

}
