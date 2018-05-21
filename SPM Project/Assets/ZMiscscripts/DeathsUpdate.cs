using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathsUpdate : MonoBehaviour {

	void OnEnable() {
        if(GameManager.instance != null) 
            GetComponent<Text>().text = GameManager.instance.GetDeathCounter().ToString();


    }
}
