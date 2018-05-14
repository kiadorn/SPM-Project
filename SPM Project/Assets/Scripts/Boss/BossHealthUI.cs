using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour {

    public GameObject Hand;
    public Slider HealthBar;
    public int currentHealth;
    // Use this for initialization
	void Start () {
	}
    private float CalculateHealth()
    {
        currentHealth = Hand.gameObject.GetComponent<HandSmash>().CurrentHealth;
        return currentHealth;
    }
	private void HealthUpdate()
    {
        HealthBar.value = CalculateHealth();
        Debug.Log(currentHealth);
    }
	// Update is called once per frame
	void Update () {
        HealthUpdate();
	}
}
