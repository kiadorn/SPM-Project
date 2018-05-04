using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

	[Header("Stats")]
	public int originalHeath;
	public bool invulnerable;
	public float invulnerableTime;

	private ShootingSystem AI;
    private bool _shooting;

    private Vector3 OGPos;
	private float time;
	private Animator animator;
	private int currentHealth;
	private AudioSource source;


    void Awake() {
        AI = transform.GetChild(0).GetComponent<ShootingSystem>();
        OGPos = transform.position;
//		animator = GetComponent <Animator>();  //Används för animatoner
    }

    void OnEnable() {
        _shooting = false;
        transform.position = OGPos;
		currentHealth = originalHeath;
    }

    void Update() {
		if (time < invulnerableTime) {
			time += Time.deltaTime;
		}
        if (_shooting && AI.CanShoot) {
            AI.Shoot();
        }
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
//		source.PlayOneShot (Death);
//		animator.SetInteger ("VariabelNamn", VariabelVärde); //Används för animatoner, sätt korrekt datatyp och värden för dödsanimaton.
//		yield return WaitForSeconds(0f); // sätt värde till tiden dödsanimaton tar.
		this.gameObject.SetActive(false);
		yield return 0;
	}

	public void SwitchInvulnerableState(){
		invulnerable = !invulnerable;
	}


    public void EnteredZone() {
        _shooting = true;
        AI.awake = true;

        //Audio
        AI.source[0].clip = AI.Alerted;
        AI.source[0].Play();
    }

    public void ExitedZone() {
        _shooting = false;
        AI.awake = false;

        //Audio
		AI.source[0].clip = AI.Retract;
		AI.source[0].Play();
    }

    // Use this for initialization
    void Start () {
		
	}
	

}
