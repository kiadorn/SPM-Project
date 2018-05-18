using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour {

    Animator anim;
    PlayerController Player;
    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimation();

    }
    public void SetAnimation()
    {
        if (Player.CurrentState is AirState)
        {
            anim.SetBool("RunningState", false);
            anim.SetBool("WallState", false);
            anim.SetBool("JumpingState", true);
            anim.SetBool("IsIdle", false);
            Debug.Log("Hoppar");
        }
        if ((Player.CurrentState is GroundState && Player.Velocity.x >= 0.5) || (Player.CurrentState is GroundState && Player.Velocity.x <= -0.5))
        {
            anim.SetBool("RunningState", true);
            anim.SetBool("WallState", false);
            anim.SetBool("JumpingState", false);
            anim.SetBool("IsIdle", false);
        }
        if (Player.CurrentState is WallState)
        {
            anim.SetBool("RunningState", false);
            anim.SetBool("WallState", true);
            anim.SetBool("JumpingState", false);
            anim.SetBool("IsIdle", false);
        }
        if (Player.CurrentState is GroundState && Player.Velocity.x == 0)
        {
            anim.SetBool("RunningState", false);
            anim.SetBool("WallState", false);
            anim.SetBool("JumpingState", false);
            anim.SetBool("IsIdle", true);
            Debug.Log("Idle" + Player.CurrentState);
        }
        if (Player.CurrentState is HurtState)
        {

        }
        if (Player.CurrentState is PlayerAttack)
        {

        }
        if (Player.GetComponent<PlayerStats>().dead)
        {
            anim.SetBool("Dead", true);
        }
        else anim.SetBool("Dead", false);
    }
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            anim.SetTrigger("Die");
        }
    }
}

