using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/States/Passive")]
public class PatrolPassiveState : State {

    private bool movingRight;
    private float timer = 0f;
    private float saveSpeed;
    public float waitingTime = 2f;
    public float aggroRange = 5f;

    private PatrolEnemyController _controller;

    public override void Initialize(Controller owner)
    {
        _controller = (PatrolEnemyController)owner;
        movingRight = _controller.startMovingRight;
        saveSpeed = _controller.speed;
    }

    public override void Enter()
    {
        _controller.speed = 3f;
    }

    public override void Update()
    {
        UpdatePatrolMovement();
        CheckForPlayer();
    }

    private void UpdatePatrolMovement()
    {
        _controller.transform.Translate(Vector2.right * _controller.speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(_controller.groundDetection.position, Vector2.down, _controller.groundCheckDistance);
        if (groundInfo.collider == false || groundInfo.collider.gameObject.layer != 8)
        {
            if (movingRight)
            {

                _controller.speed = 0;
                timer += Time.deltaTime;
                if (timer > waitingTime)
                {
                    _controller.transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                    timer = 0f;
                    _controller.speed = saveSpeed;
                }
            }
            else
            {
                _controller.speed = 0;
                timer += Time.deltaTime;

                if (timer > waitingTime)
                {
                    _controller.transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                    timer = 0f;
                    _controller.speed = saveSpeed;
                }
            }
        }
    }

    private void CheckForPlayer()
    {
        float checkDistance = Vector3.Distance(_controller.transform.position, _controller.player.transform.position);
        if (checkDistance < aggroRange)
        {
            _controller.TransitionTo<PatrolAggressiveState>();
        }
    }

}
