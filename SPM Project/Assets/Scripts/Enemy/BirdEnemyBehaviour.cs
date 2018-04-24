using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemyBehaviour : MonoBehaviour {

    //FUNGERAR
    [Header("Knockback")]
    public float KnockbackPlayer;
    public float KnockbackEnemy;
    public float waitTime;
    [SerializeField]
    private bool beingPushed = false;

    [Header("Speed")]
    public float AttackSpeed;
    public float AttackAcceleration;
    public float MaxAttackSpeed;
    public float GoingBackSpeed;

    private float timer;

    [HideInInspector]
    public bool _attacking;
    [HideInInspector]
    public bool _canAttack;
    [Header("Direction")]
    public Vector2 OGPos;
    public Vector2 AttackPos;

    //TEST
    /*public Vector2 Velocity;
    public Vector2 dirToPlayer;
    public Vector2 currentDirection;
    public float Acceleration;
    public bool towardsPlayer = false;
    public Vector2 playerPos;*/



    private void Start()
    {
        OGPos = transform.position;
    }

    void Update()
    {
        //SetDirection();
        UpdateMovement();
        //transform.Translate(Velocity * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 dir = collision.transform.position - transform.position;
            collision.transform.GetComponent<PlayerController>().Velocity = dir.normalized * KnockbackPlayer;
            //transform.position += (Vector3)(dir.normalized * -1 * KnockbackDistance / KnockbackModifier);
            beingPushed = true;
            //dirToPlayer = collision.transform.position - transform.position;
        }
    }

    public void BirdAttackPlayer(Vector2 Player)
    {
        if (_canAttack)
        {
            _attacking = true;
            AttackPos = Player;
            
        }
        //playerPos = Player;
        //towardsPlayer = true;
    }

    /*public void SetDirection()
    {
        if (towardsPlayer)
        {
            currentDirection = playerPos - (Vector2)transform.position;
        } else
        {
            currentDirection = OGPos - (Vector2)transform.position;
        }

    }*/

    private void UpdateMovement()
    {

        if (beingPushed)
        {
            transform.Translate(((Vector2)transform.position - AttackPos).normalized * KnockbackEnemy * Time.deltaTime);
            timer += Time.deltaTime;
            if (timer < waitTime)
            {
                return;
            }
            timer = 0;
            beingPushed = false;
            AttackSpeed = 0;
        }
        else
        {

            if ((Vector2)transform.position == OGPos)
            {
                _canAttack = true;
            }

            if (_attacking)
            {
                if (AttackSpeed < MaxAttackSpeed) AttackSpeed += Time.deltaTime * AttackAcceleration;
                transform.position = Vector3.MoveTowards(transform.position, AttackPos, AttackSpeed * Time.deltaTime);

            }
            else
            {
                AttackSpeed = 0;
                transform.position = Vector3.MoveTowards(transform.position, OGPos, GoingBackSpeed * Time.deltaTime);
            }
        }

        /*Vector2 delta = currentDirection.normalized * Acceleration * Time.deltaTime;
        Debug.Log("currentDirection: " + currentDirection);
        Debug.Log("delta: " + delta);
        if (Mathf.Abs((Velocity + delta).x) < MaxAttackSpeed || (Mathf.Abs(Velocity.x) > MaxAttackSpeed && Vector2.Dot(Velocity.normalized, delta) < 0.0f))
        {
            Velocity += delta;
        }
        else
        {
            Velocity.x = MathHelper.Sign(currentDirection.x) * MaxAttackSpeed;
            //Velocity.y = MathHelper.Sign(currentDirection.y) * MaxAttackSpeed;
        }*/
    }
    

}
