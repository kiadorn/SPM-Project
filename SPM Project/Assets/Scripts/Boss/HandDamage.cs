﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDamage : MonoBehaviour {

    public HandSmash HealthInfo;
    public PlayerStats stats;

	private GameObject BossSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
			BossSound.GetComponent<BossHitSound> ().DealDamage ();
            collision.gameObject.GetComponent<PlayerController>().TransitionTo<HurtState>();

            stats.ChangeHealth(-1);
        }
    }

    private void Awake() {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
		BossSound = GameObject.Find("BossGetHit");
    }

    public void TakeDamage() {
        if(HealthInfo.CurrentHealth > 0) {
            StartCoroutine(swappy());
            HealthInfo.CurrentHealth -= 1;
			BossSound.GetComponent<BossHitSound> ().TakeDamage ();

        }
    }

    private IEnumerator swappy()
    {
        HealthInfo.hand.GetComponent<SpriteRenderer>().color = Color.red;
        HealthInfo.CurrentHealth -= 1;
        yield return new WaitForSeconds(0.1f);
        HealthInfo.hand.GetComponent<SpriteRenderer>().color = Color.white;
        yield return 0;

    }
}
