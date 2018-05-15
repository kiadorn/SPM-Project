using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class CameraFocusTrigger : MonoBehaviour {

    public GameObject Focus;
    public bool FreezePlayer;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            //Camera.main.GetComponent<CameraFollow>().switchToCameraFocus(Focus.transform.position, FreezePlayer); //Byter till CameraFocus i gammal Camera
            CameraHelper.switchToCameraFocus(Focus.transform.position, FreezePlayer); //Byter till CameraFocus i Pro Camera 2D
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
