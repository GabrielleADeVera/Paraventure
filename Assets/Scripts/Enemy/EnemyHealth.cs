using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float currentHealth;
	public float startingHealth = 100f;

	public bool inRange; //Each Enemy is able to be in range of the Player

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		
	}


	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			inRange = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.CompareTag ("Player"))
			inRange = false;
	}

}