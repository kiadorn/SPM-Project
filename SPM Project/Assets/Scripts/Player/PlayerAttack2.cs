using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Följande script måste hängas på på en child till spelaren som innehåller en BoxCollider2D.
public class PlayerAttack2 : MonoBehaviour
{
    public float attackCooldown;
    public float offset;
    private PolygonCollider2D attackArc;
    private PlayerController _controller;
    private List<GameObject> objectsInRange;
    private float attackTimeStamp;
    private float xDir;

    void Start()
    { 
        attackTimeStamp = attackCooldown;
        objectsInRange = new List<GameObject>();
        attackArc = this.GetComponentInChildren<PolygonCollider2D>();
    }

    void Awake()
    {
        //_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {

        UpdateColliderPosition();

        if (attackTimeStamp <= attackCooldown)
        {
            attackTimeStamp += Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && attackTimeStamp >= attackCooldown)
        {
            GetComponentInChildren<Animator>().SetTrigger("SwingTrigger");
            attackTimeStamp = 0;
            PlayerAtk();
        }

    }
    //När spelkaraktären har en sprite och storlek måste 0.16f ändras till något mer passande värde. Nuvarande värde är endast temporärt för testning.
    private void UpdateColliderPosition()
    {
        xDir = Input.GetAxisRaw("Horizontal");

        if (xDir > 0)
        {
            transform.localPosition = new Vector2(offset, 0f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (xDir < 0)
        {
            transform.localPosition = new Vector2((offset * -1), 0f);
            transform.rotation = Quaternion.Euler(0, 0, 180);
            transform.localScale = new Vector3(1, -1, 1);
        }
        else if (xDir == 0)
        {
            return;
        }
    }

    private void PlayerAtk()
    {
        /*Här behövs en ForEach stats som går igenom objectsInRange listan och applicerar skada på varje object som finns i listan. (Kan ej göras för tillfället då jag ej vet hur eller var hälsan på fiender kommer att se ut) /Joakim */

    }
    //Lägger till fiender som är i collidern attackArc.
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && !objectsInRange.Contains(other.gameObject))
        {
            objectsInRange.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && !objectsInRange.Contains(other.gameObject))
        {
            objectsInRange.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
    //Tar bort fiender som försvinner ur collidern attackArc.
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && objectsInRange.Contains(other.gameObject))
        {
            objectsInRange.Remove(other.gameObject);
        }

    }
}
