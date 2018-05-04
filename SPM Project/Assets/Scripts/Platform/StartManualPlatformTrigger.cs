using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManualPlatformTrigger : MonoBehaviour {

    public MovePlatform PlatformToMove; 

    public void Action() {
        PlatformToMove.shouldIMove = true;
    }
}
