using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Controller {

    public GameObject Hand;
    public GameObject BossRoom;
    public PlayerController Player;
    public float angleSpeed;

    public bool done = false;

	private void Update() {
        CurrentState.Update();
    }

    public void RotateBossRoom(float targetAngle, int direction)
    {
        Quaternion q = Quaternion.Euler(0, 0, targetAngle);
        if (direction >= 1)
        {
            if (BossRoom.transform.rotation.z <= q.z)
            {
                Debug.Log("Boss: " + BossRoom.transform.rotation.z + " Target: " + q.z);
                BossRoom.transform.Rotate(new Vector3(0, 0, direction * angleSpeed));
            } else
            {
                done = true;
            }
        } else if (direction <= -1)
        {
            if (BossRoom.transform.rotation.z >= q.z)
            {
                Debug.Log("Boss: " + BossRoom.transform.rotation.z + " Target: " + q.z);
                BossRoom.transform.Rotate(new Vector3(0, 0, direction * angleSpeed));
            } else
            {
                done = true;
            }
        }
    }

}
