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
    [SerializeField]
    private float currentSpeed;

    private void Update()
    {
        CurrentState.Update();
    }

    private void OnValidate()
    {
        transform.eulerAngles = (startMovingRight == true) ? new Vector3(0, 0, 0) : new Vector3(0, -180, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerStats.ChangeHealth(-1);
            player.gameObject.GetComponent<PlayerController>().TransitionTo<HurtState>();
        }
    }
}
