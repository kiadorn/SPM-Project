using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGronaLund : MonoBehaviour {

	void OnEnable() {
        transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        TriggerBySpike.Aksjuk = false;
    }
}
