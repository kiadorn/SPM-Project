using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour {

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    public void Awake()
    {
        minX = transform.position.x - GetComponent<BoxCollider2D>().size.x / 2;
        maxX = transform.position.x + GetComponent<BoxCollider2D>().size.x / 2;
        minY = transform.position.y - GetComponent<BoxCollider2D>().size.y / 2;
        maxY = transform.position.y + GetComponent<BoxCollider2D>().size.y / 2;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<CameraFollow>().UpdateBounds(minX, maxX, minY, maxY);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<CameraFollow>().Bound = false;
        }
    }

    public void Update()
    {
        //Debug.Log("minX: " + minX + " maxX: " + maxX + " minY: " + minY + " maxY: " + maxY);
    }

}
