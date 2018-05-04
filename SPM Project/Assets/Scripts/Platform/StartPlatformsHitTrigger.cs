using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatformsHitTrigger : MonoBehaviour {

    public MovePlatformAuto[] platformScripts;

    public void Action() {
        foreach(MovePlatformAuto mo in platformScripts) {
            mo.enabled = true;
        }
    }
}
