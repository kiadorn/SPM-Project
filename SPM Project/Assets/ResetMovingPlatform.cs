using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMovingPlatform : MonoBehaviour {

	void OnEnable () {
        transform.GetChild(0).GetComponent<MovePlatform>().Reset();
    }
}
