using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Event/HitEvent")]
public class HitEvent : ScriptableObject {

    public void Action()
    {
        Debug.Log("EUREKA");
    }
}
