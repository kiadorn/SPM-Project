using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class CameraFocus : MonoBehaviour {

    [Header("Timers")]
    public float SmoothingTime;
    public float waitTime;
    [Header("Positions")]
    [ReadOnly] public Vector3 startPos;
    [ReadOnly] public Vector3 endPos;
    private PlayerController Player;
    private Vector3 _currentVelocity;
    private bool waiting = false;
    private bool movingBack = false;
    private float timer;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        startPos = transform.position;
        timer = 0;
        waiting = false;
        movingBack = false;
    }

    private void Update()
    {
        if (waiting && !movingBack)
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                movingBack = true;
                waiting = false;
            }
        } else if (waiting && movingBack)
        {
            switchToCameraFollow();
        }

        if (!waiting)
        {
            if (!movingBack)
            {
                UpdateMovement(endPos);
            } else {
                UpdateMovement(startPos);
            }
        }
    }

    public void switchToCameraFollow()
    {
        Player.TransitionTo<AirState>();
        GetComponent<CameraFocus>().enabled = false;
        GetComponent<ProCamera2D>().enabled = true;
        //this.GetComponent<CameraFollow>().enabled = true;
    }

    private void UpdateMovement(Vector3 targetPos)
    {
        targetPos = new Vector3(targetPos.x, targetPos.y, -1);
        if (Vector3.Distance(transform.position, targetPos) > 1)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _currentVelocity, SmoothingTime);
        }
        else
        {
            waiting = true;
        }
    }




}
