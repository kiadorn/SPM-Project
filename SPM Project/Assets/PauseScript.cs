using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {
	public GameObject quitConfirmPane;
	private bool paused;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		paused = false;
		quitConfirmPane.SetActive (false);
		this.gameObject.SetActive(false);
	}

	public void PauseUnpauseGame(){
		if (paused) {
			ResumeTime ();
		} else {
			PauseTime ();
		}
	}

	public void PauseTime(){
		Time.timeScale = 0;
		paused = true;
		Cursor.visible = true;
		this.gameObject.SetActive(true);
	}

	public void ResumeTime(){
		Time.timeScale = 1;
		paused = false;
		Cursor.visible = false;
		this.gameObject.SetActive(false);
	}

	public void QuitApplicationFromPauseScreen(){
		Application.Quit ();
	}
}
