using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour {

    Animator anim;
    PlayerController Player;
    SpriteRenderer Legs;
    SpriteRenderer Torso;
    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        Torso = GetComponent<SpriteRenderer>(); ;
        Legs = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        SetAnimation();

    }
    public void SetAnimation()
    {
        if (Player.Velocity.x >= 0.2)
        {
            Torso.flipX = false;
            Legs.flipX = false;
        }
        if ( Player.Velocity.x <= -0.2)
        {
            Torso.flipX = true;
            Legs.flipX = true;
        }
            if (Player.CurrentState is AirState)
        {
            anim.SetBool("RunningState", false);
            anim.SetBool("WallState", false);
            anim.SetBool("JumpingState", true);
            anim.SetBool("IsIdle", false);
            anim.SetBool("Dashing", false);
            anim.SetBool("IsHurt", false);


        }
        if ((Player.CurrentState is GroundState && Player.Velocity.x >= 0.5) || (Player.CurrentState is GroundState && Player.Velocity.x <= -0.5))
        {
            anim.SetBool("RunningState", true);
            anim.SetBool("WallState", false);
            anim.SetBool("JumpingState", false);
            anim.SetBool("IsIdle", false);
            anim.SetBool("Dashing", false);
            anim.SetBool("IsHurt", false);

        }
        if (Player.CurrentState is WallState)
        {
            anim.SetBool("RunningState", false);
            anim.SetBool("WallState", true);
            anim.SetBool("JumpingState", false);
            anim.SetBool("IsIdle", false);
            anim.SetBool("Dashing", false);
            anim.SetBool("IsHurt", false);


        }
        if (Player.CurrentState is GroundState && Player.Velocity.x == 0)
        {
            anim.SetBool("RunningState", false);
            anim.SetBool("WallState", false);
            anim.SetBool("JumpingState", false);
            anim.SetBool("IsIdle", true);
            anim.SetBool("Dashing", false);
            anim.SetBool("IsHurt", false);


        }
        if (Player.CurrentState is DashVelocityState)
        {

            anim.SetBool("RunningState", false);
            anim.SetBool("WallState", false);
            anim.SetBool("JumpingState", false);
            anim.SetBool("IsIdle", false);
            anim.SetBool("Dashing", true);
            anim.SetBool("IsHurt", false);

        }
        if (Player.CurrentState is HurtState)
        {

            anim.SetBool("RunningState", false);
            anim.SetBool("WallState", false);
            anim.SetBool("JumpingState", false);
            anim.SetBool("IsIdle", false);
            anim.SetBool("Dashing", false);
            anim.SetBool("IsHurt", true);
        }
        
        if (Player.GetComponent<PlayerStats>().dead && !anim.GetBool("Dead"))
        {
            anim.SetBool("Dead", true);
            anim.SetBool("RunningState", false);
            anim.SetBool("WallState", false);
            anim.SetBool("JumpingState", false);
            anim.SetBool("IsIdle", false);
            anim.SetBool("Dashing", false);
            anim.SetBool("IsHurt", false);

        }
        if (!Player.GetComponent<PlayerStats>().dead)
        {
            anim.SetBool("Dead", false);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            anim.SetTrigger("Die");
        }
    }
}

