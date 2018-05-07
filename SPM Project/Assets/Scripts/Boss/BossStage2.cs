using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/States/BossStage2")]
public class BossStage2 : State {

    private BossController _controller;
    private GameObject turret1;
    private GameObject turret2;

    public override void Initialize(Controller owner) {
        _controller = (BossController)owner;
        turret1 = _controller.turret1;
        turret2 = _controller.turret2;
    }

    public override void Update() {
        if (Input.GetKey("k")) {
            _controller.TransitionTo<BossStage3Intro>();
        }

        if (!turret1.activeSelf && !turret2.activeSelf)
        {
            _controller.TransitionTo<BossStage3Intro>();
        }
    }

    public override void Enter()
    {
        turret1.GetComponent<TurretController>().SwitchInvulnerableState();
        turret2.GetComponent<TurretController>().SwitchInvulnerableState();
    }

    public override void Exit()
    {
		_controller.turret1.SetActive(false);
		_controller.turret2.SetActive(false);
    }
}
