﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Goal : MonoBehaviour {

    public BossController BossController;

    private void Awake() {
        BossController = GameObject.Find("BossControl").GetComponent<BossController>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerStats>().hasKey)
        {
            BossController.TransitionTo<BossStage5>();
        }
    }

}
