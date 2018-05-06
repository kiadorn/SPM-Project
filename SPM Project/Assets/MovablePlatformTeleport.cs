using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatformTeleport : MonoBehaviour {

    public float moveSpeed;
    public Transform target;
    public Transform bottom;
    [ReadOnly] public GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3 (transform.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        if (transform.position.y == target.position.y)
        {
            if (Player.transform.IsChildOf(transform))
            {
                Player.transform.SetParent(null);
            }
            transform.position = new Vector3(transform.position.x, bottom.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player || collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == Player || collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }


}
