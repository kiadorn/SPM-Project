using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectObtain : MonoBehaviour {


	private AudioSource [] source;
	[Header("Audio Clips")]
	public AudioClip PickUpKeySound;
	public AudioClip PickUpSwordSound;
	public AudioClip OpenDoorSound;
	public AudioClip StartTriggerSound;

	// Use this for initialization
	void Start () {
		source = GetComponents<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PickUpSword(){
		source[0].clip = PickUpSwordSound;
		source[0].Play ();
	}

	public void PickUpKey(){
		source[2].clip = PickUpKeySound;
		source[2].Play ();
	}

	public void OpenDoor(){
		source[1].clip = OpenDoorSound;
		source [1].volume = 0.9f;
		source[1].Play ();
	}

	public void StartTrigger(){
		source[1].clip = StartTriggerSound;
		source [1].volume = 0.75f;
		source[1].Play ();
	}
}
