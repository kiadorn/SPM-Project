using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordObtain : MonoBehaviour {

	private GameObject Obtain;
	private GameObject CameraHit;
	private GameObject DoorTrigger;
	private GameObject ExitTrigger;
	private GameObject ExitTriggerSprite;
    [ReadOnly] public bool moving;
    public Transform door;
    public Transform doorMovePos;

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Player"))
        {
			Obtain.GetComponent<ObjectObtain> ().PickUpSword ();
            col.gameObject.GetComponent<PlayerStats>().ObtainSword();
			CameraHit.GetComponent<CameraFocusByHit> ().enabled = true;
            //DoorTrigger.GetComponent<BoxCollider2D> ().enabled = true;
            //DoorTrigger.GetComponent<MeshRenderer> ().enabled = true;
            moving = true;
			ExitTrigger.GetComponent<BoxCollider2D> ().enabled = true;
			ExitTriggerSprite.GetComponent<MeshRenderer> ().enabled = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    private void Update()
    {
        if (moving) {
            DoorTrigger.transform.position = Vector3.MoveTowards(door.position, doorMovePos.position, 3f * Time.deltaTime);
            if (door.position == doorMovePos.position) moving = false;
        }
    }

    void Start(){
		Obtain = GameObject.Find("ObjectObtain");
		CameraHit = GameObject.Find ("CameraHitTriggerAndDisableSWORD");
		DoorTrigger = GameObject.Find ("SwordDoor");
		ExitTrigger = GameObject.Find ("CameraHitTriggerAndDisableDoor");
		ExitTriggerSprite = GameObject.Find ("ExitTriggerSprite");
	}
}
