using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    [Header("References")]
    public Text HealthUI;
    public Text CurrencyUI;
    public GameManager manager;

    [Header("Stats")]
    public bool HasSword;
    public int Currency = 0;
    public int CurrentHealth;
    public int StartingHealth;
    public int LoadedHealthPoints;
    public CheckPoint CurrentCheckPoint;

    public float InvulnerableTime;
    private bool _invulnerable = false;
    private float timer;
    private float colorSwapTimer;
    private bool swapping;

    void Start()
    {
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

        if (_invulnerable)
        {
            if (!swapping)
            {
                StartCoroutine(SwapColors());
            }
            timer += Time.deltaTime;
            if(timer >= InvulnerableTime)
            {
                Debug.Log("Jag är INTE invulnerable");
                _invulnerable = false;
                timer = 0;
            }
            
        }
    }

    private IEnumerator SwapColors()
    {
        swapping = true;
        for (float i = 0; i <= timer; i += 0.1f)
        { 
        GetComponentInChildren<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(0.1f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.1f);
        }
        swapping = false;
        yield return 0;
    }

    public void ChangeHealth(int i)
    {
        if (!_invulnerable || i > 0)
        {
            gameObject.GetComponent<PlayerController>().TransitionTo<HurtState>();
            CurrentHealth += i;
            _invulnerable = true;
            Debug.Log("Jag är invulnerable");
            CheckIfDead();
        }
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
        transform.position = CurrentCheckPoint.transform.position;
        CurrentCheckPoint.EnableEnemies();
        CurrentHealth = StartingHealth;
        UpdateHealth();
        _invulnerable = false;
        GetComponent<PlayerController>().TransitionTo<AirState>();
    }
}

