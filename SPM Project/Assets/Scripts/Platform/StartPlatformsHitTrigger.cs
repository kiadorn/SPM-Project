﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatformsHitTrigger : MonoBehaviour {

	private GameObject obtain;

    public MovePlatformAuto[] platformScripts;

    public void Action() {
        foreach(MovePlatformAuto mo in platformScripts) {
            mo.enabled = true;
			obtain.GetComponent<ObjectObtain> ().StartTrigger ();
        }
    }

	void Start(){
		obtain = GameObject.Find ("ObjectObtain");
	}
}
