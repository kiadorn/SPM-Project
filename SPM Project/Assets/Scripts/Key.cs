using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
    public string LevelThatHasBeenCompleted; //skriv vilken nivå det här objektet tillhör i den instansen av prefabben
	void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player") ) {
            Debug.Log("COLLIDING WITH PLAYER");
            this.gameObject.SetActive(false);
            if (GameManager.instance.Level1Done == false && LevelThatHasBeenCompleted == "1")
            {
                GameManager.instance.Level1Done = true;
                Debug.Log(GameManager.instance.Level1Done);
            }
            if (GameManager.instance.Level1Done == true && LevelThatHasBeenCompleted == "2")
            {
                GameManager.instance.Level2Done = true;
                Debug.Log(GameManager.instance.Level2Done);

            }
        }
    }


}
