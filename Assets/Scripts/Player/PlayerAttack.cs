using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour {

	public SphereCollider sphereCollider;
	public MeshCollider meshCollider;
	public float AttackCooldown = .7f;
	public float TimeBetweenAttacks = 0.3f;
	public float NextAttack;
	Animator animator;

	int AttackCounter = 0;
	float LastAttack = 0f;
	const int ComboLimit = 2;

	EnemyHealth enemyHealth;
	public bool isAttacking;

	float timer;


	// Use this for initialization
	void Awake () {
		sphereCollider = GetComponent <SphereCollider>();
		animator = GetComponent<Animator> ();
//		meshCollider = GetComponentInChildren<MeshCollider> ();
	}

	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Enemy")&&isAttacking) {
//			Debug.Log ("Staying in " + other.tag + " trigger");
			collisionTest colTest = other.GetComponent<collisionTest>();
			colTest.ReceiveDamage (1);
		}
	}


	// Called once per frame
	void Update(){
		
		isAttacking = false;
		timer += Time.deltaTime;
		if (Input.GetMouseButtonDown (0)&&timer>=TimeBetweenAttacks&&Time.timeScale!=0) {
			//If user hits LMB (Attack)
			ComboChain ();
		}

	}


	void ComboChain() {

		timer = 0f;

		if ((Time.time-LastAttack) > AttackCooldown) {
			//If the user has not attacked in 0.5s time frame, reset AttackChain
			AttackCounter = 0;
			isAttacking = false;
		}

		switch (AttackCounter) {

		case 0:
			{
				isAttacking = true;
				animator.SetTrigger ("Attack1");//Do attack1
				LastAttack = Time.time;//Set the LastAttack to now
				AttackCounter++;//Increment AttackCounter

				break;
			}
		case 1:
			{
				isAttacking = true;
				animator.SetTrigger ("Attack2");//Do attack2
				LastAttack = Time.time;//Set the LastAttack to now
				AttackCounter++;//Increment AttackCounter
//				Debug.Log ("Attack Counter is " + AttackCounter);

				break;

			}
		case 2:
			{
				isAttacking = true;
				animator.SetTrigger ("Attack3");//Do attack3
				LastAttack = Time.time;//Set the LastAttack to now
				AttackCounter++;//Increment AttackCounter

				break;
			}
		default:
			{//Reached when AttackCounter is > 2
				AttackCounter = 0;//Reset AttackChain
				isAttacking = false;
				break;
			}
		}
	}
		
}