using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityAttribute : MonoBehaviour {

    public Vector2 Velocity;
    private Vector3 _lastPosition;
    private void Update()
    {
        Velocity = (transform.position - _lastPosition) / Time.deltaTime;
        _lastPosition = transform.position;
    }
}
