using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/States/BossStage3")]
public class BossStage3 : State {

    private BossController _controller;
    public GameObject hand;
    public float startTime;

    public int HandHealth;


    public override void Initialize(Controller owner) {
        _controller = (BossController)owner;
        hand = _controller.Hand;
    }

    public override void Enter()
    {
        hand.SetActive(true);

    }

    public override void Exit()
    {
        hand.SetActive(false);
    }

    public override void Update() {
        if (Input.GetKey("l")) {
            _controller.TransitionTo<BossStage4Intro>();
        }

        if(hand.GetComponent<HandSmash>().CurrentHealth == 0) {
            _controller.TransitionTo<BossStage4Intro>();
        }
    }

}
