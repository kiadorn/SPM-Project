using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour {
	public int playerHealth;
	public float invulnTime;
	private float invulnTimer;

	private void Start(){
		playerHealth = 2;
		Debug.Log ("Spelarhälsa satt till 2 i kod.");
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
	public void RestoreHealth(){
		if(playerHealth != 2){
			playerHealth = playerHealth + 1;
		}

	}
	public void PlayerDeath(){
		//Vad händer när spelaren dör?
		//Spawna vid checkpoint.
	}
}
