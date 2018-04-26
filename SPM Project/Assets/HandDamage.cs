using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDamage : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("SMASH PLAYER");
            collision.gameObject.GetComponent<PlayerController>().TransitionTo<HurtState>();
        }
    }

}
