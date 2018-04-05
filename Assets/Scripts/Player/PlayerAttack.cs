using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour {

	public SphereCollider sphereCollider;
	public float AttackCooldown = 2f;
	public float NextAttack;


	// Use this for initialization
	void Start () {
		sphereCollider = GetComponent <SphereCollider>();
		
	}
	void OnTriggerStay(Collider other){
		if(other.gameObject.CompareTag("Enemy") && Input.GetMouseButtonDown(0) && Time.time > NextAttack){
			NextAttack = Time.time + AttackCooldown;
			EnemyHealth enemyHealth = other.GetComponent<EnemyHealth> ();
			enemyHealth.currentHealth -= 7;
				Debug.Log ("Enemy Health is " + enemyHealth.currentHealth);

			}
	}
	// Update is called once per frame
	void Update () {

	}
}
