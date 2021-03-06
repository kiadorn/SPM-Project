﻿using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Wall")]
public class WallState : State
{
	private PlayerController _controller;
	private GameObject pitch;
	public float GlideSpeed;
	public float WallCheckDistance = 0.15f;
	private Vector2 _wallNormal;
	private Vector2 _inputDirection;
	private Vector2 _wallHitPoint;
	private Transform transform { get { return _controller.transform; } }
	[Header("Jumping")]
	public Vector2 WallJumpSpeed;
	public float FallOffSpeed;
	public float InitialJumpDistance;

	private Vector2 Velocity
	{
		get { return _controller.Velocity; }
		set { _controller.Velocity = value; }
	}
	public override void Initialize(Controller owner)
	{
		_controller = (PlayerController)owner;
		pitch = GameObject.Find ("AudioPitchController");
	}
	public override void Update()
	{
		UpdateInput();
		UpdateCollision();
		UpdateVelocity();
		transform.position += (Vector3)Velocity * Time.deltaTime;
		WallJump();
	}

    //Första versionen av Detach WallClimb
    //private void UpdateInput()
    //{
    //    float input = Input.GetAxisRaw("Vertical");
    //    if (input >= -0.3f)
    //    {
    //        _inputDirection = Vector2.zero;
    //    }
    //    else
    //    {

    //        if (Physics2D.Raycast(this.transform.position, Vector2.right, (_controller.transform.GetComponent<BoxCollider2D>().bounds.size.x / 2) + 0.1f))
    //        {
    //            _inputDirection = Vector2.left;
    //        }
    //        else
    //        {
    //            _inputDirection = Vector2.right;
    //        }
    //    }
    //}

	private void UpdateInput()
	{
		
		float inputV = Input.GetAxisRaw("Vertical");
		float inputH = Input.GetAxisRaw("Horizontal");
		if (inputV >= 0f) {
			_inputDirection = Vector2.zero;
		} else {
			if (Physics2D.Raycast(this.transform.position, Vector2.right, 1f) && inputV <= 0.1f && inputH <= 0f) {
				_inputDirection = Vector2.left;
			}else if(inputV <= 0.1f && inputH >= 0f){
				_inputDirection = Vector2.right;
			}
		}
	}
	private void UpdateCollision()
	{
		RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position + (Vector3)
			_controller.Collider.offset, _controller.Collider.size, 0.0f, _inputDirection,
			WallCheckDistance, _controller.CollisionLayers);
		if (hits.Length == 0) { _controller.TransitionTo<AirState>(); return; }
		_controller.SnapToHit(hits[0]);
		_wallNormal = Vector2.zero;
		_wallHitPoint = Vector2.zero;
		foreach (RaycastHit2D hit in hits)
		{
			if (MathHelper.CheckAllowedSlope(_controller.SlopeAngles, hit.normal))
				_controller.TransitionTo<GroundState>();
			if (MathHelper.GetWallAngleDelta(hit.normal) > _controller.MaxWallAngleDelta)
                _controller.TransitionTo<AirState>();
            if (hit.collider.CompareTag("Unclimbable Wall"))
                {
                    _controller.TransitionTo<AirState>();
                }
			_wallNormal += hit.normal;
			_wallHitPoint += hit.point;
		}
		_wallNormal /= hits.Length;
		_wallHitPoint /= hits.Length;
	}
	private void UpdateVelocity()
	{
		float rotation = Vector2.Dot(_wallNormal, Vector2.right) < 0.0 ? 90 : -90;
		Vector2 direction = MathHelper.RotateVector(_wallNormal, rotation);
		Velocity = direction * GlideSpeed;
	}

	private void WallJump()
	{
		Vector2 directionToWall = (_wallHitPoint - (Vector2)transform.position).normalized;
		if (Vector2.Dot (directionToWall, _inputDirection) < 0.0f)
			Jump (new Vector2 (FallOffSpeed, Velocity.y));
		else if (Input.GetButtonDown ("Jump")) {
			Jump(WallJumpSpeed);
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
		}
	}
	private void Jump(Vector2 speed)
	{
		Velocity = new Vector2(MathHelper.Sign(_wallNormal.x) * speed.x, speed.y);
		transform.position += (Vector3)_wallNormal * InitialJumpDistance;
		_controller.TransitionTo<AirState>();
	}

}