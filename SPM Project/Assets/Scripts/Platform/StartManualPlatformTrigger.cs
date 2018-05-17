using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManualPlatformTrigger : MonoBehaviour {

	private GameObject obtain;

    public MovePlatform PlatformToMove; 

    public void Action() {
        PlatformToMove.shouldIMove = true;
		obtain.GetComponent<ObjectObtain> ().StartTrigger ();
        CameraShake.AddIntensity(0.75f);
    }

	void Start(){
		obtain = GameObject.Find ("ObjectObtain");
	}
}
