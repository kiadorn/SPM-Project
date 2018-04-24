using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    Vector2 position;
    public bool Latest = true;

    public PlayerStats Stats;

	// Use this for initialization
	void Start () {
		
	}
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (Latest) {
                //Stats.Current = this;
                Latest = false;
            }
                
                

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
