using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Pause")]
public class PauseState : State {

    public PlayerController _controller;

    public override void Initialize(Controller owner)
    {
        _controller = (PlayerController)owner;
    }

    public override void Enter()
    {
        _controller.Velocity = Vector2.zero;
    }


    public override void Update()
    {

    }
}
