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

	//audio
	[HideInInspector] 
	public AudioSource [] source;
	[Header ("Audio Clips")]
	public AudioClip [] Impact;
	public AudioClip Alerted;
	public AudioClip Death;

	//Stats
	[Header ("Enemy Stats")]
	public float invulnerableTime;
	public int health;

	private bool invulnerable;
	private float time;
	private int currentHealth;
	private Animator animator;

	private void Active(){
		currentHealth = health;
	}

    private void Start()
    {
		//audio
		animator = GetComponent <Animator>();  //Används för animatoner
		source = GetComponents<AudioSource>();
        OGPos = transform.position;
    }

    void Update()
    {
		if (time < invulnerableTime) {
			time += Time.deltaTime;
		}

        UpdateMovement();
    }

    public void BirdAttackPlayer(Vector2 Player)
    {
        if (_canAttack)
        {
            _attacking = true;
            AttackPos = Player;
            
        }
    }

	public void TakeDamage(){
		if (!invulnerable && invulnerableTime >= time) {
			time = 0;
			currentHealth -= 1;
			if(currentHealth <= 0){
				StartCoroutine(OnDeath());
			}
		} else {
			return;
		}
	}

	private IEnumerator OnDeath(){
		animator.SetInteger ("Animation", 2);
		source [0].clip = Death;
		source [0].Play ();
		gameObject.SetActive(false);
		yield return 0;
	}

	public void SwitchInvulnerableState(){
		invulnerable = !invulnerable;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Player")) {
			//audio

			source [1].clip = Impact [Random.Range (0, Impact.Length)];
			source [1].Play ();

			Vector2 dir = collision.transform.position - transform.position;
			collision.transform.GetComponent<PlayerController> ().Velocity = dir.normalized * KnockbackPlayer;
			beingPushed = true;
		}
	}

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
				animator.SetInteger ("Animation", 1);
                if (AttackSpeed < MaxAttackSpeed) AttackSpeed += Time.deltaTime * AttackAcceleration;
                transform.position = Vector3.MoveTowards(transform.position, AttackPos, AttackSpeed * Time.deltaTime);
            }
            else
            {
				animator.SetInteger ("Animation", 0);
                AttackSpeed = 0;
                transform.position = Vector3.MoveTowards(transform.position, OGPos, GoingBackSpeed * Time.deltaTime);
            }
        }
    }
    

}
