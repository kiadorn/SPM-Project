using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour {
    public string KeyToDoor;
	// Use this for initialization
	void Start () {
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("COLLIDING WITH PLAYER");
            this.gameObject.SetActive(false);
            GameManager.instance.hasKey = true ;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
