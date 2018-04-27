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

    [Header("Objektinstanser")]
    public GameObject Player;
    public GameObject stats;
    private GameManager manager;

    [Header("Stats och unlocks")]
    public bool HasSword;
    public int Currency = 0;
    public int CurrentHealth;
    public int StartingHealth;
    public int LoadedHealthPoints;
    public CheckPoint CurrentCheckPoint;

    void Start()
    {
        manager = stats.GetComponent<GameManager>();
        CurrentHealth = StartingHealth;
        UpdateHealth();
        ChangeCurrency(Currency);
        
    }

    void Update()
    {
        LoadStats();
        if (Input.GetKeyDown("n"))
        {
            SavePlayerStats();
        }
        if (Input.GetKeyDown("m"))
        {
            if (CurrentCheckPoint)
            {
                Death();
            }

        }
    }

    public void ChangeHealth(int i)
    {
        CurrentHealth += i;
        CheckIfDead();
    }

    public void ChangeCurrency(int i)
    {
        Currency += i;
        CurrencyUI.text = Currency.ToString();
    }

    public void SavePlayerStats()
    {
        GameManager.SavePlayer(this);
    }

    private void LoadStats()
    {
        if (Input.GetKeyDown("u"))
        {
            Debug.Log("Laddat från GameManager");
            CurrentHealth = manager.HealthPoints;
            Currency = manager.Currency;
        }
    }

    private void UpdateHealth()
    {
        this.HealthUI.text = CurrentHealth.ToString();
    }

    private void CheckIfDead()
    {
        if (CurrentHealth < 1)
        {
            Death();
        }
        else
        {
            UpdateHealth();
        }
    }

    public void Death() {
        Player.GetComponent<PlayerController>().TransitionTo<AirState>();
        Player.transform.position = CurrentCheckPoint.transform.position;
        CurrentCheckPoint.EnableEnemies();
        CurrentHealth = StartingHealth;
        UpdateHealth();
    }
}

