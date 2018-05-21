using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerClimbableBySpike : MonoBehaviour {

	private AudioSource source;
	[Header("Audio")]
	public AudioClip [] trigger;
    public GameObject[] unclimbables;
    public Material OGMaterial;
    public Material ChangeMaterial;

	public void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Hazard")) {
			source.clip = trigger [Random.Range (0, trigger.Length)];
			source.Play ();
            GameObject.Find("ChangeToClimb").tag = "Untagged";
            foreach (GameObject go in unclimbables)
            {
                go.SetActive(false);
                //GameObject.Find("ChangeToClimb").GetComponent<Renderer>().material = ChangeMaterial;
            }
            CameraShake.AddIntensity(1);
        }
	}

    void OnEnable() {
        GameObject.Find("ChangeToClimb").tag = "Unclimbable Wall";
        foreach (GameObject go in unclimbables)
        {
            go.SetActive(true);
        }
            //GameObject.Find("ChangeToClimb").GetComponent<Renderer>().material = OGMaterial;
        }

	public void Start(){
		source = GetComponent<AudioSource> ();
	}
}

