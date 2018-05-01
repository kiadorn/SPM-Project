using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/States/BossStage2Intro")]
public class BossStage2Intro : State {

    private BossController _controller;

    public override void Initialize(Controller owner)
    {
        _controller = (BossController)owner;
    }

    public override void Update()
    {
        //Gör minus ett
        _controller.RotateBossRoom(1, -1);
        if (_controller.done)
        {
            _controller.done = false;
            _controller.TransitionTo<BossStage2>();
        }
    }

    public override void Enter()
    {
        _controller.Player.TransitionTo<PauseState>();
    }

    public override void Exit()
    {
        _controller.Player.TransitionTo<AirState>();
    }

}
