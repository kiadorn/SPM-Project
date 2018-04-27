using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Player/States/Hurt")]

public class HurtState : State {

    public float yVelocity;
    public float xVelocity;
    private float tempX;
    private Vector2 Velocity
    {
        get { return _controller.Velocity; }
        set { _controller.Velocity = value; }
    }
    private PlayerController _controller;
    private bool toTheRight;
    private float timer;
    private List<Collider2D> _ignoredPlatforms = new List<Collider2D>();

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
        UpdateNormalForce(hits);
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
        Velocity = new Vector2(tempX, yVelocity);
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

    private void UpdateNormalForce(RaycastHit2D[] hits)
    {
        if (hits.Length == 0) return;
        RaycastHit2D snapHit = hits.FirstOrDefault(h => !h.collider.CompareTag("OneWay"));
        if (snapHit.collider != null) _controller.SnapToHit(snapHit);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("OneWay") && Velocity.y > 0.0f && !_ignoredPlatforms.Contains(hit.collider))
            {
                _ignoredPlatforms.Add(hit.collider);
            }
            if (_ignoredPlatforms.Contains(hit.collider))
                continue;

            Velocity += MathHelper.GetNormalForce(Velocity, hit.normal);
            if (MathHelper.CheckAllowedSlope(_controller.SlopeAngles, hit.normal))
                _controller.TransitionTo<GroundState>();

            if (MathHelper.GetWallAngleDelta(hit.normal) < _controller.MaxWallAngleDelta && Vector2.Dot((hit.point - (Vector2)_controller.transform.position).normalized, Velocity.normalized) > 0.0f)
            {
                if (!hit.collider.CompareTag("Unclimbable Wall"))
                {
                    _controller.TransitionTo<WallState>();
                }
            }
        }
    }

    private void UpdateGravity()
    {
        _controller.Velocity += Vector2.down * _controller.Gravity * Time.deltaTime;
    }
}
