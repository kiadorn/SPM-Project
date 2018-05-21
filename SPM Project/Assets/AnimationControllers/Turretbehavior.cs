using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turretbehavior : MonoBehaviour {
    Animator turretanimator;
    ShootingSystem turret;
	// Use this for initialization
	void Start () {
        turret = transform.parent.gameObject.GetComponent<ShootingSystem>();
        turretanimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        SetAnimation();
	}
    private void SetAnimation()
    {
        if (!turret.awake)
        {
            turretanimator.SetBool("Retract", true);
            turretanimator.SetBool("Active", false);

        }
        if (turret.awake)
        {
            turretanimator.SetBool("Active", true);
            turretanimator.SetBool("Retract", false);

        }
    }
}
