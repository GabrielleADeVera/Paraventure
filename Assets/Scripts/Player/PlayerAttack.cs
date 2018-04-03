using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	public SphereCollider sphereCollider;


	// Use this for initialization
	void Start () {
		sphereCollider = GetComponent <SphereCollider>();
		
	}
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Enemy")){
			
			EnemyHealth enemyHealth = other.GetComponent<EnemyHealth> ();
			enemyHealth.currentHealth -= 7;
			Debug.Log ("Enemy Health is " + enemyHealth.currentHealth);

			}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
