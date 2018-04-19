using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Hurt")]

public class HurtState : State {

    public float yVelocity;
    public float xVelocity;
    private PlayerController _controller;
    private bool toTheRight;

    public override void Initialize(Controller owner)
    {
        _controller = (PlayerController)owner;
    }

    public override void Enter()
    {
        PushMovement();
    }

    public override void Update () {
        UpdateGravity();
        RaycastHit2D[] hits = _controller.DetectHits();
        GroundCheck(hits);
    }

    private void PushMovement()
    {
        _controller.Velocity += new Vector2(xVelocity, yVelocity);
    }

    private void GroundCheck(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit in hits)
        {
            if (MathHelper.CheckAllowedSlope(_controller.SlopeAngles, hit.normal))
            {
                _controller.TransitionTo<GroundState>();
            }
        }
    }

    private void UpdateGravity()
    {
        //float gravityModifier = _controller.Velocity.y < 0.0f ? FastFallModifier : 1f;
        _controller.Velocity += Vector2.down * _controller.Gravity * Time.deltaTime;
    }
}
