using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour {

    public GameObject Hand;
    public Slider HealthBar;
    private float currentHealth;
    public GameObject bar;
    public float lerpSpeed;
    public float lerpedHealth;
    public float targetHealth;
    public float timer;
    // Use this for initialization
	void Start () {
        currentHealth = 6;
	}
    private void lerpHealth()
    {
        if (HealthBar.value != targetHealth)
        {
            timer += Time.deltaTime;

            lerpedHealth = Mathf.Lerp(currentHealth, targetHealth, timer * lerpSpeed);
        }
        else timer = 0;
        if (HealthBar.value == targetHealth)
        {
            currentHealth = targetHealth;
        }
        
    }
    private float CalculateHealth()
    {
        targetHealth = Hand.gameObject.GetComponent<HandSmash>().CurrentHealth;

        return targetHealth;
    }
	private void HealthUpdate()
    {
        
        HealthBar.value = lerpedHealth;
    }
	// Update is called once per frame
	void Update () {
        enableHealthBar();
        if (Hand.active)
        {
            CalculateHealth();
            lerpHealth();
            HealthUpdate();
        }
        lerpHealth();
        HealthUpdate();


        Debug.Log(HealthBar.value);
	}
    public void enableHealthBar()
    {
        if (Hand.active)
        {
            bar.SetActive(true);
        }
        //else bar.SetActive(false);
        
    }
}
