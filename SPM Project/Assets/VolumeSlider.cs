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

    void Awake() {
        mixer.SetFloat("MasterVolume", GetMasterLevel());
    }
    public float GetMasterLevel() {
        float value;
        bool result = mixer.GetFloat("MasterVolume", out value);
        if (result) {
            return value;
        }
        else {
            return 0f;
        }
    }

    private void Update(){
		mixer.GetFloat ("MasterVolume", out currentVolume);
		this.GetComponent<Slider> ().value = currentVolume;
	}
}
