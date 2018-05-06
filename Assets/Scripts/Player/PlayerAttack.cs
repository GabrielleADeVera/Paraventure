using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour {

	public SphereCollider sphereCollider;
	public float AttackCooldown = .7f;
	public float TimeBetweenAttacks = 0.5f;
	public float NextAttack;
	Animator animator;


	int AttackCounter = 0;
	float LastAttack = 0f;
	const int ComboLimit = 2;

	EnemyHealth enemyHealth;


	// Use this for initialization
	void Awake () {
		sphereCollider = GetComponent <SphereCollider>();
		animator = GetComponent<Animator> ();
	}

	// Called once per frame
	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			//If user hits LMB (Attack)
			ComboChain ();
		}

	}

	void ComboChain() {

		if ((Time.time-LastAttack) > TimeBetweenAttacks) {
			//If the user has not attacked in 0.5s time frame, reset AttackChain
			AttackCounter = 0;
		}

		switch (AttackCounter) {

		case 0:
			{
				animator.SetTrigger ("Attack1");//Do attack1
				LastAttack = Time.time;//Set the LastAttack to now
				AttackCounter++;//Increment AttackCounter

				break;
			}
		case 1:
			{
				animator.SetTrigger ("Attack2");//Do attack2
				LastAttack = Time.time;//Set the LastAttack to now
				AttackCounter++;//Increment AttackCounter
//				Debug.Log ("Attack Counter is " + AttackCounter);

				break;

			}
		case 2:
			{
				animator.SetTrigger ("Attack3");//Do attack3
				LastAttack = Time.time;//Set the LastAttack to now
				AttackCounter++;//Increment AttackCounter

				break;
			}
		default:
			{//Reached when AttackCounter is > 2
				AttackCounter = 0;//Reset AttackChain
				break;
			}
		}
	}

	void DealDamage(float damage){
	}
}