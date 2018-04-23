using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public Text HealthUI;
    public Text CurrencyUI;
    public GameObject SwordIcon;
    public GameObject ShieldIcon;

    public GameObject Player;

    public int StartingHealth;

    public int HealthPoints;
    public int Currency = 0;
    public bool HasSword;
    public bool HasShield;

    public CheckPoint Current;
    
    public void ChangeHealth(int i) {
        HealthPoints += i;
        CheckIfDead();
    }



    public void ChangeCurrency(int i) {
        Currency += i;
        CurrencyUI.text = Currency.ToString();
    }

	// Use this for initialization
	void Start () {
        HealthPoints = StartingHealth;
        UpdateHealth();
        ChangeCurrency(Currency);
	}

    public void SwordObtain() {
        SwordIcon.SetActive(true);
        HasSword = true;
    }

    public void ShieldObtain() {
        ShieldIcon.SetActive(true);
        HasShield = true;
    }

    private void UpdateHealth() {
        HealthUI.text = HealthPoints.ToString();
    }

    private void CheckIfDead() {
        if (HealthPoints < 1) {
            //Kill player-move to respawn;
            HealthUI.text = "Dead";
        }
        else {
            UpdateHealth();
        }
    }
	
	// Update is called once per frame
	void Update () {
        /*
        //Testing Health
        if (Input.GetKeyDown("o")) {
            ChangeHealth(1);
        }
        if (Input.GetKeyDown("u")) {
            ChangeHealth(-1);
        }
        //Testing Health
        if (Input.GetKeyDown("k")) {
            ChangeCurrency(1);
        }
        if (Input.GetKeyDown("l")) {
            ChangeCurrency(-1);
        }*/
        if (Input.GetKeyDown("m")) {
            if (Current)
                Player.transform.position = Current.transform.position;
        }
    }
}
