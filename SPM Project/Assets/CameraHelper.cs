using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public static class CameraHelper {

    public static void switchToCameraFocus(Vector3 focus, bool freeze)
    {
        if (freeze)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().TransitionTo<PauseWithVelocityState>();
        }
        Camera.main.GetComponent<CameraFocus>().endPos = focus;
        Camera.main.GetComponent<CameraFocus>().enabled = true;
        Camera.main.GetComponent<ProCamera2D>().enabled = false;
    }

    public static void switchToProCamera2D()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().TransitionTo<AirState>();
        Camera.main.GetComponent<CameraFocus>().enabled = false;
        Camera.main.GetComponent<ProCamera2D>().enabled = true;
    }
}
