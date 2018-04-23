using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour {


    public float KnockbackDistance;
    public float AttackSpeed;
    public float BackSpeed;

    [HideInInspector]
    public bool _attacking;
    [HideInInspector]
    public bool _canAttack;
    private Vector2 OGPos;
    public Vector2 AttackPos;

    private void Start()
    {
        OGPos = transform.position;
    }

    void Update()
    {
        UpdateMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 dir = collision.transform.position - transform.position;
            collision.transform.GetComponent<PlayerController>().Velocity = dir.normalized * KnockbackDistance;
            transform.position += (Vector3)(dir.normalized * -1 * KnockbackDistance/10);
        }
    }

    public void BirdAttackPlayer(Vector2 Player)
    {
        if (_canAttack)
        {
            _attacking = true;
            AttackPos = Player;
        }

    }

    private void UpdateMovement()
    {
        if ((Vector2)transform.position == OGPos)
        {
            _canAttack = true;
        }

        if (_attacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, AttackPos, AttackSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, OGPos, BackSpeed * Time.deltaTime);
        }
    }   
}
