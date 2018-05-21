using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SnapShotControl : MonoBehaviour {
    public AudioMixerSnapshot Snapshot;
	public void Start () {
        Snapshot.TransitionTo(0f);
	}

}
