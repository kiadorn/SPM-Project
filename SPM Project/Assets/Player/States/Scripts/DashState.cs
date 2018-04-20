using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Teoretisk alternativ lösning:
Använd transform.position = Vector3.Lerp(spelarposition, slutposition, TidAttDasha) för att förflytta spelaren under dash.  (Se exemplet: https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html)
lerpa från högt värde till lågt och sätt en timer.

Problem som uppstår har förmodligen med att göra att Stat:et gör väldigt lite och förlitar sig för mycket på AirState för rörelse.
*/


[CreateAssetMenu(menuName = "Player/States/Dash")]
public class DashState : State{

	public float dashSpeed; //Hastighet dashen är.
	public float dashDistance; //Distans dashen är.
	public float dashTimeTarget; //Tiden spelaren befinner sig i dash state.
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

	}
	public override void Update(){
			Dash();
			dashTime += Time.deltaTime;
			playerPos = new Vector2 (this.transform.position.x, this.transform.position.y);
		Vector2 lel = new Vector2 (0f,0f);
		Vector2 lel2 = new Vector2 (0.25f, 0.25f);
		if (Physics2D.BoxCast(playerPos, lel2, 0,lel) != null) {
			RaycastHit2D[] hits = _controller.DetectHits ();
			UpdateNormalForce (hits);
		}
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
	}