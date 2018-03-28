using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	Transform player;
	PlayerHealth playerHealth;
//	EnemyHealth enemyHealth;
//	NavMeshAgent nav;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <PlayerHealth> ();

		
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHealth.currentHealth <= 0)
			return;
		
	}
}
