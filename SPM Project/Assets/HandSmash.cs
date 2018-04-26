using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSmash : MonoBehaviour {

    [Header("Sizes")]
    [Range(1, 20)]
    public float handSize;
    [Header("References")]
    public GameObject shadow;
    public GameObject hand;
    public PolygonCollider2D collider;
    public GameObject player;
    [Header("Modifiers")]
    public float shadowModifier;
    public float OpacityModifier;
    public float timeToAttack;
    public float timeToStop;
    public float followSpeed;



    private bool smashing;
    private bool _coolDown = false;
    private float timer = 0f;

    private void Start()
    {
        shadow.SetActive(false);
        hand.SetActive(false);
    }

    private void OnValidate()
    {
        shadow.transform.localScale = new Vector3(handSize, handSize, handSize);
        hand.transform.localScale = new Vector3(handSize, handSize, handSize);
    }

    private void Update()
    {
        if (Input.GetKey("l"))
        {
            smashing = true;
            shadow.SetActive(true);
            hand.SetActive(false);
            shadow.transform.localScale = new Vector3(handSize, handSize, handSize);
            hand.transform.localScale = new Vector3(handSize, handSize, handSize);
            timer = 0;
        } 

        if (smashing && !_coolDown)
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
            
        } else if (timer >= timeToAttack)
        {
            hand.transform.localScale = shadow.transform.localScale;
            hand.SetActive(true);
            shadow.SetActive(false);
            CameraShake.AddIntensity(1);
            smashing = false;
        }
    }

    private void Cooldown()
    {

    }

    

}
