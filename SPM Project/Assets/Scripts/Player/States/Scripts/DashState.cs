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
	private float dashDistanceIncrement;
	private float angle;
	RaycastHit2D[] hits;
	private bool stopDash;
	public override void Initialize(Controller owner){
		_controller = (PlayerController)owner;
	}

	public override void Enter()
	{
		stopDash = true;
		angle = 0;
		dashTime = 0;
		xDir = Input.GetAxisRaw ("Horizontal");
		yDir = Input.GetAxisRaw ("Vertical");

	}
	public override void Update(){
		if (xDir != 0) {
			targetPos = new Vector3 (this.transform.position.x + xDir * dashDistanceIncrement, this.transform.position.y + yDir * dashDistanceIncrement, 0f);
		} else {
			xDir = _controller.GetLastXDirection ();
			targetPos = new Vector3 (this.transform.position.x + xDir * dashDistanceIncrement, this.transform.position.y + yDir * dashDistanceIncrement, 0f);
		}
			dashTime += Time.deltaTime;
			CheckSurrounding ();
			Dash ();
	}
	public void Dash (){
		if (stopDash) {
			transform.position = Vector3.MoveTowards (this.transform.position, targetPos, dashSpeed * Time.deltaTime);
		}
		CheckSurrounding ();
		dashDistanceIncrement++;
	}

	//Collision tests
	private void CheckSurrounding(){
		for (int i = 0; i < 6; i++) {
			float x = Mathf.Cos (angle);
			float y = Mathf.Sin (angle);
			angle += 45;
			Vector2 dir = new Vector3 (x, y);
			hits = Physics2D.RaycastAll (this.transform.position, dir, 1f);
			if (hits.Length >= 2) {
				if (hits != null && hits [1].collider != null && hits [1].transform.tag == "Geometry") {
					stopDash = false;
					if (hits [1].transform.rotation.z > 45) {  //Eventuellt problem här, kan behövas bättre uträkning istället för att endast kolla vinkeln.
						_controller.SnapToHit(hits[1]);
						_controller.TransitionTo<WallState> ();
					} else if (hits [1].transform.rotation.z < 45) {
						_controller.SnapToHit(hits[1]);
						_controller.TransitionTo<WallState> ();
					}
				}
			} else if(dashTime >= dashTimeTarget) {
				stopDash = false;
				_controller.TransitionTo<AirState> ();
			}
		}
	}
	}