using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Följande script måste hängas på på en child till spelaren som innehåller en BoxCollider2D.
public class PlayerAttack : MonoBehaviour {
	public float attackCooldown;
	private BoxCollider2D attackArc;
	//private PlayerController _controller;
	private List<GameObject> objectsInRange;
	private float attackTimeStamp;
	private float xDir;

	void Start () { /*Behövs Start när man har Initialize???*/
		attackTimeStamp = attackCooldown;
		objectsInRange = new List<GameObject>();
		attackArc = this.GetComponent<BoxCollider2D>();
	}

	public void Initialize(Controller owner)
	{
		//_controller = (PlayerController)owner;
	}

	void Update () {

		UpdateColliderPosition ();

		if (attackTimeStamp <= attackCooldown) {
			attackTimeStamp += Time.deltaTime;
		}

		if(Input.GetButtonDown("Fire1") && attackTimeStamp >= attackCooldown){
			attackArc.enabled = true;
			attackTimeStamp = 0;
			PlayerAtk();
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

	private void PlayerAtk(){
		/*Här behövs en ForEach stats som går igenom objectsInRange listan och applicerar skada på varje object som finns i listan. (Kan ej göras för tillfället då jag ej vet hur eller var hälsan på fiender kommer att se ut) /Joakim */
	}
	//Lägger till fiender som är i collidern attackArc.
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Enemy"){
			other.SendMessage ("TakeDamage", null, SendMessageOptions.DontRequireReceiver);
		}
	}
}
