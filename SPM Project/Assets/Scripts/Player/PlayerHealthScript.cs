using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour {
	public int playerHealth;
	public float invulnTime;
	private float invulnTimer;

	private void Start(){
		
	}

	void Update(){
			invulnTimer += Time.deltaTime;
	}

	//Spelarhälsa, kanske vill koppla något grafiskt till invulntime?
	public void RemoveHealth(int d){
		if(invulnTimer >= invulnTime){
		playerHealth = playerHealth - d;
		if(playerHealth <= 0){
			PlayerDeath ();
			}
		}
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Health")){
            if (playerHealth != 2)
            {
                playerHealth = playerHealth + 1;
                Debug.Log("Helade spelaren");
            }
        }
	}
	public void PlayerDeath(){
		//Vad händer när spelaren dör?
		//Spawna vid checkpoint.
	}
}
