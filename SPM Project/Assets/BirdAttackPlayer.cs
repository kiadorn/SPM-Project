using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAttackPlayer : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.parent.GetChild(0).GetComponent<FlyingEnemy>().BirdAttackPlayer(collision.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.gameObject.CompareTag("Player"))
        {
            transform.parent.GetChild(0).GetComponent<FlyingEnemy>()._canAttack = false;
            transform.parent.GetChild(0).GetComponent<FlyingEnemy>()._attacking = false;
        }
    }
}
