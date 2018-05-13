using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {
	public float deathDistance = 0.5f;
	public float distanceAway;
	public Transform thisObject;
	public Transform target;
	private NavMeshAgent navComponent;
	private bool hasAniComp = false;

	
	public float attackDistance = 0.5f;

	public collisionTest colTest;

	float currentHealth;



	public Animation enemyAnimation;
	public Animator enemyAnimator;

	//Animator parameters
	public float moveSpeed;
	public bool isAttacking;


	// Use this for initialization
	void Start () {
		//print (enemyHealthStatus);
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		navComponent = this.gameObject.GetComponent<NavMeshAgent>();
		colTest = GetComponent<collisionTest> ();

		enemyAnimation = GetComponent<Animation> ();
		enemyAnimator = GetComponent<Animator> ();

		if (null != enemyAnimation && enemyAnimation.enabled == true) {
			hasAniComp = true;
		} else {

			hasAniComp = false;
			enemyAnimator.enabled = true;
		}
	}

	bool CheckAniClip (string clipname)
	{	
		if( this.GetComponent<Animation>().GetClip(clipname) == null ) 
			return false;
		else if ( this.GetComponent<Animation>().GetClip(clipname) != null ) 
			return true;

		return false;
	}
	
	// Update is called once per frame
	void Update () {
		currentHealth = colTest.currentHealth;
		float dist = Vector3.Distance (target.position, transform.position);

		if (dist != 0 && currentHealth > 0) {
//			Debug.Log ("dist != 0");
			enemyAnimator.SetBool ("isWalking", true);
		} else {
			enemyAnimator.SetBool ("isWalking", false);
		}
		if (target && currentHealth > 0) {
			navComponent.SetDestination (target.position);
//			if (enemyAnimation.enabled == true) {
//				enemyAnimation.CrossFade ("walk", 0.4f);
//			}//using animation
//			else {//using Animator
//				Debug.Log("In EnemyMove for Animator");
//				enemyAnimator.SetBool("isWalking", true);
//			}
		} else {
			if (target == null) {
				target = this.gameObject.GetComponent<Transform> ();
			} else {
				target = GameObject.FindGameObjectWithTag("Player").transform;
			}
			if (dist <= deathDistance)
			{
				//kill player
			}//end if dist<=deathDistance
		}

	}


}
