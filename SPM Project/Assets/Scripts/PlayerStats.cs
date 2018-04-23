using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {
    public Text HealthUI;
    public Text CurrencyUI;
    public int HealthPoints;
    public int Currency = 0;
    public GameObject Player;
    public int StartingHealth;
    public int CurrentHealth;
    public GameObject stats;
    private GameManager manager;
    public void ChangeHealth(int i)
    {
        HealthPoints += i;
        CheckIfDead();
    }

    public void ChangeCurrency(int i)
    {
        Currency += i;
        CurrencyUI.text = Currency.ToString();
    }

    // Use this for initialization
    void Start()
    {
        manager = stats.GetComponent<GameManager>();
        HealthPoints = StartingHealth;
        UpdateHealth();
        ChangeCurrency(Currency);
        
    }
    private void LoadStats()
    {
        if (Input.GetKeyDown("u"))
        {
            HealthPoints = manager.HealthPoints;
            Currency = manager.Currency;
        }

    }

    private void UpdateHealth()
    {
        this.HealthUI.text = HealthPoints.ToString();
    }

    private void CheckIfDead()
    {
        if (HealthPoints < 1)
        {
            //Kill player-move to respawn;
            this.HealthUI.text = "Dead";
        }
        else
        {
            UpdateHealth();
        }
    }

    //Update is called once per frame
    void Update()
    {
        LoadStats();
        //Testing Health
        if (Input.GetKeyDown("o"))
        {
            ChangeHealth(1);
        }
        if (Input.GetKeyDown("p"))
        {
            ChangeHealth(-1);
        }
        //Testing Health
        if (Input.GetKeyDown("k"))
        {
            ChangeCurrency(1);
        }
        if (Input.GetKeyDown("l"))
        {
            ChangeCurrency(-1);
        }

    }
}

