using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchController : MonoBehaviour {
	

	public void Pichter(AudioSource source){
		source.volume = Random.Range (0.9f, 1f);
		source.pitch = Random.Range (0.95f, 1.15f);
	}
}
