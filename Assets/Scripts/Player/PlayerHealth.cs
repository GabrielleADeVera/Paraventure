using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 10;
	public int currentHealth;

	public Animator playerAnimator;

	public bool isDead;
	bool damaged;

//	PlayerMovement playerMovement;

	AudioSource playerAudio;

	public AudioClip deathClip;

	// Use this for initialization
	void Start () {
//		playerMovement = GetComponent<PlayerMovement> ();
		currentHealth = startingHealth;
		playerAnimator = GetComponent<Animator> ();
		playerAudio = GetComponent<AudioSource> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator example(){
		yield return new WaitForSeconds(playerAudio.clip.length);

	}

	void Death(){
		playerAnimator.SetBool ("isDead", isDead);
		playerAudio.clip = deathClip;
		playerAudio.Play ();
		StartCoroutine (example ());

		Application.LoadLevel(1);

	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		playerAnimator.SetTrigger ("GetHit");
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			isDead = true;

			if (isDead) {
				Death ();
			}
//			Debug.Log("Dead!");
		}
	}
}