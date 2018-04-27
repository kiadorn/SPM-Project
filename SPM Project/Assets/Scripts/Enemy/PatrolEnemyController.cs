﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyController : Controller
{

    public PlayerStats playerStats;
    [Header("Patrol State")]
    public Transform groundDetection;
    public bool startMovingRight;
    public float groundCheckDistance;
    public float speed;
    [SerializeField]
    private float currentSpeed;

	//audio
	[HideInInspector] 
	public AudioSource source;
	[Header ("Audio Clips")]
	public AudioClip Skitter;
	public AudioClip Alerted;
	public AudioClip Death; //inte använd än
    private Vector3 OGPos;

    void Awake()
    {
        base.Awake();
        OGPos = transform.position;
    }

	private void Start (){
		source = GetComponent<AudioSource> ();
	}
    private void Update()
    {
        CurrentState.Update();
    }

    private void OnValidate()
    {
        transform.eulerAngles = (startMovingRight == true) ? new Vector3(0, 0, 0) : new Vector3(0, -180, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerStats.ChangeHealth(-1);
            player.gameObject.GetComponent<PlayerController>().TransitionTo<HurtState>();
        }
    }

    private void OnEnable() {
        transform.position = OGPos;
    }
}
