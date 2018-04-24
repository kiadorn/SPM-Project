using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public Transform playerPosition;
    public float modifier1Y;
    public float modifier1X;
    public float z1;
    public float modifier2Y;
    public float modifier2X;
    public float z2;
    public float modifier3Y;
    public float modifier3X;
    public float z3;

    void Update () {
        transform.GetChild(0).transform.position = new Vector3(playerPosition.position.x * modifier1X/100, playerPosition.position.y * modifier1Y/100, z1);
        transform.GetChild(1).transform.position = new Vector3(playerPosition.position.x * modifier2X/100, playerPosition.position.y * modifier2Y/100, z2);
        transform.GetChild(2).transform.position = new Vector3(playerPosition.position.x * modifier3X/100, playerPosition.position.y * modifier3Y/100, z3);
    }
}
