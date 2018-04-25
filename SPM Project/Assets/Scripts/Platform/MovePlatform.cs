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
    private bool SaveMove = true;

    private void Awake()
    {
        originalPos = transform.localPosition;
        SaveMove = moveBack;
    }

    public void Move()
    {
        
        if (transform.localPosition == transform.parent.GetChild(1).transform.localPosition)
        {
            isDone = true;
            if (!isWaiting && moveBack)
                StartCoroutine(WaitToMoveBack(waitTime));
        }
        if (!isDone)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.parent.GetChild(1).transform.localPosition, moveForwardSpeed * Time.deltaTime);
        } else if (moveBack)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, originalPos, moveBackSpeed * Time.deltaTime);
        }

        if (transform.localPosition == originalPos)
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

    public void Reset() {
        isDone = false;
        shouldIMove = false;
        transform.localPosition = originalPos;
        isWaiting = false;
        moveBack = SaveMove;

    }

}
