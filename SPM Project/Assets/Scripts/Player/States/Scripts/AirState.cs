using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName = "Player/States/Air")]
public class AirState : State{
	[Header("Movement")]
	public float Acceleration;
	public float Friction;

	public bool CanCancelJump;
	public bool canDash;
	private float FastFallModifier = 1.2f;
	private PlayerController _controller;
	private Transform transform { get { return _controller.transform; }}
	private Vector2 Velocity { get { return _controller.Velocity; }
		set{ _controller.Velocity = value; } }
    private List<Collider2D> _ignoredPlatforms = new List<Collider2D>();
	
	public override void Initialize(Controller owner)
	{
		_controller = (PlayerController)owner;
	}

	public override void Update()
	{
		UpdateMovement ();
		if((Input.GetButtonDown("Dash") || Input.GetAxis("Dash") != 0) && canDash){
            //_controller.TransitionTo<DashState>();
            _controller.TransitionTo<DashVelocityState>();
			canDash = false;
		}
        UpdateMovement();
        UpdateGravity();
		RaycastHit2D[] hits = _controller.DetectHits();
		UpdateNormalForce(hits);
		transform.Translate(Velocity * Time.deltaTime);
		CancelJump ();
	}

	private void UpdateMovement()
	{
		float input = Input.GetAxisRaw("Horizontal");
		if (Mathf.Abs(input) > _controller.InputMagnitudeToMove)
		{
			Vector2 delta = Vector2.right * input * Acceleration * Time.deltaTime;
			if (Mathf.Abs ((Velocity + delta).x) < _controller.MaxSpeed || Mathf.Abs (Velocity.x) > _controller.MaxSpeed && Vector2.Dot (Velocity.normalized, delta) < 0.0f) {
				Velocity += delta;
			} else {
				_controller.Velocity.x = MathHelper.Sign (input) * _controller.MaxSpeed;
			}
		}
		else
		{
			Vector2 currentDirection = Vector2.right * MathHelper.Sign(Velocity.x);
			float horizontalVelocity = Vector2.Dot(Velocity.normalized, currentDirection) * Velocity.magnitude;
			Velocity -= currentDirection * horizontalVelocity * Friction * Time.deltaTime;
		}
	}

	private void CancelJump()
	{
		float minJumpVelocity = _controller.GetState<GroundState>().JumpVelocity.Min;
		if (Velocity.y < minJumpVelocity) {
			CanCancelJump = false;
		}
		if (!CanCancelJump || Input.GetButton ("Jump")) {
			return;
		}
		CanCancelJump = false;
		_controller.Velocity.y = minJumpVelocity;
	}

	private void UpdateGravity()
	{
		float gravityModifier = Velocity.y < 0.0f ? FastFallModifier : 1f;
		Velocity += Vector2.down * _controller.Gravity * gravityModifier * Time.deltaTime;
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

            if (MathHelper.GetWallAngleDelta(hit.normal) < _controller.MaxWallAngleDelta && Vector2.Dot((hit.point - (Vector2)transform.position).normalized, Velocity.normalized) > 0.0f)
            {
                if (!hit.collider.CompareTag("Unclimbable Wall")) {
                    _controller.TransitionTo<WallState>();
                }
            }
		}

        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + (Vector3)_controller.Collider.offset, _controller.Collider.size, 0.0f, _controller.CollisionLayers);
        for (int i = _ignoredPlatforms.Count-1; i >= 0; i--)
        {
            if (!colliders.Contains(_ignoredPlatforms[i]))
            {
                _ignoredPlatforms.Remove(_ignoredPlatforms[i]);
            }
        }
	}
}