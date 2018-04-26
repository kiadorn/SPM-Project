using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/States/Aggressive")]
public class PatrolAggressiveState : State {

    public float aggroRange = 5f;
    private PatrolEnemyController _controller;
    private Vector2 direction;
    public float somethingInfront = 0.1f;

    public override void Initialize(Controller owner)
    {
        _controller = (PatrolEnemyController)owner;
    }

    public override void Enter()
    {
        _controller.speed = 3;
        _controller.GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }

    public override void Update()
    {
        UpdateMovement();
        CheckForPlayer();
    }

    private void UpdateMovement()
    {
        //Vänder sig mot spelaren
        float distance = _controller.player.transform.position.x - _controller.transform.position.x;
        if (distance > 0)
        {
            direction = Vector2.right;
            _controller.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (distance < 0)
        {
            direction = Vector2.left;
            _controller.transform.eulerAngles = new Vector3(0, -180, 0);
        }

        //Raycast framför sig
        RaycastHit2D groundInfoForward = Physics2D.Raycast(_controller.groundDetection.position, direction, somethingInfront);
        //Raycast under sig
        RaycastHit2D groundInfoDown = Physics2D.Raycast(_controller.groundDetection.position, Vector2.down, _controller.groundCheckDistance);
        //Stannar om det är ingenting under
        if (groundInfoDown.collider == false || groundInfoDown.collider.gameObject.layer != 8 && !groundInfoForward.collider.gameObject.CompareTag("Player"))
        {
            _controller.speed = 0;
        }
        else
        {
            //Stannar om det är något framför och det är Geometry
            if (groundInfoForward.collider == true && groundInfoForward.collider.gameObject.layer == 8 && !groundInfoForward.collider.gameObject.CompareTag("Player"))
            {
                _controller.speed = 0;
            }
            //Annars fortsätter
            else
            {

                _controller.speed = 3;
            }
        }

        //Rörelse
        _controller.transform.Translate(Vector2.right * _controller.speed * Time.deltaTime);
    }

    private void CheckForPlayer()
    {
        float checkDistance = Vector3.Distance(_controller.transform.position, _controller.player.transform.position);
        if (checkDistance > aggroRange)
        {
            _controller.TransitionTo<PatrolPassiveState>();
        }
    }
}
