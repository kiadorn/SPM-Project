using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Dash")]
public class DashState : State{
	public int dashSpeed;

	private Transform transform { get { return _controller.transform; }}
	private PlayerController _controller;
	private float originalGravity;

	public override void Initialize(Controller owner){
		_controller = (PlayerController)owner;
	}

	public override void Enter()
	{
		DisableGravity ();
		Dash();
		EnableGravity ();
		RaycastHit2D[] hits = _controller.DetectHits();
		UpdateNormalForce(hits);
		//behöver check för att se om man dashat in i en vägg/golv
		_controller.TransitionTo<AirState>();
	}
	public void DisableGravity(){
		originalGravity = _controller.Gravity;
		_controller.Gravity = 0;
	}
	public void Dash (){
		float xdirandmag = Input.GetAxisRaw ("Horizontal") * dashSpeed;
		float ydirandmag  = Input.GetAxisRaw ("Vertical") * dashSpeed;
		transform.Translate(new Vector2(xdirandmag, ydirandmag ) * Time.deltaTime);
	}
	public void EnableGravity (){
		_controller.Gravity = originalGravity;
	}
	private void UpdateNormalForce(RaycastHit2D[] hits)
	{
		if (hits.Length == 0) {
			_controller.TransitionTo<AirState> ();
		}
		foreach (RaycastHit2D hit in hits)
		{
			if (MathHelper.CheckAllowedSlope (_controller.SlopeAngles, hit.normal)) {
				_controller.TransitionTo<GroundState> ();
			}
			if (MathHelper.GetWallAngleDelta (hit.normal) < _controller.MaxWallAngleDelta) {
				_controller.TransitionTo<WallState> ();
			}
		}
	}
}