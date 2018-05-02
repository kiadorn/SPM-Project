using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyController : Controller
{

    public Transform player;
    public PlayerStats playerStats;
    [Header("Patrol State")]
    public Transform groundDetection;
    public bool startMovingRight;
    public float groundCheckDistance;
    public float speed;
    [HideInInspector]
    public float saveSpeed;
   
    [HideInInspector]
    public AudioSource source;
    [Header("Audio Clips")]
    public AudioClip Skitter;
    public AudioClip Alerted;
    public AudioClip Death;
	[Header("Stats")]
	public bool invulnerable;
    public int startingHealth;
    public float invulnerableTime;
    private int currentHealth;

    private Vector3 OGPos;
	private float time;
	private Animator animator;

	private void OnEnable() {
        GetComponent<BoxCollider2D>().enabled = true;
        currentHealth = startingHealth;
        transform.position = OGPos;
    }

    new void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        saveSpeed = speed;
//		animator = GetComponent <Animator>();  //Används för animatoner
        source = GetComponent<AudioSource>();
        OGPos = transform.position;
        base.Awake();
    }

    private void Update()
    {
		if (time < invulnerableTime) {
			time += Time.deltaTime;
		}
        CurrentState.Update();
    }

	public void TakeDamage(){
		if (!invulnerable && invulnerableTime >= time) {
			time = 0;
			currentHealth -= 1;
//			animator.SetInteger ("VariabelNamn", VariabelVärde); //Används för animatoner, sätt korrekt datatyp och värden för skadeanimation.
			if(currentHealth <= 0){
				StartCoroutine(OnDeath());
			}
		} else {
			return;
		}

	}
	private IEnumerator OnDeath(){
		source.PlayOneShot (Death);
//		animator.SetInteger ("VariabelNamn", VariabelVärde); //Används för animatoner, sätt korrekt datatyp och värden för dödsanimaton.
//		yield return WaitForSeconds(0f); // sätt värde till tiden dödsanimaton tar.
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
        if (collision.gameObject.CompareTag("Player"))
        {
            playerStats.ChangeHealth(-1);
            
        }
    }
}
