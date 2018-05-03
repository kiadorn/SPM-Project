using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            col.gameObject.GetComponent<PlayerStats>().ChangeHealth(1);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(WaitForSound());
        }
    }

    private IEnumerator WaitForSound() {
        yield return new WaitForSeconds(2f); //Ersätt tid med längd för ljudklipp
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        gameObject.SetActive(false);
        yield return 0;
    }
}
