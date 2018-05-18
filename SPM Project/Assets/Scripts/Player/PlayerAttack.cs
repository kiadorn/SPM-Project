using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	public float attackCooldown;
	public bool attackUnlocked;

	private PlayerController _controller;
	private float attackTimeStamp;
	private float xDir;
	private Vector2 attackSize;
	private BoxCollider2D playerCollider;
	private PlayerController playerControllerScript;
	private Vector2 attackDirection;
	private AudioSource source;

	[Header("AudioClips")]
	public AudioClip Swing;

	void Start () {
		_controller = GetComponentInParent<PlayerController>();
		playerControllerScript = GetComponent<PlayerController>();
		playerCollider = GetComponent<BoxCollider2D>();
		attackSize = new Vector2 (playerCollider.size.x, playerCollider.size.y);
		attackTimeStamp = attackCooldown;
		source = GetComponent<AudioSource>();
	}

	void Update (){
		if ((_controller.CurrentState is WallState)) {
			xDir = playerControllerScript.GetLastXDirection () * -1;
		} else {
			xDir = playerControllerScript.GetLastXDirection ();
		}

		if (attackTimeStamp <= attackCooldown){
			attackTimeStamp += Time.deltaTime;
		}

		if (attackUnlocked) {
			if(Input.GetButtonDown ("Fire1") && attackTimeStamp >= attackCooldown && ((_controller.CurrentState is WallState) || (_controller.CurrentState is AirState) || (_controller.CurrentState is GroundState))){
				Attack ();
				attackTimeStamp = 0f;
			}
		}
	}

	private void Attack(){
		source.PlayOneShot (Swing);
		attackDirection = new Vector2 (xDir, 0);
		RaycastHit2D[] hits = Physics2D.BoxCastAll ((Vector2)transform.position, attackSize, 0.0f, attackDirection, 2f);
		foreach (RaycastHit2D hit in hits) {
			if (hit.transform.tag == "Enemy") {
				hit.transform.SendMessage ("TakeDamage", null, SendMessageOptions.DontRequireReceiver);
			} else if (hit.transform.tag == "Interactable") {
				hit.transform.SendMessage ("Action", null, SendMessageOptions.DontRequireReceiver);
			}
		}	
	}
}
