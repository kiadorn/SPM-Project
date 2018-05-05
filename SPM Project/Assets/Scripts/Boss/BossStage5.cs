using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Boss/States/BossStage5")]
public class BossStage5 : State {

    public float timerModifier;
    private float timer;
    private BossController _controller;

    public override void Initialize(Controller owner)
    {
        _controller = (BossController)owner;
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        CameraShake.AddIntensity(timer);
        _controller.whiteScreen.GetComponent<Image>().color = new Color(1, 1, 1, timer/timerModifier);
    }

    public override void Enter()
    {
        _controller.Player.TransitionTo<PauseNoVelocityState>();
    }
}
