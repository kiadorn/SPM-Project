using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovingAudioArea : MonoBehaviour {

	public MovePlatformAuto[] platforms;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.CompareTag ("Player")) {
			foreach (MovePlatformAuto mo in platforms) {
				mo.playArea = true;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.CompareTag ("Player")) {
			foreach (MovePlatformAuto mo in platforms) {
				mo.playArea = false;
				mo.fading = true;
			}
		}
	}
}
