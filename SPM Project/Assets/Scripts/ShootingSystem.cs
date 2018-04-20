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



	void Update () {

        RangeCheck();
	}

    void RangeCheck() {
        Distance = Vector3.Distance(transform.position, Target.transform.position);
        if(Distance < WakeRange) {
            awake = true;
            Vector3 vectorToTarget = Target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        }
        if (Distance > WakeRange) {
            awake = false;
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
}
