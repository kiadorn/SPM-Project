using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemyAttackTrigger : MonoBehaviour {

    private BirdEnemyBehaviour behaviour;

    private void Awake()
    {
        behaviour = transform.parent.GetChild(0).GetComponent<BirdEnemyBehaviour>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Audio
            behaviour.source.clip = behaviour.Alerted;
            behaviour.source.Play ();

            behaviour.BirdAttackPlayer(collision.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.gameObject.CompareTag("Player"))
        {
            behaviour._canAttack = false;
            behaviour._attacking = false;
        }
    }
}
