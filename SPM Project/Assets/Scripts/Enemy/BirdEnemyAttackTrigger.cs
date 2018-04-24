using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemyAttackTrigger : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.parent.GetChild(0).GetComponent<BirdEnemyBehaviour>().BirdAttackPlayer(collision.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.gameObject.CompareTag("Player"))
        {
            transform.parent.GetChild(0).GetComponent<BirdEnemyBehaviour>()._canAttack = false;
            transform.parent.GetChild(0).GetComponent<BirdEnemyBehaviour>()._attacking = false;
            //transform.parent.GetChild(0).GetComponent<BirdEnemyBehaviour>().towardsPlayer = false;
        }
    }
}
