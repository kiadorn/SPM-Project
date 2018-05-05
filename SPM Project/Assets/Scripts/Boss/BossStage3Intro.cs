using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/States/BossStage3Intro")]
public class BossStage3Intro : State
{
    private BossController _controller;

    public override void Initialize(Controller owner)
    {
        _controller = (BossController)owner;
    }

    public override void Update()
    {
        //Gör minus ett till höger??????
        _controller.RotateBossRoom(1, -1);
        if (_controller.done)
        {
            _controller.done = false;
            _controller.TransitionTo<BossStage3>();
        }
    }

    public override void Enter()
    {
        _controller.Player.TransitionTo<PauseNoVelocityState>();
        _controller.Player.gameObject.transform.SetParent(_controller.BossRoom.transform);
        _controller.rightWall.transform.tag = "Untagged";
        _controller.turret1.SetActive(false); _controller.turret2.SetActive(false);
    }

    public override void Exit()
    {
        _controller.Player.TransitionTo<AirState>();
        _controller.Player.gameObject.transform.SetParent(null);
        _controller.Player.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        CameraShake.AddIntensity(10);
    }
}
