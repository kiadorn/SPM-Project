﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTheWildRide : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            TriggerBySpike.Aksjuk = false;
        }
    }
}
