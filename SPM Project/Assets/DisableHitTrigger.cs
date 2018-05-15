using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableHitTrigger : MonoBehaviour {

    public GameObject DisableObject;

    //Audio
    private AudioSource source;
    [Header("Audio Clips")]
    public AudioClip Open;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Action()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //Spela animation för att ta bort
        DisableObject.SetActive(false);
        source.clip = Open;
        source.Play();
    }
}
