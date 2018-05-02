﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Goal : MonoBehaviour {

    public BossController BossController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.gameObject.gameObject.gameObject.gameObject.CompareTag("Player"))
        {
            BossController.TransitionTo<BossStage2Intro>();
        }
    }
}
