using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public float startingHealth = 100;
	public float currentHealth;

	bool isDead;
	bool damaged;

	PlayerMovement playerMovement;

	// Use this for initialization
	void Start () {
		playerMovement = GetComponent<PlayerMovement> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
