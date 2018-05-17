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

    private Vector3 OGPos;
    private bool _moveDoor;
    public float MoveSpeed;

    private void Awake() {
        OGPos = transform.parent.localPosition;
    }

    private void OnEnable() {
        transform.parent.localPosition = OGPos;
        _moveDoor = false;
        Aksjuk = false;
        _time = 0;
    }

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

        if (_moveDoor) {
            transform.parent.localPosition = Vector3.MoveTowards(transform.parent.localPosition, transform.parent.parent.GetChild(1).transform.localPosition, MoveSpeed * Time.deltaTime);
            if(transform.parent.localPosition == transform.parent.parent.GetChild(1).transform.localPosition) {
                _moveDoor = false;
            }
        }
    }

    private void Extreme()
    {
        CameraShake.AddIntensity(1);
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().CurrentState is AirState)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().TransitionTo<HurtState>();
        }
    }

	public void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Hazard")) {
			source.clip = trigger [Random.Range (0, trigger.Length)];
			source.Play ();
            //transform.parent.gameObject.GetComponent<MeshRenderer>().enabled = false;
            //transform.parent.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
            _moveDoor = true;
            Aksjuk = true;
            CameraShake.AddIntensity(1);
            foreach (MovePlatformAuto mo in platformScripts) {
				mo.enabled = true;
				obtain.GetComponent<ObjectObtain> ().StartTrigger ();
			}

        }
    }
}
