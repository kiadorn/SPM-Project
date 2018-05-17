using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/States/BossStage4")]
public class BossStage4 : State {

    private BossController _controller;
    private GameObject hand;
    private GameObject BossHit;

    public float Stage4FollowSpeed;
    public float Stage4AttackCooldown;
    public float Stage4ResetCooldown;
    public float Stage4TimeToAttack;
    public float Stage4TimeToStop;
    public int HandHealth;
    public float Stage4RestTimer;

    private float timer;
    private bool defeatedHand;
    private bool audioPlayed = false;

    public override void Initialize(Controller owner) {
        _controller = (BossController)owner;
        hand = _controller.Hand;
    }

    public override void Enter() {
        timer = 0;
        BossHit = GameObject.Find("BossGetHit");
        hand.SetActive(true);
        hand.GetComponent<HandSmash>().followSpeed = Stage4FollowSpeed;
        hand.GetComponent<HandSmash>().AttackCooldown = Stage4AttackCooldown;
        hand.GetComponent<HandSmash>().resetCooldown = Stage4ResetCooldown;
        hand.GetComponent<HandSmash>().timeToAttack = Stage4TimeToAttack;
        hand.GetComponent<HandSmash>().timeToStop = Stage4TimeToStop;
        hand.GetComponent<HandSmash>().CurrentHealth = HandHealth;
		_controller.turret1.SetActive (true);
		_controller.turret1.GetComponentInChildren<MeshRenderer> ().enabled = true;
		_controller.turret1.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer> ().enabled = true;
		_controller.turret1.GetComponent<BoxCollider2D>().enabled = true;
		_controller.turret2.SetActive (true);
		_controller.turret2.GetComponentInChildren<MeshRenderer> ().enabled = true;
		_controller.turret2.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer> ().enabled = true;
		_controller.turret2.GetComponent<BoxCollider2D>().enabled = true;
    }

    public override void Exit() {
        hand.SetActive(false);
    }

    public override void Update() {
		ShowGoal ();
        if (Input.GetKey("b")) {
            _controller.TransitionTo<BossStage5>();
        }

        CheckHandHealth();
    }

    public void CheckHandHealth()
    {

        if (hand.GetComponent<HandSmash>().CurrentHealth == 0)
        {
            defeatedHand = true;
            hand.SetActive(false);
            timer += Time.deltaTime;
            if (!audioPlayed) BossHit.GetComponent<BossHitSound>().Die();
            audioPlayed = true;
            if (timer > Stage4RestTimer)
            {
                hand.GetComponent<HandSmash>().CurrentHealth = HandHealth;
                hand.SetActive(true);
                audioPlayed = false;
                timer = 0;
            }
        }
    }

	public void ShowGoal(){
		if (defeatedHand && (!_controller.turret1.activeSelf) && (!_controller.turret2.activeSelf)){
			foreach (GameObject g in _controller.stage4GoalObjects) {
				g.SetActive (true);
			}
		}
	}

}
