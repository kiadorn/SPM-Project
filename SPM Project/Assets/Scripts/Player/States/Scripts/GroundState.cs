﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Ground")]
public class GroundState : State{
	private PlayerController _controller;
	private GameObject pitch;

	[Header("Movement")]
	public float Acceleration;
	public float Deceleration;
	public float TurnModifier;
	private Vector2 _groundNormal;

	[Header("Jumping")]
	public float InitialJumpDistance;
	public MinMaxFloat JumpVelocity;
	public MinMaxFloat JumpHeight;
	public float TimeToJumpApex;
	public int MaxJumps = 1;
	private int _jumps;
    private Vector2 previousVelocity;

	private Vector2 VectorAlongGround { get { return
			MathHelper.RotateVector(_groundNormal, -90f);} }
	private Transform transform { get { return _controller.transform; }}
	private Vector2 Velocity { get { return _controller.Velocity; }
		set{ _controller.Velocity = value; } }


	public override void Initialize(Controller owner){
		_controller = (PlayerController)owner;
		_controller.Gravity = (2 * JumpHeight.Max) / Mathf.Pow(TimeToJumpApex, 2);
		JumpVelocity.Max = _controller.Gravity * TimeToJumpApex;
		JumpVelocity.Min = Mathf.Sqrt(2 * _controller.Gravity * JumpHeight.Min);
		pitch = GameObject.Find ("AudioPitchController");
	}

	public override void Enter()
	{
		_controller.GetState<AirState> ().canDash = true;
		_jumps = MaxJumps;
		_controller.sources [1].pitch = 1f;
		_controller.sources [1].volume = 0.5f;
	}
	public override void Update()
	{
		UpdateGravity();
		RaycastHit2D[] hits = _controller.DetectHits(true);
		if (hits.Length == 0){
			_controller.sources[1].Stop ();
			_controller.TransitionTo<AirState>();
			return;
		}
		UpdateGroundNormal(hits);
		UpdateMovement();
		CheckVelocity ();
		UpdateNormalForce(hits);
		transform.Translate(Velocity * Time.deltaTime);
		UpdateJump();
	}
	public void UpdateJump()
	{
		if (!Input.GetButtonDown("Jump") || _jumps <= 0 || _controller.pauseScreen.activeSelf) return;
		transform.position += Vector3.up * InitialJumpDistance;
		_controller.Velocity.y = JumpVelocity.Max;
		_jumps--;
		_controller.sources [1].loop = false;
		_controller.sources [1].Stop ();
		int length = _controller.Jump.Length;
		int replace = Random.Range (0, (length - 1));
		_controller.sources [1].clip = _controller.Jump[replace];
		pitch.GetComponent<PitchController> ().Pichter (_controller.sources [1]);
		_controller.sources [1].Play ();
		_controller.JumpJustPlayed = _controller.Jump [replace];
		_controller.Jump [replace] = _controller.Jump [length - 1];
		_controller.Jump [length - 1] = _controller.JumpJustPlayed;

		_controller.GetState<AirState>().CanCancelJump = true;
		if(!(_controller.CurrentState is AirState)) _controller.TransitionTo<AirState>();
	}
	private void UpdateGravity()
	{
		Velocity += Vector2.down * _controller.Gravity * Time.deltaTime;
	}
	private void UpdateGroundNormal(RaycastHit2D[] hits)
	{
		int numberOfGroundHits = 0;
		_groundNormal = Vector2.zero;
		foreach (RaycastHit2D hit in hits)
		{
			if (!MathHelper.CheckAllowedSlope(_controller.SlopeAngles, hit.normal))
				continue;
			_groundNormal += hit.normal;
			numberOfGroundHits++;
		}
		if (numberOfGroundHits == 0)
		{
			_controller.TransitionTo<AirState>();
			return;
		}
		_groundNormal /= numberOfGroundHits;
		_groundNormal.Normalize();
	}
	private void UpdateNormalForce(RaycastHit2D[] hits)
	{
		if (hits.Length == 0) return;
		_controller.SnapToHit(hits[0]);
		foreach (RaycastHit2D hit in hits)
		{
			if (Mathf.Approximately(Velocity.magnitude, 0.0f)) continue;
			Velocity += MathHelper.GetNormalForce(Velocity, hit.normal);
            if (hit.collider.GetComponent<VelocityAttribute>())
            {
                Velocity -= previousVelocity;
                Velocity += hit.collider.GetComponent<VelocityAttribute>().Velocity;
                previousVelocity = hit.collider.GetComponent<VelocityAttribute>().Velocity;
            }
		}
	}
	private void UpdateMovement()
	{
		if (_groundNormal.magnitude < MathHelper.FloatEpsilon) return;
		float input = Input.GetAxisRaw("Horizontal");
		if (Mathf.Abs(input) > _controller.InputMagnitudeToMove) Accelerate(input);
		else Decelerate();
	}
	private void Accelerate(float input)
	{
		int direction = MathHelper.Sign(Velocity.x);
		float turnModifier = MathHelper.Sign(input) != direction && direction != 0 ? TurnModifier : 1f;
		Vector2 deltaVelocity = VectorAlongGround * input * Acceleration * turnModifier * Time.deltaTime;
		Vector2 newVelocity = Velocity + deltaVelocity;
		Velocity = newVelocity.magnitude > _controller.MaxSpeed ? VectorAlongGround * MathHelper.Sign(Velocity.x) * _controller.MaxSpeed : newVelocity;
	}
	private void Decelerate()
	{
		Vector2 deltaVelocity = MathHelper.Sign(Velocity.x) * VectorAlongGround * Deceleration * Time.deltaTime;
		Vector2 newVelocity = Velocity - deltaVelocity;
		Velocity = Velocity.magnitude < MathHelper.FloatEpsilon || MathHelper.Sign(newVelocity.x) != MathHelper.Sign(Velocity.x) ? Vector2.zero : newVelocity;
	}

    public void CheckWithEnemy()
    {
        //Incomplete
        _controller.TransitionTo<HurtState>();
    }

	public void CheckVelocity(){
		if ((_controller.Velocity.x > 5 || _controller.Velocity.x < -5) && !_controller.sources[1].isPlaying) {
			_controller.sources[1].clip = _controller.Footsteps;
			_controller.sources [1].loop = true;
			_controller.sources[1].Play ();
		}
		if ((_controller.Velocity.x == 0) && _controller.sources[1].isPlaying) {
			_controller.sources [1].loop = false;
			_controller.sources[1].Stop ();
		}
	}
}