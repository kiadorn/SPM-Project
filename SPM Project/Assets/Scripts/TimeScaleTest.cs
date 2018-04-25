using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleTest : MonoBehaviour {


	void Update () {
        {
            if (Input.GetKeyDown("t"))
            {
                Time.timeScale -= 0.1f;
                Debug.Log("Timescale: " + Time.timeScale);
            } else if (Input.GetKeyDown("g"))
            {
                Time.timeScale += 0.1f;
                Debug.Log("Timescale: " + Time.timeScale);
            }
        }
	}
}
