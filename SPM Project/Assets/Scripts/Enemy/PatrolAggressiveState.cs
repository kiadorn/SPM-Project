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
		_controller.source[1].clip = _controller.Alerted;
		_controller.source[1].Play();
        _controller.speed = _controller.saveSpeed;
        Color c = _controller.GetComponentInChildren<SpriteRenderer>().color;
        _controller.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 0, 0, c.a);
    }

    public override void Update()
    {
        CheckForPlayer();
        UpdateHorizontalMovement();
        UpdateGravity();
        UpdateAudio();
    }

    private void CheckForPlayer()
    {
        float checkDistance = Vector3.Distance(_controller.transform.position, _controller.player.transform.position);
        if (checkDistance > aggroRange)
        {
            _controller.TransitionTo<PatrolPassiveState>();
        }
    }

    private void UpdateHorizontalMovement()
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
        RaycastHit2D[] groundInfoDownHits = Physics2D.RaycastAll(_controller.groundDetection.position, Vector2.down, _controller.groundCheckDistance);

        bool foundInfront = false;
        bool foundGround = false;

        //Om det finns Geometry framför
        if (groundInfoForward.collider == true && groundInfoForward.collider.gameObject.layer == 8)
        {
            foundInfront = true;
        }
        else
        {
            //Om det finns Geometry under
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
            _controller.speed = 0;
        }
        else
        {
            //Väntar ifall den INTE hittade något under
            if (!foundGround)
            {
                _controller.speed = 0;
            }
        }

        //Rörelse
        _controller.transform.Translate(Vector2.right * _controller.speed * Time.deltaTime);

    }

    private void UpdateGravity()
    {
        //Raycast under sig
        RaycastHit2D[] groundInfoDownHits = Physics2D.RaycastAll(_controller.transform.position, Vector2.down, 0.1f + _controller.gameObject.GetComponent<BoxCollider2D>().size.y);

        bool foundGround = false;

        //Om det finns Geometry under en
        foreach (RaycastHit2D hit in groundInfoDownHits)
        {
            if (hit.collider == true && hit.collider.gameObject.layer == 8)
            {
                foundGround = true;
            }
        }

        if (!foundGround)
        {
            _controller.transform.Translate(Vector2.down * _controller.Gravity * Time.deltaTime);
        }
    }

    private void UpdateAudio()
    {
        //Audio. Byt till två audiosources
        _controller.source[0].clip = _controller.Skitter;
        _controller.source[0].loop = true;
        if (_controller.speed > 0 && !_controller.source[0].isPlaying)
        {
            _controller.source[0].Play();
        }
        else if (_controller.speed == 0 && _controller.source[0].isPlaying)
        {
            _controller.source[0].Stop();
        }
    }
}
