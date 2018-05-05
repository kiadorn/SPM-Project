using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSmash : MonoBehaviour {

    [Header("References")]
    public GameObject shadow;
    public GameObject hand;
    [ReadOnly] public GameObject player;
    [Header("Modifiers")]
    public float shadowModifier;
    public float OpacityModifier;
    public float followSpeed;
    public float cameraShakeModifier = 1f;
    [Range(1, 20)]
    public float handSize;
    [Header("Health")]
    public int StartingHealth;
    public int CurrentHealth;
    [Header("Attack Timer")]
    public float timeToAttack;
    public float timeToStop;
    private float timer = 0f;
    [Header("Attack Cooldown")]
    public float AttackCooldown;
    private bool _canAttack;
    private float _cooldownTimer;
    [Header("Reset Timer")]
    public float resetCooldown;
    private bool _waitingToReset;
    private float resetTimer;
    [Header("First Attack Timer")]
    public float TimeBeforeFirstAttack;

    private bool smashing;

    private void Awake() {
        CurrentHealth = StartingHealth;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        shadow.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        hand.SetActive(false);
        ResetAttack();
        _cooldownTimer -= TimeBeforeFirstAttack;
        transform.position = player.transform.position;
    }

    private void OnValidate()
    {
        shadow.transform.localScale = new Vector3(handSize, handSize, handSize);
        hand.transform.localScale = new Vector3(handSize, handSize, handSize);
    }

    private void Update()
    {
        if (!_canAttack)
        {
            ResetAttack();
            Cooldown();
        }
        else if (_waitingToReset)
        {
            WaitToReset();
        }
        else
        {
            SmashHand();
        }
    }

    private void SmashHand()
    {
        timer += Time.deltaTime;
        if (timer < timeToAttack)
        {
            if (timer < timeToStop)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed);
            }
            shadow.transform.localScale = shadow.transform.localScale * (-(shadowModifier) * Time.deltaTime + 1);
            shadow.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, timer * OpacityModifier);    
        }
        else if (timer >= timeToAttack)
        {
            hand.transform.localScale = shadow.transform.localScale;
            hand.SetActive(true);
            shadow.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            CameraShake.AddIntensity(cameraShakeModifier);
            _waitingToReset = true;
        }
    }

    private void Cooldown()
    {
        _cooldownTimer += Time.deltaTime;
        if(_cooldownTimer > AttackCooldown) {
            _canAttack = true;
            _cooldownTimer = 0;
        }
    }

    private void ResetAttack() {
        hand.SetActive(false);
        shadow.transform.localScale = new Vector3(handSize, handSize, handSize);
        hand.transform.localScale = new Vector3(handSize, handSize, handSize);
        timer = 0;
    }

    private void WaitToReset() {
        resetTimer += Time.deltaTime;
        if(resetTimer > resetCooldown) {
            _waitingToReset = false;
            _canAttack = false;
            resetTimer = 0;
        }
    }


}
