using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour {

    public bool moveBack;
    public float moveForwardSpeed;
    public float moveBackSpeed;
    [Range(0, 5)]
    public float waitTime;

    [HideInInspector]
    public bool shouldIMove = false;
    private Vector3 originalPos;
    private bool isDone = false;
    private bool isWaiting;

    private void Start()
    {
        originalPos = transform.position;
    }

    public void Move()
    {
        
        if (transform.position == transform.parent.GetChild(1).transform.position)
        {
            isDone = true;
            if (!isWaiting && moveBack)
                StartCoroutine(WaitToMoveBack(waitTime));
        }
        if (!isDone)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.parent.GetChild(1).transform.position, moveForwardSpeed * Time.deltaTime);
        } else if (moveBack)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPos, moveBackSpeed * Time.deltaTime);
        }

        if (transform.position == originalPos)
        {
            shouldIMove = false;
            isDone = false;
        }
    }

    private void Update()
    {
        if (shouldIMove)
        {
            Move();
        }
    }

    private IEnumerator WaitToMoveBack(float time)
    {
        isWaiting = true;
        moveBack = false;
        yield return new WaitForSeconds(time);
        moveBack = true;
        yield return new WaitForSeconds(time);
        isWaiting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
