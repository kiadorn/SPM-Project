using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/DashVelocity")]
public class DashVelocityState : State
{
    public float waitTime = 1f;
    public float speed;
    private float timer;
    private Vector2 Velocity { get { return _controller.Velocity; }
        set { _controller.Velocity = value; } }
    private Transform transform { get { return _controller.transform; } }
    private PlayerController _controller;
    private float xDir;
    private float yDir; 
    private float magnitude;

    public override void Initialize(Controller owner)
    {
        _controller = (PlayerController)owner;
    }

    public override void Enter()
    {
        magnitude = Velocity.magnitude;
        xDir = Input.GetAxisRaw("Horizontal");
        yDir = Input.GetAxisRaw("Vertical");
        Velocity = Vector2.zero;
    }
    public override void Update()
    {
        Velocity = new Vector2(xDir, yDir).normalized * speed;
        
        Debug.Log(Velocity);
        RaycastHit2D[] hits = _controller.DetectHits();
        UpdateNormalForce(hits);
        Dash2();
        transform.Translate(Velocity * Time.deltaTime);
    }
    
    public override void Exit()
    {
        if (magnitude > _controller.MaxSpeed)
        {
            magnitude = _controller.MaxSpeed;
        }
        //Velocity = Velocity.normalized * magnitude;
        Velocity = Vector2.zero;
        //Velocity = new Vector2(xDir, yDir).normalized * speed;
    }

    public void Dash2()
    {
        timer += Time.deltaTime;
        if (timer < waitTime)
        {
            return;
        }
        timer = 0;
        _controller.TransitionTo<AirState>();
    }

    //Collision tests
    private void UpdateNormalForce(RaycastHit2D[] hits)
    {
        if (hits.Length == 0) return;
        _controller.SnapToHit(hits[0]);
        foreach (RaycastHit2D hit in hits)
        {
            Velocity += MathHelper.GetNormalForce(Velocity, hit.normal);
            if (MathHelper.CheckAllowedSlope(_controller.SlopeAngles, hit.normal))
                _controller.TransitionTo<GroundState>();

            if (MathHelper.GetWallAngleDelta(hit.normal) < _controller.MaxWallAngleDelta
                && Vector2.Dot((hit.point - (Vector2)transform.position).normalized,
                    Velocity.normalized) > 0.0f)
                _controller.TransitionTo<WallState>();
        }

    }
}