using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyBehaviour : MonoBehaviour {

    public float speed;
    public bool startMovingRight = true;
    public float groundCheckDistance;
    public Transform groundDetection;

    private bool movingRight;
    private float timer = 0f;
    private float saveSpeed;
    public float waitingTime = 2f;

    private void Start()
    {
        movingRight = startMovingRight;
        saveSpeed = speed;
    }

    void Update () {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundCheckDistance);
        if (groundInfo.collider == false)
        {
            if (movingRight)
            {
                speed = 0;
                timer += Time.deltaTime;
                if (timer > waitingTime)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                    timer = 0f;
                    speed = saveSpeed;
                }
            } else
            {
                speed = 0;
                timer += Time.deltaTime;
                if (timer > waitingTime)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                    timer = 0f;
                    speed = saveSpeed;
                }
            }
        }
	}

    private void OnValidate()
    {
        transform.eulerAngles = (startMovingRight == true) ? new Vector3(0, 0, 0) : new Vector3(0, -180, 0);
    }

}
