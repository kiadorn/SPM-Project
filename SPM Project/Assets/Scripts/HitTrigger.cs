using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTrigger : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
        } else {
            other.gameObject.SendMessage("Action", null, SendMessageOptions.DontRequireReceiver);
        } 
    }
}
