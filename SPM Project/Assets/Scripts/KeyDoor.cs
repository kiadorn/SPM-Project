using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour {

	private GameObject obtain;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerStats>().hasKey)
        {
			obtain.GetComponent<ObjectObtain> ().OpenDoor ();
            collision.gameObject.GetComponent<PlayerStats>().ChangeKeyStatus(false);
            this.gameObject.SetActive(false);
        }
    }

	void Start(){
		obtain = GameObject.Find ("ObjectObtain");
	}

}
