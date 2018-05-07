using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusByHit : MonoBehaviour {

    public GameObject Focus;
    public GameObject DisableObject;
    public float TimeToDisable;
    public float TimeCameraStay = 3f;
    public bool FreezePlayer;
    public bool DontDisableObject;

	//Audio
	private AudioSource source;
	[Header("Audio Clips")]
	public AudioClip Open;
    private CameraFollow Cam;

    private void Awake() {
        Cam = GameObject.Find("Camera").GetComponent<CameraFollow>();
		source = GetComponent<AudioSource> ();
    }

    public void Action() {
        Cam.switchToCameraFocus(Focus.transform.position, FreezePlayer);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        if (!DontDisableObject)
        {
            StartCoroutine(WaitForDisable());
        }
    }

    public IEnumerator WaitForDisable() {

        yield return new WaitForSeconds(TimeToDisable);
		source.clip = Open;
		source.Play ();
        DisableObject.SetActive(false);
        yield return 0;
    }
}