using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusByHit : MonoBehaviour {

    public GameObject Focus;
    public GameObject DisableObject;
    public float TimeToDisable;
    public bool FreezePlayer;
    private CameraFollow Cam;

    private void Awake() {
        Cam = GameObject.Find("Camera").GetComponent<CameraFollow>();
    }

    public void Action() {
        StartCoroutine(Cam.ChangeTarget(Focus, 3f, FreezePlayer));
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(WaitForDisable());
    }

    public IEnumerator WaitForDisable() {

        yield return new WaitForSeconds(TimeToDisable);
        DisableObject.SetActive(false);
        yield return 0;
    }
}