using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/PauseNoVelocity")]
public class PauseNoVelocityState : State {

    [ReadOnly] public PlayerController _controller;

    public override void Initialize(Controller owner)
    {
        _controller = (PlayerController)owner;
    }

    public override void Enter()
    {
        _controller.gameObject.GetComponent<PlayerStats>().paused = true;
        _controller.Velocity = Vector2.zero;
		_controller.sources[1].Stop ();
    }

    public override void Exit()
    {
        _controller.gameObject.GetComponent<PlayerStats>().paused = false;
    }

    public override void Update()
    {

    }
}
