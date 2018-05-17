using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusByHit : MonoBehaviour {

    public GameObject Focus;
    public GameObject DisableObject;
    public GameObject TargetPosition;
    public float TimeToDisable;
    public float TimeCameraStay = 3f;
    public bool FreezePlayer;
    public bool DontDisableObject;
    public float Speed;
	//Audio
	private AudioSource source;
	[Header("Audio Clips")]
	public AudioClip Open;
    private bool StartMovingObject;

    private void Awake() {
		source = GetComponent<AudioSource> ();
    }

    private void Update() {
        if (StartMovingObject) {
            DisableObject.transform.position = Vector3.MoveTowards(DisableObject.transform.position, TargetPosition.transform.position, Speed * Time.deltaTime);
            if(DisableObject.transform.position == TargetPosition.transform.position) {
                StartMovingObject = false;
            }
        }
    }

	private void OnEnable(){
		gameObject.GetComponent<BoxCollider2D> ().enabled = true;
        StartMovingObject = false;
    }

    public void Action() {
        CameraHelper.switchToCameraFocus(Focus.transform.position, FreezePlayer);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        if (!DontDisableObject)
        {
            CameraShake.AddIntensity(1);
            StartCoroutine(WaitForDisable());
        }
    }

    public IEnumerator WaitForDisable() {

        yield return new WaitForSeconds(TimeToDisable);
		source.clip = Open;
		source.Play ();
        StartMovingObject = true;
        yield return 0;
    }
}