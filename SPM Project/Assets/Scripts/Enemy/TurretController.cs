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

	private float time;
	private Animator animator;
	private int currentHealth;
	private AudioSource source;


    void Awake() {
        AI = transform.GetChild(0).GetComponent<ShootingSystem>();
//		animator = GetComponent <Animator>();  //Används för animatoner
    }

    void OnEnable() {
        _shooting = false;
		currentHealth = originalHeath;
        Color c = new Color(1, 1, 1, 1);
        transform.GetChild(1).gameObject.SetActive(true);
        GetComponentInChildren<SpriteRenderer>().color = c;
        transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().color = c;
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
			AI.source [1].clip = AI.Hurt[Random.Range(0, AI.Hurt.Length)];
			AI.source [1].volume = 0.7f;
			AI.source [1].Play ();
//			animator.SetInteger ("VariabelNamn", VariabelVärde); //Används för animatoner, sätt korrekt datatyp och värden för skadeanimation.
			if(currentHealth <= 0){
				StartCoroutine(OnDeath());
			}
		} else {
			return;
		}

	}
	private IEnumerator OnDeath(){
		AI.source[0].clip = AI.Death;
		AI.source [0].Play();
<<<<<<< HEAD
        //GetComponentInChildren<SpriteRenderer> ().enabled = false;
        //transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer> ().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(1).gameObject.SetActive(false);
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            Color c = new Color(1, 1, 1, i);
            GetComponentInChildren<SpriteRenderer>().color = c;
            transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().color = c;
            yield return null;
        }
        //		animator.SetInteger ("VariabelNamn", VariabelVärde); //Används för animatoner, sätt korrekt datatyp och värden för dödsanimaton.
        yield return new WaitForSeconds(1f); // sätt värde till tiden dödsanimaton tar.
        gameObject.SetActive(false);
        yield return 0;
=======
		GetComponentInChildren<MeshRenderer> ().enabled = false;
		transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer> ().enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;
//		animator.SetInteger ("VariabelNamn", VariabelVärde); //Används för animatoner, sätt korrekt datatyp och värden för dödsanimaton.
		yield return new WaitForSeconds(1.4f); // sätt värde till tiden dödsanimaton tar.
		this.gameObject.SetActive(false);
		yield return 0;
>>>>>>> origin/Steven7
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
