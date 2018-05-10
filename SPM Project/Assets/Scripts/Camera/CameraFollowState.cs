using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowState : State {

    private CameraController _controller;

    public override void Initialize(Controller owner)
    {
        _controller = (CameraController)owner;
    }

    public override void Update()
    {
        
    }

    private void UpdateMovement()
    {
        //_controller.transform.position += new Vector3(0, Target.transform.position.y - transform.position.y, 0);
        //Vector3 moveToPos = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
        //transform.position = Vector3.MoveTowards(transform.position, moveToPos, 50 * Time.deltaTime);
        //_controller.transform.position = Vector3.SmoothDamp(_controller.transform.position, _targetPosition, ref _currentVelocity, SmoothingTime);
    }
}
