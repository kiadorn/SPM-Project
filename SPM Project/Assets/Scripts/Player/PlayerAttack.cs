using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	public float attackCooldown;
	private BoxCollider2D attackArc;
	private float attackTimeStamp;
	private float xDir;
	public GameObject spriteObject; //Testrad ta bort när spelaren har animation

	private Transform spriteTransform; //Testrad ta bort när spelaren har animation
	private AudioSource source;
	[Header("AudioClips")]
	public AudioClip Swing;

	void Start () {
		attackTimeStamp = attackCooldown;
		attackArc = this.GetComponent<BoxCollider2D>();
		source = GetComponent<AudioSource>();
	}

	void Update () {

		UpdateColliderPosition ();

		if (attackTimeStamp <= attackCooldown) {
			attackTimeStamp += Time.deltaTime;
		}
		if (Input.GetButtonDown ("Fire1") && attackTimeStamp >= attackCooldown) {
			attackArc.enabled = true;
			this.GetComponent<SpriteRenderer> ().enabled = true;
			attackTimeStamp = 0;
			source.clip = Swing;
			source.Play ();
		} else {
			this.GetComponent<SpriteRenderer> ().enabled = false;
		}
		
	}

	//När spelkaraktären har en sprite och storlek måste 1.5f ändras till något mer passande värde. Nuvarande värde är endast temporärt för testning.
	private void UpdateColliderPosition(){
		if(attackArc.enabled == true){
		attackArc.enabled = false;
		}
		xDir = Input.GetAxisRaw ("Horizontal");
		if(xDir > 0){
			attackArc.transform.localPosition = new Vector2 (1.5f, 0f);
		}else if(xDir < 0){
			attackArc.transform.localPosition = new Vector2 (-1.5f, 0f);
		}else if(xDir == 0){
			return;
		}
	}

	//Kallar på metoder för varje object i attackArc collidern när den aktiveras.
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Enemy"){
			other.SendMessage ("TakeDamage", null, SendMessageOptions.DontRequireReceiver);
		}if (other.tag == "Interactable") {
			other.SendMessage ("Action", null, SendMessageOptions.DontRequireReceiver);
		}
			
	}
}
