using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformAuto : MonoBehaviour {

    public float moveForwardSpeed;
    public float moveBackSpeed;
    [Range(0, 5)]
    public float waitTime;

    [HideInInspector]
    public bool shouldIMove = false;
    private Vector3 originalPos;
    private bool goingUp = false;
    private float timer;

    private void Start()
    {
        originalPos = transform.position;
    }


    private void Update()
    {
        Move();
    }

    public void Move()
    {

        //Om man anländer till målpunkten.
        if (transform.position == transform.parent.GetChild(1).transform.position)
        {
            goingUp = true;
            //Timer
            timer += Time.deltaTime;
            if (timer < waitTime)
            {
                return;
            }
            timer = 0;
        }
        //Om man anländer till ursprungspunkten.
        else if (transform.position == originalPos)
        {
            goingUp = false;
            //Timer
            timer += Time.deltaTime;
            if (timer < waitTime)
            {
                return;
            }
            timer = 0;
        }

        //Rörelse
        if (!goingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.parent.GetChild(1).transform.position, moveForwardSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPos, moveBackSpeed * Time.deltaTime);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
