using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Controller {

    [ReadOnly] public PlayerController Player;
    public float angleSpeed;

    public bool done = false;

    [Header("Boss Objects")]
    public GameObject Hand;
    public GameObject BossRoom;

    [Header("Stage Objects")]
    public GameObject[] stage1Objects;
    public GameObject[] stage2Objects;
    public GameObject[] stage4Objects;
    public GameObject turret1;
    public GameObject turret2;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject whiteScreen;

	private void Update() {
        CurrentState.Update();
    }

    private  new void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        base.Awake();
    }

    public void RotateBossRoom(float targetAngle, int direction)
    {
        Quaternion q = Quaternion.Euler(0, 0, targetAngle);
        if (direction >= 1)
        {
            if (BossRoom.transform.rotation.z <= q.z)
            {
                //Debug.Log("Boss: " + BossRoom.transform.rotation.z + " Target: " + q.z);
                BossRoom.transform.Rotate(new Vector3(0, 0, direction * angleSpeed));
            } else
            {
                done = true;
            }
        } else if (direction <= -1)
        {
            if (BossRoom.transform.rotation.z >= q.z)
            {
                //Debug.Log("Boss: " + BossRoom.transform.rotation.z + " Target: " + q.z);
                BossRoom.transform.Rotate(new Vector3(0, 0, direction * angleSpeed));
            } else
            {
                done = true;
            }
        }
    }

}
