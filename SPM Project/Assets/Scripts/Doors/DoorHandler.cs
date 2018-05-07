using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("COLLIDING WITH PLAYER");
            gameObject.SetActive(false);
          
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
