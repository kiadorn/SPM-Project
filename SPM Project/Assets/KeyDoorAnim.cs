using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorAnim : MonoBehaviour {

    public float AnimationSpeed;

    private GameObject _obtain;
    private Vector3 _oGpos;
    private GameObject _targetPos;
    private bool _opening = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerStats>().hasKey) {
            _obtain.GetComponent<ObjectObtain>().OpenDoor();
            collision.gameObject.GetComponent<PlayerStats>().ChangeKeyStatus(false);
            //this.gameObject.SetActive(false);
            //GetComponent<BoxCollider2D>().enabled = false;
            _opening = true;
        }
    }

    void Awake() {
        _obtain = GameObject.Find("ObjectObtain");
        _oGpos = transform.localPosition;
        _targetPos = transform.parent.GetChild(1).gameObject;
    }

    private void Update() {
        if (_opening) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _targetPos.transform.localPosition, AnimationSpeed * Time.deltaTime);
            if (transform.localPosition == _targetPos.transform.localPosition) {
                _opening = false;
            }
        }
    }

    void OnEnable() {
        _opening = false;
        transform.localPosition = _oGpos;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
