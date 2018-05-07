using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

    [Header("References")]
    public Text HealthUI;
    public Text CurrencyUI;
    public GameObject KeyIcon;
    public GameObject SwordIcon;
    public GameObject DashIcon;
    public GameObject AttackControl;
    public GameManager manager;
    private PlayerController _controller;

    [Header("Stats")]
    public bool HasSword;
    public int Currency = 0;
    public int CurrentHealth;
    public int StartingHealth;
    public int LoadedHealthPoints;
    public CheckPoint CurrentCheckPoint;

    public float InvulnerableTime;
    [ReadOnly] public bool paused;
    public bool _invulnerable = false;
    private float timer;
    private float colorSwapTimer;
    private bool swapping;
    public bool hasKey = false;

    public String BossStageName;
    private String currentScene;
    [ReadOnly] public int SavedCurrency = 0;

    void Start()
    {
        _controller = GetComponent<PlayerController>();
        CurrentHealth = StartingHealth;
        UpdateHealth();
        ChangeCurrency(Currency);
        currentScene = SceneManager.GetActiveScene().name;

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
                _invulnerable = false;
                timer = 0;
            }
        }

        changeDashIcon();
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
        if (paused) { return; }
        if (!_invulnerable && i < 0)
        {
            gameObject.GetComponent<PlayerController>().TransitionTo<HurtState>();
            CurrentHealth += i;
            _invulnerable = true;
        }

        else if (i > 0)
        {
            CurrentHealth += i;
        }
        CheckIfDead();
    }

    public void ChangeCurrency(int i)
    {
        Currency += i;
        UpdateCurrency();
    }

    private void UpdateCurrency() {
        CurrencyUI.text = Currency.ToString();
    }

    public void SavePlayerStats()
    {
        GameManager.SavePlayer();
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
            if(BossStageName != null && currentScene == BossStageName)
            {
                SceneManager.LoadScene(currentScene);
            }
            else
            {
                Death();
            }

        }
        else
        {
            UpdateHealth();
        }
    }

    public void Death() {
        _controller.Velocity = Vector2.zero;
		//Audio
		_controller.source2.clip = _controller.DeathSound;
		_controller.source2.Play ();

        ChangeKeyStatus(false);
        Currency = SavedCurrency;
        UpdateCurrency();
        GetComponentInChildren<TrailRenderer>().enabled = false;
        transform.SetParent(null);
        transform.position = CurrentCheckPoint.transform.position;
        GetComponentInChildren<TrailRenderer>().enabled = true;
        CurrentCheckPoint.EnableEnemies();
        CurrentHealth = StartingHealth;
        UpdateHealth();
        _invulnerable = false;
        _controller.TransitionTo<AirState>();
    }

    public void ChangeKeyStatus(bool change)
    {
        hasKey = change;
        KeyIcon.SetActive(change);
    }
    
    public void ObtainSword()
    {
        SwordIcon.SetActive(true);
        AttackControl.SetActive(true);
    }

    private void changeDashIcon()
    {
        if (_controller.GetState<AirState>().canDash)
        {
            DashIcon.GetComponent<Image>().color = Color.white;
        } else
        {
            DashIcon.GetComponent<Image>().color = Color.black;
        }
    }
}

