﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour {

    public float Distance;
    public float WakeRange;
    public float ShootInterval;
    public float BulletSpeed;
    public float BulletTimer;

    public bool awake = false;

    public GameObject Bullet;
    private Transform Target;
    private Transform ShootingArea;

    public float speed = 5.0f;

    public Vector3 hiddenPos;
    private GameObject AggroPos;
    private GameObject PassivePos;
    private GameObject Muzzle;
    private GameObject ShootSpot;
   // Vector3 AgPos;

    public float moveSpeed = 0.2f;
    public bool CanShoot;

	//audio
	[HideInInspector] 
	public AudioSource[] source;
	[Header ("Audio Clips")]
	public AudioClip Alerted;
	public AudioClip Retract;
	public AudioClip [] Fire;
	[ReadOnlyAttribute] public AudioClip FireJustPlayed;
	public AudioClip[] Hurt;
	[ReadOnlyAttribute] public AudioClip HurtJustPlayed;
	public AudioClip Death; //inte använd än

    void Awake() {
        AggroPos = transform.GetChild(1).gameObject;
        PassivePos = transform.GetChild(2).gameObject;
        ShootSpot = transform.GetChild(0).GetChild(0).gameObject;
        //Muzzle = transform.GetChild(0).gameObject;
        ShootingArea = transform.parent.GetChild(1).GetComponent<Transform>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
		//audio
		source = GetComponents<AudioSource>();
    }

    void Update () {
        hiddenPos = PassivePos.transform.position;
        //AgPos = AggroPos.transform.position;
        RangeCheck();

        CanShoot = true;
	}

    void RangeCheck() {
        Distance = Vector3.Distance(transform.position, Target.transform.position);
        if(awake) {
            Vector3 vectorToTarget = Target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
            //Muzzle.transform.position = Vector3.MoveTowards(Muzzle.transform.position, AggroPos.transform.position, (moveSpeed * Time.deltaTime));
        }
        if (!awake) {
            //Muzzle.transform.position = Vector3.MoveTowards(Muzzle.transform.position, PassivePos.transform.position, (moveSpeed * Time.deltaTime));
            BulletTimer = ShootInterval;
        }
    }

    public void Shoot() {
        BulletTimer += Time.deltaTime;
        if (BulletTimer >= ShootInterval) {
            Vector2 dir = Target.transform.position - transform.position;
            dir.Normalize();

            GameObject bulletClone;
            bulletClone = Instantiate(Bullet, ShootSpot.transform.position, ShootingArea.transform.rotation);
            bulletClone.GetComponent<Rigidbody2D>().velocity = dir * BulletSpeed;

			//audio
			int length = Fire.Length;
			int replace = Random.Range (0, (length - 1));
			source [1].clip = Fire[replace];
			source [1].volume = 1f;
			source [1].Play ();
			FireJustPlayed = Fire [replace];
			Fire [replace] = Fire [length - 1];
			Fire [length - 1] = FireJustPlayed;

            BulletTimer = 0;
        }

    }
}
