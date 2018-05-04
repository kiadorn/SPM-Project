using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/States/BossStage4")]
public class BossStage4 : State {

    private BossController _controller;
    private GameObject hand;

    public float Stage4FollowSpeed;
    public float Stage4AttackCooldown;
    public float Stage4ResetCooldown;
    public float Stage4TimeToAttack;
    public float Stage4TimeToStop;

    public override void Initialize(Controller owner) {
        _controller = (BossController)owner;
        hand = _controller.Hand;
    }

    public override void Enter() {
        hand.SetActive(true);
        hand.GetComponent<HandSmash>().followSpeed = Stage4FollowSpeed;
        hand.GetComponent<HandSmash>().AttackCooldown = Stage4AttackCooldown;
        hand.GetComponent<HandSmash>().resetCooldown = Stage4ResetCooldown;
        hand.GetComponent<HandSmash>().timeToAttack = Stage4TimeToAttack;
        hand.GetComponent<HandSmash>().timeToStop = Stage4TimeToStop;
		hand.GetComponent<HandSmash> ().CurrentHealth = 5;
		_controller.turret1.SetActive (true);
		_controller.turret2.SetActive (true);
    }

    public override void Exit() {
        hand.SetActive(false);
    }

    public override void Update() {
		ShowGoal ();
        if (Input.GetKey("b")) {
            _controller.TransitionTo<BossStage5>();
        }
    }

	public void ShowGoal(){
		if (hand.GetComponent<HandSmash>().CurrentHealth == 0 && (!_controller.turret1.activeSelf) && (!_controller.turret2.activeSelf)){
			foreach (GameObject g in _controller.stage4GoalObjects) {
				g.SetActive (true);
			}
		}
	}

}
