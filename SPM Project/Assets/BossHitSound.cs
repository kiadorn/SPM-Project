using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitSound : MonoBehaviour {

	private AudioSource[] source;
	[Header("Audio")]
	public AudioClip[] TakeDamageSound;
    [ReadOnly] public AudioClip TakeDamageSoundLastPlayed;
	public AudioClip[] DealDamageSound;
    [ReadOnly] public AudioClip DealDamageSoundLastPlayed;
    public AudioClip[] DeathSound;


	public void Start () {
		source = GetComponents<AudioSource> ();
	}

	public void TakeDamage(){
        int length = TakeDamageSound.Length;
        int replace = Random.Range(0, (length - 1));
        source[0].clip = TakeDamageSound [replace];
		source[0].Play ();
        TakeDamageSoundLastPlayed = TakeDamageSound[replace];
        TakeDamageSound[replace] = TakeDamageSound[length - 1];
        TakeDamageSound[length - 1] = TakeDamageSoundLastPlayed;
    }

	public void DealDamage(){
        int length = DealDamageSound.Length;
        int replace = Random.Range(0, (length - 1));
        source[0].clip = DealDamageSound[replace];
        source[0].Play();
        DealDamageSoundLastPlayed = DealDamageSound[replace];
        DealDamageSound[replace] = DealDamageSound[length - 1];
        DealDamageSound[length - 1] = DealDamageSoundLastPlayed;
    }

    public void Die()
    {
        int length = DeathSound.Length;
        int replace = Random.Range(0, (length - 1));
        source[1].clip = DeathSound[replace];
        source[1].Play();
    }
}
