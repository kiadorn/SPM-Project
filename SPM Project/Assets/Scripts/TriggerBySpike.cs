using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBySpike : MonoBehaviour {

    [ReadOnly] public static bool Aksjuk = false;
    private float _time;
    public float Cooldown;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Hazard")) {
            transform.parent.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
            transform.parent.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            Aksjuk = true;
        }

    }

    public void Update() {
        if (Aksjuk) {
            _time += Time.deltaTime;
            if(_time >= Cooldown) {
                Extreme();
                _time = 0;
            }
        }
    }

    private void Extreme() {
        CameraShake.AddIntensity(20);
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().CurrentState is AirState) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().TransitionTo<HurtState>();
        }

    }
}
