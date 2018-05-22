using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemyAttackTrigger : MonoBehaviour {

    private BirdEnemyBehaviour behaviour;
	private bool played;

    private void Awake()
    {
        behaviour = transform.parent.GetChild(0).GetComponent<BirdEnemyBehaviour>();
		played = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Audio
			if (!played && !behaviour.source[0].isPlaying && behaviour._canAttack){
				played = true;
				behaviour.source[0].clip = behaviour.Alerted;
				behaviour.source [0].volume = 0.65f;
				behaviour.source[0].Play ();
			}
            behaviour.BirdAttackPlayer(collision.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.gameObject.CompareTag("Player"))
        {
			played = false;
            behaviour._canAttack = false;
            behaviour._attacking = false;
			behaviour.source [0].volume = 1f;
        }
    }
}
