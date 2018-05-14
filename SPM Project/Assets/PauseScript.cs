using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {
	public GameObject quitConfirmPane;
    public GameObject DefaultMenu;

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
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (player.CurrentState is WallState || player.CurrentState is GroundState || player.CurrentState is AirState) {
            player.TransitionTo<AirState>();
        }
        this.gameObject.SetActive(true);
        DefaultMenu.SetActive(true);
        quitConfirmPane.SetActive(false);
    }

	public void ResumeTime(){
		Time.timeScale = 1;
		paused = false;
		Cursor.visible = false;
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (player.CurrentState is WallState || player.CurrentState is GroundState || player.CurrentState is AirState) {
            player.TransitionTo<AirState>();
        }
        quitConfirmPane.SetActive(false);
        this.gameObject.SetActive(false);
	}

	public void QuitApplicationFromPauseScreen(){
        Time.timeScale = 1;
        SceneManager.LoadScene("_MainMenu");
        //Application.Quit ();
    }
}
