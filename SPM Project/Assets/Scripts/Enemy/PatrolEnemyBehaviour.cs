using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyBehaviour : MonoBehaviour {

    public float speed;
    public float groundCheckDistance;

    public bool movingRight = true;

    public Transform groundDetection;

    private Vector2 vectorDirection;

    private void Start()
    {
        /*if (movingRight)
        {
            vectorDirection = Vector2.right;
        } else
        {
            vectorDirection = Vector2.left;
        }*/
        vectorDirection = (movingRight == true) ? Vector2.right : Vector2.left;
    }

    void Update () {
        transform.Translate(vectorDirection * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundCheckDistance);
        if (groundInfo.collider == false) {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            } else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

	}
}
