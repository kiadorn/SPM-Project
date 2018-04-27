using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/States/Passive")]
public class PatrolPassiveState : State {

    public float waitingTime = 2f;
    public float aggroRange = 5f;
    public float somethingInfront = 0.1f;
    private bool movingRight;
    private float timer;
    private Vector2 direction;

    private PatrolEnemyController _controller;

    public override void Initialize(Controller owner)
    {
        _controller = (PatrolEnemyController)owner;
        movingRight = _controller.startMovingRight;
    }

    public override void Enter()
    {
        timer = 0f;
        _controller.speed = _controller.saveSpeed;
        _controller.GetComponentInChildren<SpriteRenderer>().color = Color.black;
        _controller.source.clip = _controller.Skitter;
        _controller.source.loop = true;
        _controller.source.Play();
    }

    public override void Update()
    {
        UpdatePatrolMovement();
        CheckForPlayer();
        UpdateAudio();
    }

    private void UpdatePatrolMovement()
    {

        if (movingRight)
        {
            direction = Vector2.right;
        } else
        {
            direction = Vector2.left;
        }

        //Raycast framför sig
        RaycastHit2D groundInfoForward = Physics2D.Raycast(_controller.groundDetection.position, direction, somethingInfront);
        //Raycast under sig
        RaycastHit2D[] groundInfoDownHits = Physics2D.RaycastAll(_controller.groundDetection.position, Vector2.down, _controller.groundCheckDistance);

        bool foundInfront = false;
        bool foundGround = false;

        //Om det är något framför som är Geometry
        if (groundInfoForward.collider == true && groundInfoForward.collider.gameObject.layer == 8)
        {
            foundInfront = true;
        }
        else
        {   
            //Om det är något under som är Geometry
            foreach (RaycastHit2D hit in groundInfoDownHits)
            {
                if (hit.collider == true && hit.collider.gameObject.layer == 8)
                {
                    foundGround = true;
                }
            }
        }

        _controller.speed = _controller.saveSpeed; //"I'm a genius" - Calle 26/04/2018 11:58

        //Väntar ifall den hittade något framför
        if (foundInfront)
        {
            WaitBeforeTurning();
        } else
        {
            //Väntar ifall den INTE hittade något under
            if (!foundGround)
            {

                WaitBeforeTurning();
            }
        }

        //Rörelse
        _controller.transform.Translate(Vector2.right * _controller.speed * Time.deltaTime);
    }

    private void WaitBeforeTurning()
    {
        _controller.speed = 0;

        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            timer = 0f;
            _controller.speed = _controller.saveSpeed;
            if (movingRight)
            {
                movingRight = false;
                _controller.transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else
            {
                movingRight = true;
                _controller.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        } 
    }

    private void UpdateAudio()
    {
        if (_controller.speed > 0 && !_controller.source.isPlaying)
        {
            _controller.source.Play();
        } else if (_controller.speed == 0 && _controller.source.isPlaying)
        {
            _controller.source.Stop();

        }
    }

    private void CheckForPlayer()
    {
        float checkDistance = Vector3.Distance(_controller.transform.position, _controller.player.transform.position);
        if (checkDistance < aggroRange)
        {
			//Audio
			_controller.source.loop = false;
			_controller.source.Stop();

            _controller.TransitionTo<PatrolAggressiveState>();
        }
    }

}
