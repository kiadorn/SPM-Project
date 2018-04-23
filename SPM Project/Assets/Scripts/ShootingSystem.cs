using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour {

    public float Distance;
    public float WakeRange;
    public float ShootInterval;
    public float BulletSpeed;
    public float BulletTimer;

    public bool awake = false;

    public GameObject Bullet;
    public Transform Target;
    public Transform ShootingArea;

    public float speed = 5.0f;

    public Vector3 hiddenPos;
    public GameObject AggroPos;
    public GameObject PassivePos;
    public GameObject ShootSpot;
   // Vector3 AgPos;

    public float moveSpeed = 0.2f;
    public bool CanShoot;

    void Update () {
        hiddenPos = PassivePos.transform.position;
        //AgPos = AggroPos.transform.position;
        RangeCheck();
        if(ShootSpot.transform.position != AggroPos.transform.position) {
            CanShoot = false;
        }
        else {
            CanShoot = true;
        }
	}

    void RangeCheck() {
        Distance = Vector3.Distance(transform.position, Target.transform.position);
        if(awake) {
            Vector3 vectorToTarget = Target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
            ShootSpot.transform.position = Vector3.MoveTowards(ShootSpot.transform.position, AggroPos.transform.position, (moveSpeed * Time.deltaTime));
        }
        if (!awake) {
            ShootSpot.transform.position = Vector3.MoveTowards(ShootSpot.transform.position, PassivePos.transform.position, (moveSpeed * Time.deltaTime));
        }
    }

    public void Shoot() {
        BulletTimer += Time.deltaTime;
        
        if (BulletTimer >= ShootInterval) {
            Vector2 dir = Target.transform.position - transform.position;
            dir.Normalize();

            GameObject bulletClone;
            bulletClone = Instantiate(Bullet, ShootingArea.transform.position, ShootingArea.transform.rotation);
            bulletClone.GetComponent<Rigidbody2D>().velocity = dir * BulletSpeed;

            BulletTimer = 0;
        }

    }

    void Start() {
    }
}
