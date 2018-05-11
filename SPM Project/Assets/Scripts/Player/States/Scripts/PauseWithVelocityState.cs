using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/PauseWithVelocity")]
public class PauseWithVelocityState : State
{

    [ReadOnly] public PlayerController _controller;
    [ReadOnly] public Vector2 exitVelocity;

    public override void Initialize(Controller owner)
    {
        _controller = (PlayerController)owner;
    }

    public override void Enter()
    {
        _controller.gameObject.GetComponent<PlayerStats>().paused = true;
        exitVelocity = _controller.Velocity;
        _controller.Velocity = Vector2.zero;
		_controller.sources[1].Stop ();
    }

    public override void Exit()
    {
        _controller.gameObject.GetComponent<PlayerStats>().paused = false;
        _controller.Velocity = exitVelocity;
    }

    public override void Update()
    {

    }
}
