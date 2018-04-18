using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour {
	private int playerHealth;

	private void Start(){
		playerHealth = 2;
	}

	//Spelarhälsa
	public void RemoveHealth(int d){
		playerHealth = playerHealth - d;
		if(playerHealth <= 0){
			PlayerDeath ();
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
