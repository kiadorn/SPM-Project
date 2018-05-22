using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour {
	private string currentScene;
	private AudioSource [] source;
	[Header("BGM Clips")]
	public AudioClip [] BackgroundMusic;

	// Use this for initialization
	public void Start () {
		currentScene = SceneManager.GetActiveScene ().name;
		source = GetComponents<AudioSource> ();
		checkCurrentLevel ();
	}
	
	// Update is called once per frame
	public void Update () {
		
	}

	public void checkCurrentLevel(){
		if (currentScene == "Level1Intro") {
			source[0].clip = BackgroundMusic [0];
			source[0].Play ();
		}
		if (currentScene == "NewLevel1"){
			source[1].clip = BackgroundMusic [1];
			source[1].Play ();
		}
		if (currentScene == "NewLevel2"){
			source[2].clip = BackgroundMusic [2];
			source[2].Play ();
		}
		if (currentScene == "BossLevel"){
			source[3].clip = BackgroundMusic [3];
			source[3].Play ();
		}
	}
}
