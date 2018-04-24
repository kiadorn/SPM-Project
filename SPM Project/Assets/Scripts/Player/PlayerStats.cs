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

    // Use this for initialization
    void Start()
    {
        manager = stats.GetComponent<GameManager>();
        CurrentHealth = LoadedHealthPoints;
        UpdateHealth();
        ChangeCurrency(Currency);
        
    }
    public void SavePlayerStats()
    {
        GameManager.SavePlayer(this);
    }
    private void LoadStats()
    {
        if (Input.GetKeyDown("u"))
        {
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
            //Kill player-move to respawn;
            this.HealthUI.text = "Dead";
        }
        else
        {
            UpdateHealth();
        }
    }
<<<<<<< HEAD:SPM Project/Assets/Scripts/Player/PlayerStats.cs
	
	// Update is called once per frame
	void Update () {
        /*
=======

    //Update is called once per frame
    void Update()
    {
        LoadStats();
>>>>>>> origin/Zake:SPM Project/Assets/Scripts/PlayerStats.cs
        //Testing Health
        if (Input.GetKeyDown("o"))
        {
            ChangeHealth(1);
        }
<<<<<<< HEAD:SPM Project/Assets/Scripts/Player/PlayerStats.cs
        if (Input.GetKeyDown("u")) {
=======
        if (Input.GetKeyDown("p"))
        {
>>>>>>> origin/Zake:SPM Project/Assets/Scripts/PlayerStats.cs
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
<<<<<<< HEAD:SPM Project/Assets/Scripts/Player/PlayerStats.cs
        }*/
        if (Input.GetKeyDown("m")) {
            if (Current)
                Player.transform.position = Current.transform.position;
=======
        }
        if (Input.GetKeyDown("n"))
        {
            SavePlayerStats();
>>>>>>> origin/Zake:SPM Project/Assets/Scripts/PlayerStats.cs
        }
       
    }
}

