using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBySpike : MonoBehaviour {

	private AudioSource source;
	[Header("Audio")]
	public AudioClip [] trigger;
    private GameObject obtain;
    public MovePlatformAuto[] platformScripts;
    [ReadOnly] public static bool Aksjuk = false;
    private float _time;
    public float Cooldown;

    public void Start()
    {
        source = GetComponent<AudioSource>();
        obtain = GameObject.Find("ObjectObtain");
    }

    public void Update() {
        if (Aksjuk) {
            _time += Time.deltaTime;
            if(_time >= Cooldown) {
                Extreme();
                _time = 0;
            }
        }
    }

    private void Extreme()
    {
        CameraShake.AddIntensity(20);
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().CurrentState is AirState)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().TransitionTo<HurtState>();
        }
    }

	public void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Hazard")) {
			source.clip = trigger [Random.Range (0, trigger.Length)];
			source.Play ();
			transform.parent.gameObject.GetComponent<MeshRenderer>().enabled = false;
			transform.parent.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
            Aksjuk = true;
            foreach (MovePlatformAuto mo in platformScripts) {
				mo.enabled = true;
				obtain.GetComponent<ObjectObtain> ().StartTrigger ();
			}

        }
    }
}
