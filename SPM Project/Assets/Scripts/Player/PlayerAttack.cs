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
	public AudioClip [] Swing;
	[ReadOnlyAttribute] public AudioClip SwingJustPlayed;

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
<<<<<<< HEAD

		if (attackTimeStamp <= attackCooldown){
			attackTimeStamp += Time.deltaTime;
=======
		this.GetComponent<SpriteRenderer> ().enabled = false;
        if ((Input.GetButtonDown("Fire1") || Input.GetAxis("Fire1") != 0) && attackTimeStamp >= attackCooldown) {
			attackArc.enabled = true;
			this.GetComponent<SpriteRenderer> ().enabled = true;
			attackTimeStamp = 0;
			int length = Swing.Length;
			int replace = Random.Range (0, (length - 1));
			source.clip = Swing[replace];
			source.Play ();
			SwingJustPlayed = Swing [replace];
			Swing [replace] = Swing [length - 1];
			Swing [length - 1] = SwingJustPlayed;
			spriteObject.GetComponent<SpriteRenderer> ().color = originalColor; //Testrad ta bort när spelaren har animation
		}
		
	}
	//När spelkaraktären har en sprite och storlek måste 1.5f ändras till något mer passande värde. Nuvarande värde är endast temporärt för testning.
	private void UpdateColliderPosition(){
		if(attackArc.enabled == true){
		attackArc.enabled = false;
>>>>>>> origin/Steven7
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
