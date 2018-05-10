using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Controller {

    public GameObject focus;
    [ReadOnly] public GameObject Player;
    

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

}
