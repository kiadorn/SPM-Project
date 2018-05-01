using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/States/BossStage2")]
public class BossStage2 : State {

    private BossController _controller;

    public override void Initialize(Controller owner) {
        _controller = (BossController)owner;
    }

    public override void Update() {
        if (Input.GetKey("k")) {
            _controller.TransitionTo<BossStage3>();
        }
    }

}
