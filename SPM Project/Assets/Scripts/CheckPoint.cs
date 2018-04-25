using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    Vector2 position;
    public bool Latest = true;

    public PlayerStats Stats;

    public GameObject[] ObjectsToReset;

	// Use this for initialization
	void Awake () {
        Stats = GameObject.Find("UI").GetComponent<PlayerStats>();
	}


    public void EnableEnemies() {
        foreach(GameObject resetObject in ObjectsToReset) {
            resetObject.SetActive(false);
            resetObject.SetActive(true);
          //  if (resetObject.GetComponent("TurretController") as TurretController) {
  
                       Debug.Log("Test2");
                   // resetObject.transform.GetComponent<TurretController>().Reset();


         //   }
   //         Debug.Log("Test3");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (Latest) {
                Stats.CurrentCheckPoint = this;
                Latest = false;


            }
                
                

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
