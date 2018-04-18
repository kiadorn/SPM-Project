using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName = "Player/States/Air")]
public class AirState : State{
	[Header("Movement")]
	public float dashDistance;
	public float Acceleration;
	public float Friction;

	public bool CanCancelJump;
	public bool canDash;
	private float FastFallModifier = 2f;
	private PlayerController _controller;
	private Transform transform { get { return _controller.transform; }}
	private Vector2 Velocity { get { return _controller.Velocity; }
		set{ _controller.Velocity = value; } }
	
	public override void Initialize(Controller owner)
	{
		_controller = (PlayerController)owner;
	}

	public override void Update()
	{
		UpdateDash ();
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
			if (Mathf.Abs ((_controller.Velocity + delta).x) < _controller.MaxSpeed || Mathf.Abs (Velocity.x) > _controller.MaxSpeed && Vector2.Dot (Velocity.normalized, delta) < 0.0f) {
				_controller.Velocity += delta;
			} else {
				_controller.Velocity.x = MathHelper.Sign (input) * _controller.MaxSpeed;
			}
		}
		else
		{
			Vector2 currentDirection = Vector2.right * MathHelper.Sign(Velocity.x);
			float horizontalVelocity = Vector2.Dot(Velocity.normalized, currentDirection) *
				Velocity.magnitude;
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

	public void UpdateDash()
	{
		if (!Input.GetButtonDown ("Jump") || canDash == false) {
			return;
		} else if (Input.GetButton ("Right") && Input.GetButtonDown ("Jump")) {
			transform.position += Vector3.right * dashDistance;
		} else if (Input.GetButton ("Left") && Input.GetButtonDown ("Jump")) {
			transform.position += Vector3.left * dashDistance;
		} else if (Input.GetButton ("Up") && Input.GetButtonDown ("Jump")) {
			transform.position += Vector3.up * dashDistance;
		} else if (Input.GetButton ("Down") && Input.GetButtonDown ("Jump")) {
			transform.position += Vector3.down * dashDistance;
		} else if ((Input.GetButton ("Right") && Input.GetButton ("Up")) && Input.GetButtonDown ("Jump")) {
			transform.position += new Vector3(1,1,0) * dashDistance;
		} else if ((Input.GetButton ("Right") && Input.GetButton ("Down")) && Input.GetButtonDown ("Jump")) {
			transform.position += new Vector3(1,-1,0) * dashDistance;
		} else if ((Input.GetButton ("Left") && Input.GetButton ("Up")) && Input.GetButtonDown ("Jump")) {
			transform.position += new Vector3(-1,1,0) * dashDistance;
		} else if ((Input.GetButton ("Left") && Input.GetButton ("Down")) && Input.GetButtonDown ("Jump")) {
			transform.position += new Vector3(-1,-1,0) * dashDistance;
		} 

	}
}