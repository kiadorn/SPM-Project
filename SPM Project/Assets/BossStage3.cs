using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStage3 : State {

    public GameObject hand;
    public float startTime;

    public override void Enter()
    {
        hand.SetActive(true);
    }

    public override void Exit()
    {
        hand.SetActive(false);
    }
}
