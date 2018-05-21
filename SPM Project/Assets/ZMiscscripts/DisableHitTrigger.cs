using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableHitTrigger : MonoBehaviour {

    public GameObject DisableObject;
    public Transform doorMovePos;

    //Audio
    private AudioSource source;
    [Header("Audio Clips")]
    public AudioClip Open;

    private Vector3 _OGpos;
    private bool moving = false;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        _OGpos = DisableObject.transform.position;
    }

    private void OnEnable()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        DisableObject.transform.position = _OGpos;
        moving = false;
    }

    public void Action()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //Spela animation för att ta bort
        //DisableObject.SetActive(false);
        source.clip = Open;
        source.Play();
        moving = true;
    }

    public void Update()
    {
        if (moving)
        {
            DisableObject.transform.position = Vector3.MoveTowards(DisableObject.transform.position, doorMovePos.position, 3f * Time.deltaTime);
            if (DisableObject.transform.position == doorMovePos.position) moving = false;
        }
    }


}
