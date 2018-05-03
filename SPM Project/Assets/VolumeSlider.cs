using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {
	public AudioMixer mixer;

	public void SetMasterVolume(Slider slider){
		mixer.SetFloat ("MasterVolume", slider.value);
	}
}
