﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/States/BossStage4")]
public class BossStage4 : State {

    private BossController _controller;
    private GameObject hand;

    public float Stage4FollowSpeed;
    public float Stage4AttackCooldown;
    public float Stage4ResetCooldown;

    public override void Initialize(Controller owner) {
        _controller = (BossController)owner;
        hand = _controller.Hand;
    }

    public override void Enter() {
        hand.SetActive(true);
        hand.GetComponent<HandSmash>().followSpeed = Stage4FollowSpeed;
        hand.GetComponent<HandSmash>().AttackCooldown = Stage4AttackCooldown;
        hand.GetComponent<HandSmash>().resetCooldown = Stage4ResetCooldown;
        hand.GetComponent<HandSmash>().timeToAttack /= 2;
        hand.GetComponent<HandSmash>().timeToStop /= 2;

    }

    public override void Exit() {
        hand.SetActive(false);
    }

    //public override void Update() {
    //    if (Input.GetKey("b")) {
    //        _controller.TransitionTo<BossStage1>();
    //    }
    //}

}
