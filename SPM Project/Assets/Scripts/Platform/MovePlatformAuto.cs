using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformAuto : MonoBehaviour {

    public float moveForwardSpeed;
    public float moveBackSpeed;
    [Range(0, 5)]
    public float waitTime;

    [HideInInspector]
    public bool shouldIMove = false;
    private Vector3 originalPos;
    private bool goingUp = false;
    private float timer;

	[HideInInspector]
	public AudioSource source;
	[Header("Audio")]
	public AudioClip moving;

	[ReadOnly]
	public bool playArea = false;

	public float fadeTime = 1f;
	[ReadOnly] public bool fading = false;

    private void Start()
    {
        originalPos = transform.position;
		source = GetComponent<AudioSource> ();
		source.clip = moving;
		source.loop = true;
		timer = 0;
    }


    private void Update()
    {
		if (fading) {
			FadeAudio ();
		}
        Move();
    }

    public void Move()
    {

        //Om man anländer till målpunkten.
        if (transform.position == transform.parent.GetChild(1).transform.position)
        {
            goingUp = true;
            //Timer
            timer += Time.deltaTime;
			if (playArea) fading = true;
            if (timer < waitTime)
            {
                return;
            }
            timer = 0;
        }
        //Om man anländer till ursprungspunkten.
        else if (transform.position == originalPos)
        {
            goingUp = false;
            //Timer
            timer += Time.deltaTime;
			if (playArea) fading = true;
            if (timer < waitTime)
            {
                return;
            }
            timer = 0;
        }

        //Rörelse
        if (!goingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.parent.GetChild(1).transform.position, moveForwardSpeed * Time.deltaTime);
			if (!source.isPlaying) source.Play ();
			if (playArea) {
				source.volume = 1;
				fading = false;
			}
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPos, moveBackSpeed * Time.deltaTime);
			if (!source.isPlaying) source.Play ();
			if (playArea) {
				source.volume = 1;
				fading = false;
			}
        }
    }

	private void FadeAudio() {
		AudioHelper.FadeOut (source, fadeTime, timer);
		if (source.volume == 0) fading = false;
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
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
		
}
