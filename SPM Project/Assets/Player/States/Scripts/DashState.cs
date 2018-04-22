using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Att göra:
Raycast runtomkring spelaren fungerar inte som intended. Dash avbryts nästan direkt om metoden körs.
*/
[CreateAssetMenu(menuName = "Player/States/Dash")]
public class DashState : State{

	public float dashSpeed; //Hastighet dashen är.
	public float dashDistance; //Distans dashen är.
	public float dashTimeTarget; //Tiden spelaren befinner sig i dash state.
	public BoxCollider2D playerCollider;
	private float dashTime;
	private Transform transform { get { return _controller.transform; }}
	private PlayerController _controller;
	private float xDir;
	private float yDir;
	private Vector3 targetPos;
	private Vector2 playerPos;

	public override void Initialize(Controller owner){
		_controller = (PlayerController)owner;
	}

	public override void Enter()
	{
		dashTime = 0;
		xDir = Input.GetAxisRaw ("Horizontal");
		yDir = Input.GetAxisRaw ("Vertical");
		targetPos = new Vector3 (this.transform.position.x + xDir * dashDistance, this.transform.position.y + yDir * dashDistance, 0f);
		_controller.Velocity = new Vector2(0, 0);
	}
	public override void Update(){			
			Dash();
			CheckSurrounding ();
			dashTime += Time.deltaTime;
			if (dashTime >= dashTimeTarget) {
			RaycastHit2D[] hits = _controller.DetectHits ();
			UpdateNormalForce (hits);
			}
	}
	public void Dash (){
		transform.position = Vector3.MoveTowards(this.transform.position, targetPos, dashSpeed*Time.deltaTime);
	}
	//Collision test
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

	private void CheckSurrounding(){
		float angle = 0;
		for (int i=0; i<24; i++) {
			float x = Mathf.Cos (angle);
			float y = Mathf.Sin (angle);
			angle += 15;
			Vector2 dir = new Vector3 (x, y);
			Debug.DrawRay (this.transform.position, dir, Color.red); 				// Debug rad, ta bort när denna metod fungerar som intended.
			if (Physics.Raycast (this.transform.position, dir, 0.1f) != null) {
				RaycastHit2D[] hits = _controller.DetectHits ();
				UpdateNormalForce (hits);
				}
			}
		}
	}