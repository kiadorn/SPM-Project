using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/States/Dash")]
public class DashState : State{
	public float dashSpeed; //Distans/hastighet dashen är.
	public float dashTimeTarget; //Tiden innan scriptet kollar om spelaren fortfarande är i luften. (Vill ej att Gravity från AirState ska påverka dashen) OBS: Funker ej ännu och är därför för tillfället utkommenterad.
	private float dashTime;

	private Transform transform { get { return _controller.transform; }}
	private PlayerController _controller;
	private float originalGravity; //används vid återställande av gravity

	public override void Initialize(Controller owner){
		_controller = (PlayerController)owner;
	}

	public override void Enter()
	{
	//	dashTime = 0;
		DisableGravity ();
		Dash();
		EnableGravity ();
		RaycastHit2D[] hits = _controller.DetectHits();
		UpdateNormalForce(hits);
	}
/*	public override void Update(){
		dashTime += Time.deltaTime*1;
	}*/

	//Stänger av gravity, så den bör ej påerka distans på dash OBS: Verkar inte göra något. :(
	public void DisableGravity(){
		originalGravity = _controller.Gravity;
		_controller.Gravity = 0;
	}
	//Dash
	public void Dash (){
		_controller.Velocity = new Vector2(0,0);
		float xdirandmag = Input.GetAxisRaw ("Horizontal") * dashSpeed;
		float ydirandmag  = Input.GetAxisRaw ("Vertical") * dashSpeed;
		_controller.Velocity += new Vector2(xdirandmag,ydirandmag);
	}

	//Återställer gravity efter dash.
	public void EnableGravity (){
		_controller.Gravity = originalGravity;
	}

	//Collision test
	private void UpdateNormalForce(RaycastHit2D[] hits)
	{
		if (hits.Length == 0/* && dashTime >= dashTimeTarget*/) {
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