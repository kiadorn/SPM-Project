using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/States/BossStage1")]
public class BossStage1 : State {

    private BossController _controller;

    public override void Initialize(Controller owner) {
        _controller = (BossController)owner;
    }

    public override void Update() {
        if (Input.GetKey("j")) {
            _controller.TransitionTo<BossStage2>();
        }
    }

}
