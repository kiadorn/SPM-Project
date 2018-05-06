using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectObtain : MonoBehaviour {


	private AudioSource source;
	[Header("Audio Clips")]
	public AudioClip PickUpKeySound;
	public AudioClip PickUpSwordSound;
	public AudioClip OpenDoorSound;
	public AudioClip StartTriggerSound;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PickUpSword(){
		source.clip = PickUpSwordSound;
		source.Play ();
	}

	public void PickUpKey(){
		source.clip = PickUpKeySound;
		source.Play ();
	}

	public void OpenDoor(){
		source.clip = OpenDoorSound;
		source.Play ();
	}

	public void StartTrigger(){
		source.clip = StartTriggerSound;
		source.Play ();
	}
}
