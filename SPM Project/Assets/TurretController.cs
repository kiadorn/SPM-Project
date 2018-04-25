using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

    private ShootingSystem AI;
    private bool _shooting;

    private Vector3 OGPos;


    void Awake() {
        AI = transform.GetChild(0).GetComponent<ShootingSystem>();
        OGPos = transform.position;

    }

    void OnEnable() {
        _shooting = false;
        transform.transform.position = OGPos;
    }

    void Update() {
        if (_shooting && AI.CanShoot) {
            AI.Shoot();
        }
    }

    public void EnteredZone() {
        _shooting = true;
        AI.awake = true;
    }

    public void ExitedZone() {
        _shooting = false;
        AI.awake = false;
    }

    // Use this for initialization
    void Start () {
		
	}
	

}
