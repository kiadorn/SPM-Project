using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
    
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.instance.HealthPoints <= 2 )
        {
            this.gameObject.SetActive(false);
            GameManager.instance.ChangeHealth(1);
        }
    }
}
