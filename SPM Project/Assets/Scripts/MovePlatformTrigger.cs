using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformTrigger : MonoBehaviour {


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            GetComponentInParent<MovePlatform>().shouldIMove = true;
            Debug.Log("We in bois");
        }
        
    }

}
