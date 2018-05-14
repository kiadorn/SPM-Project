using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour {

    public GameObject Hand;
    public Slider HealthBar;
    private int currentHealth;
    public GameObject bar;
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
    }
	// Update is called once per frame
	void Update () {
        HealthUpdate();
        enableHealthBar();
	}
    public void enableHealthBar()
    {
        if (Hand.active == true)
        {
            bar.SetActive(true);
        }
        else bar.SetActive(false);
        
    }
}
