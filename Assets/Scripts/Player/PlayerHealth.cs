using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public const int startingHealth = 100;
	public int currentHealth;

	public Animator playerAnimator;

	bool isDead;
	bool damaged;

//	PlayerMovement playerMovement;

	// Use this for initialization
	void Start () {
//		playerMovement = GetComponent<PlayerMovement> ();
		currentHealth = startingHealth;
		playerAnimator = GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		playerAnimator.SetTrigger ("GetHit");
		if (currentHealth <= 0)
		{
			currentHealth = 0;
//			Debug.Log("Dead!");
		}
	}
}