using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Hurt")]

public class HurtState : State {

    public float yVelocity;
    public float xVelocity;
    private float tempX;
    private PlayerController _controller;
    private bool toTheRight;
    private float timer;

    public override void Initialize(Controller owner)
    {
        _controller = (PlayerController)owner;
    }

    public override void Enter()
    {
        PushMovement();
        timer = 0;

    }

    public override void Update () {
        UpdateGravity();
        RaycastHit2D[] hits = _controller.DetectHits();
        _controller.transform.Translate(_controller.Velocity * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer > 0.2f) {
            GroundCheck(hits);
        }
    }

    private void PushMovement()
    {
        if (_controller.Velocity.x > 0)
        {
            tempX = xVelocity * -1f;
        } else
        {
            tempX = xVelocity;
        }
        _controller.Velocity = new Vector2(tempX, yVelocity);
    }

    private void GroundCheck(RaycastHit2D[] hits)
    {
        if (hits.Length == 0) return;
        _controller.SnapToHit(hits[0]);
        foreach (RaycastHit2D hit in hits)
        {
            _controller.Velocity += MathHelper.GetNormalForce(_controller.Velocity, hit.normal);
            if (MathHelper.CheckAllowedSlope(_controller.SlopeAngles, hit.normal))
            {
                _controller.TransitionTo<GroundState>();
            }
        }
    }

    private void UpdateGravity()
    {
        _controller.Velocity += Vector2.down * _controller.Gravity * Time.deltaTime;
    }
}
