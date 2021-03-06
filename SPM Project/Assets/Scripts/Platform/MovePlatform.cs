﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour {

    public bool moveBack;
    public float moveForwardSpeed;
    public float moveBackSpeed;
    [Range(0, 5)]
    public float waitTime;

    [HideInInspector]
    public bool shouldIMove = false;
    private Vector3 originalPos;
    private bool isDone = false;
    private bool isWaiting;
    private bool SaveMove;

	[HideInInspector]
	public AudioSource source;
	[Header("Audio")]
	public AudioClip moving;

	[ReadOnly]
	public bool playArea = false;

	public float fadeTime = 1f;
	[ReadOnly] public bool fading = false;
    private float timer;

    bool firstTime = true;

    private void Awake()
    {
        originalPos = transform.localPosition;
        SaveMove = moveBack;
        firstTime = false;
		source = GetComponent<AudioSource> ();
		source.clip = moving;
		source.loop = true;
        timer = 0;
    }

    private void Update()
    {
        if (isWaiting && fading) {
            timer += Time.deltaTime;
        }
        if (fading)
        {
            FadeAudio();
        }

        if (shouldIMove)
        {
            Move();
        }
    }

    public void Move()
    {
        
        if (transform.localPosition == transform.parent.GetChild(1).transform.localPosition)
        {
            isDone = true;
            fading = true;
            timer += Time.deltaTime;
            source.Stop ();
            if (!isWaiting && moveBack)
                StartCoroutine(WaitToMoveBack(waitTime));
        }
        if (!isDone)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.parent.GetChild(1).transform.localPosition, moveForwardSpeed * Time.deltaTime);
			if (!source.isPlaying) {
				source.Play ();
				fading = false;
			}
        } else if (moveBack)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, originalPos, moveBackSpeed * Time.deltaTime);
			if (!source.isPlaying) {
				source.Play ();
				fading = false;
			}
		}

        if (transform.localPosition == originalPos)
        {
            shouldIMove = false;
            isDone = false;
            fading = true;
            timer += Time.deltaTime;
            source.Stop ();
        }
    }



    private IEnumerator WaitToMoveBack(float time)
    {
        isWaiting = true;
        moveBack = false;
        yield return new WaitForSeconds(time);
        moveBack = true;
        yield return new WaitForSeconds(time);
        isWaiting = false;
    }

    private void FadeAudio()
    {
        AudioHelper.FadeOut(source, fadeTime, timer);
        if (source.volume == 0) {
            fading = false;
            timer = 0;
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
    public void Reset() {
        isDone = false;
        shouldIMove = false;
        isWaiting = false;
        if (!firstTime) {
            moveBack = SaveMove;
            transform.localPosition = originalPos;
        }


    }

}
