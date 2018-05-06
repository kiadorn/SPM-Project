using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {
	public AudioMixer mixer;
	private float currentVolume;

	public void SetMasterVolume(Slider slider){
		mixer.SetFloat ("MasterVolume", slider.value);
	}

	private void Update(){
		mixer.GetFloat ("MasterVolume", out currentVolume);
		this.GetComponent<Slider> ().value = currentVolume;
	}
}
