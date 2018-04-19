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
            //Vector2 direction = (Vector2)player.position - (Vector2)this.transform.position;
            //player.gameObject.GetComponent<PlayerController>().Velocity += direction * 100;
           /* if ((player.position.x - transform.position.x) > 0)
            {
                
                player.gameObject.GetComponent<PlayerController>().Velocity += new Vector2(20, 10);
            } else
            {
                player.gameObject.GetComponent<PlayerController>().Velocity += new Vector2(-20, 10);
            }*/
        }
    }
}
