using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuSelect : MonoBehaviour {

    public EventSystem ES;
    public GameObject FirstSelected;
    private GameObject _storeSelected;

    public void Awake() {
        ES = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        _storeSelected = FirstSelected;
    }

	// Use this for initialization
	void OnEnable () {
        ES.SetSelectedGameObject(null);
        _storeSelected = FirstSelected;
        //if (ES.currentSelectedGameObject != FirstSelected) {
        //    ES.SetSelectedGameObject(FirstSelected);
        //}
       // ES.SetSelectedGameObject(FirstSelected);
    }

    void OnDisable() {
        _storeSelected = null;

    }
	// Update is called once per frame
	void Update () {
		if(ES.currentSelectedGameObject != _storeSelected) {
            if(ES.currentSelectedGameObject == null) {
                ES.SetSelectedGameObject(_storeSelected);
            }
            else {
                _storeSelected = ES.currentSelectedGameObject;
            }
        }
        ES.SetSelectedGameObject(_storeSelected);
    }
}
