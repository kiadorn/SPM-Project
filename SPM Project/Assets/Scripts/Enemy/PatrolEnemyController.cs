using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyController : Controller
{
    [Header("References")]
    [ReadOnly] public Transform player;
    [ReadOnly] public PlayerStats playerStats;
    [Header("Movement")]
    public Transform groundDetection;
    public bool startMovingRight;
    public float groundCheckDistance;
    public float speed;
    [ReadOnly] public float saveSpeed;
    public float Gravity = 30f;
    [Header("Audio Clips")]
    [HideInInspector] public AudioSource[] source;
    public AudioClip Skitter;
    public AudioClip [] Alerted;
	[ReadOnlyAttribute] public AudioClip AlertedLastPlayed;
	public AudioClip [] PlayerCollision;
	[ReadOnlyAttribute] public AudioClip PlayerCollisionLastPlayed;
    public AudioClip [] Death; 
    public float waitBeforeDeath = 3f;
    [Header("Health")]
	public bool invulnerable;
    public int startingHealth;
    public float invulnerableTime;
    private int currentHealth;

    private Vector3 OGPos;
	private float time;
	private Animator animator;

	private void OnEnable() {
        //GetComponentInChildren<SpriteRenderer>().enabled = true; //Gammal, användes innan FadeOut animation
        GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1); ;
        GetComponent<BoxCollider2D>().enabled = true;
        currentHealth = startingHealth;
        transform.position = OGPos;
        transform.SetParent(null);
    }

    new void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerStats = player.GetComponent<PlayerStats>();
        saveSpeed = speed;
        source = GetComponents<AudioSource>();
        OGPos = transform.position;
        base.Awake();
		animator = GetComponentInChildren<Animator> ();
    }

    private void Update()
    {
		if (time < invulnerableTime) {
			time += Time.deltaTime;
		}
        CurrentState.Update();
		if (CurrentState is PatrolAggressiveState) {
			animator.SetBool ("Aggro", true);
		} else {
			animator.SetBool ("Aggro", false);
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
		animator.SetBool ("Death", true);
		source [1].clip = Death [Random.Range (0, Death.Length)];
		source [1].Play ();
        GetComponent<BoxCollider2D>().enabled = false;
        for (float i = 1; i >= 0; i -= 2*Time.deltaTime)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, i);
            if (i < 0.1f)
            {
                GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                break;
            }
            yield return null;
        }
        yield return new WaitForSeconds(waitBeforeDeath); // sätt värde till tiden dödsanimaton tar
		gameObject.SetActive(false);
		yield return 0;
	}

	public void SwitchInvulnerableState(){
		invulnerable = !invulnerable;
	}

    private void OnValidate()
    {
        transform.eulerAngles = (startMovingRight == true) ? new Vector3(0, 0, 0) : new Vector3(0, -180, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
			if (!playerStats._invulnerable) {
				source[1].clip = PlayerCollision [Random.Range (0, PlayerCollision.Length)];
				source[1].Play ();
			}
			playerStats.ChangeHealth(-1);
        }


    }
}
