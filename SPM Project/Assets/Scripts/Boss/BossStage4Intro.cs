using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/States/BossStage4Intro")]
public class BossStage4Intro : State
{
    private BossController _controller;

    public override void Initialize(Controller owner)
    {
        _controller = (BossController)owner;
    }

    public override void Update()
    {
        //Gör minus ett till höger
        _controller.RotateBossRoom(-89, -1);
        if (_controller.done)
        {
            _controller.done = false;
            _controller.TransitionTo<BossStage4>();
        }
    }

    public override void Enter()
    {
        _controller.Player.TransitionTo<PauseState>();
        _controller.Player.gameObject.transform.SetParent(_controller.BossRoom.transform);
        _controller.leftWall.transform.tag = "Unclimbable Wall";
        foreach (GameObject go in _controller.stage4Objects)
        {
            go.SetActive(true);
        }
    }

    public override void Exit()
    {
        _controller.Player.TransitionTo<AirState>();
        _controller.Player.gameObject.transform.SetParent(null);
        _controller.Player.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        CameraShake.AddIntensity(10);
    }

}
