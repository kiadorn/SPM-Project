using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {

    [Range(0, 2)]
    public int damageValue;
    public PlayerController _controller;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player")) {
    //        _controller.TransitionTo<HurtState>();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _controller.TransitionTo<HurtState>();
        }
    }

}
