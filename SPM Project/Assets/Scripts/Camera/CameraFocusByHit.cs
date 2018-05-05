﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusByHit : MonoBehaviour {

    public GameObject Focus;
    public GameObject DisableObject;
    public float TimeToDisable;
    public float TimeCameraStay = 3f;
    public bool FreezePlayer;
    private CameraFollow Cam;

    private void Awake() {
        Cam = GameObject.Find("Camera").GetComponent<CameraFollow>();
    }

    public void Action() {
        Cam.switchToCameraFocus(Focus.transform.position, FreezePlayer);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(WaitForDisable());
    }

    public IEnumerator WaitForDisable() {

        yield return new WaitForSeconds(TimeToDisable);
        DisableObject.SetActive(false);
        yield return 0;
    }
}