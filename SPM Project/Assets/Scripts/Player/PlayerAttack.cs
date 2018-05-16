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
	private Color originalColor; //Testrad ta bort när spelaren har animation

	private AudioSource source;
	[Header("AudioClips")]
	public AudioClip [] Swing;
	[ReadOnlyAttribute] public AudioClip SwingJustPlayed;

	void Start () {
		attackTimeStamp = attackCooldown;
		attackArc = this.GetComponent<BoxCollider2D>();
		originalColor = spriteObject.GetComponent<SpriteRenderer> ().color; //Testrad ta bort när spelaren har animation
		source = GetComponent<AudioSource>();
	}

	void Update () {

		UpdateColliderPosition ();

		if (attackTimeStamp <= attackCooldown) {
			attackTimeStamp += Time.deltaTime;
			spriteObject.GetComponent<SpriteRenderer> ().color = Color.red;  //Testrad ta bort när spelaren har animation
		} else {
			spriteObject.GetComponent<SpriteRenderer> ().color = originalColor; //Testrad ta bort när spelaren har animation
		}
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
