using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMovingTeleportPlatform : MonoBehaviour {

	private GameObject obtain;

	public MovablePlatformTeleport[] platformScripts;

	public void Action() {
		foreach(MovablePlatformTeleport mo in platformScripts) {
			mo.enabled = true;
			obtain.GetComponent<ObjectObtain> ().StartTrigger ();
		}
	}

	void Start(){
		obtain = GameObject.Find ("ObjectObtain");
	}
}
