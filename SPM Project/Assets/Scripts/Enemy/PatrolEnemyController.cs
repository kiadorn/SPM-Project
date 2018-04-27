using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyController : Controller
{

    public Transform player;
    public PlayerStats playerStats;
    [Header("Patrol State")]
    public Transform groundDetection;
    public bool startMovingRight;
    public float groundCheckDistance;
    public float speed;
    [HideInInspector]
    public float saveSpeed;
    public int startingHealth;
    private int currentHealth;
    
    [HideInInspector]
    public AudioSource source;
    [Header("Audio Clips")]
    public AudioClip Skitter;
    public AudioClip Alerted;
    public AudioClip Death; //inte använd än

    private Vector3 OGPos;

    new void Awake()
    {
        saveSpeed = speed;
        source = GetComponent<AudioSource>();
        OGPos = transform.position;
        base.Awake();
    }

    private void Update()
    {
        CurrentState.Update();
    }

    private void OnValidate()
    {
        transform.eulerAngles = (startMovingRight == true) ? new Vector3(0, 0, 0) : new Vector3(0, -180, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerStats.ChangeHealth(-1);
            
        }
    }

    private void OnEnable() {
        GetComponent<BoxCollider2D>().enabled = true;
        currentHealth = startingHealth;
        transform.position = OGPos;
    }

    /* private void TakeDamage() //Alexanders påbörjad lösning
    {
        currentHealth--;
        if (currentHealth == 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            while (!source.isPlaying)
            {
                gameObject.SetActive(false);
            }
        }
    } */
}
