using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour {

    public GameObject Focus;
    public bool FreezePlayer;
    private CameraFollow Cam;

    private void Awake() {
        Cam = GameObject.Find("Camera").GetComponent<CameraFollow>();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            StartCoroutine(Cam.ChangeTarget(Focus, 3f, FreezePlayer));
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
