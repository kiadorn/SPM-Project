using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {
	private bool paused;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		paused = false;
		this.gameObject.SetActive(false);
	}

	public void PauseUnpauseGame(){
		if (paused) {
			paused = false;
			Cursor.visible = false;
			this.gameObject.SetActive(false);
			Time.timeScale = 0;
		} else {
			paused = true;
			Cursor.visible = true;
			this.gameObject.SetActive(true);
			Time.timeScale = 1;
			//tid simulation = 1
		}
	}
}
