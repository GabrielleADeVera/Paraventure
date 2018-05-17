using UnityEngine;
using System.Collections;

public class collisionTest : MonoBehaviour {

	//for health
	public int maxHealth=50;
	public int currentHealth;
	public int damage=10;
	private bool hasAniComp = false;

	public bool isInRange;
	public bool isDead;

	float timer;
	float AttackCooldown = .7f;
	float TimeBetweenAttacks = 3f;
	float LastAttack = 0f;


	public Animation enemyAnimation;
	public Animator enemyAnimator;

	//Animator parameters
	public float moveSpeed;
	bool isAttacking;

	public int attackDamage = 10;

	GameObject player;
	bool playerDied = false;

	AudioSource enemyAudio;

	void Update(){
		timer += Time.deltaTime;
		if (player.GetComponent<PlayerHealth> ().isDead) {
			playerDied = true;
		}
	}


	void Awake(){
		currentHealth = maxHealth;
		isDead = false;

	}

	// Use this for initialization
	void Start ()
	{		
		enemyAnimation = GetComponent<Animation> ();
		enemyAnimator = GetComponent<Animator> ();
		enemyAudio = GetComponent<AudioSource> ();
		if (null != enemyAnimation && enemyAnimation.enabled == true) {
			hasAniComp = true;
		} else {
		
			hasAniComp = false;
		}
		player = GameObject.FindGameObjectWithTag("Player");
	



//		Debug.Log ("Enemy is starting with health " + currentHealth);


	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			isInRange = false;
		}
	}

	void OnTriggerStay(Collider other){
		if (timer>=TimeBetweenAttacks&&Time.timeScale!=0&&other.gameObject.tag == "Player"&& currentHealth > 0&&!playerDied) {
			DealDamage (attackDamage);
			PlayerHealth playerHealth = other.GetComponent<PlayerHealth> ();
			playerHealth.TakeDamage (attackDamage);
		}
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Player" && currentHealth > 0) 
		{
			isInRange = true;
			if (timer>=TimeBetweenAttacks&&Time.timeScale!=0) {
				DealDamage(attackDamage);
				PlayerHealth playerHealth = other.GetComponent<PlayerHealth> ();
				playerHealth.TakeDamage (attackDamage);

			}


		}//end other.gameObject.tag=="Player"
		
		if (hasAniComp == true) {
//			if (currentHealth == 0) {	
//				if (CheckAniClip ("dead") == false)
//					return;
//
//				GetComponent<Animation> ().CrossFade ("dead", 0.1f);
//				this.gameObject.SetActive (false);
////				print ("The enemy is now dead!");
//			}//end enemy health==0	
		}//end hasAniComp == true
	}

	void DealDamage(int amount){
		timer = 0f;
//		Debug.Log ("Dealing Damage");
		if((Time.time-LastAttack) > AttackCooldown){
			if (hasAniComp == true) {
				if (CheckAniClip ("attack03") == false)
					return;

				enemyAnimation.Blend ("attack03", 1.0f, 0.1f);
				LastAttack = Time.time;//Set the LastAttack to now

			}//end hasAniCompif
			else {//Using Animator
				//				Debug.Log("Else using Animator");
				enemyAnimator.SetTrigger("isAttacking");
				LastAttack = Time.time;//Set the LastAttack to now

			}
		}

	}
		
	//checks to see if the clip exists
	bool CheckAniClip (string clipname)
	{	
		if( enemyAnimation.GetClip(clipname) == null ) 
			return false;
		else if ( enemyAnimation.GetClip(clipname) != null ) 
			return true;

		return false;
	}

	public void ReceiveDamage(int damage){
	//use this function to deal damage to the enemy
		currentHealth -= damage;
//		Debug.Log ("Enemy health is currently " + currentHealth);
	
	
		if (currentHealth <= 0) {
//			Debug.Log ("Enemy health is currently " + currentHealth);

			Death ();
		} 
//		else if (enemyAnimation.enabled == true) {
//			GetComponent<Animation> ().CrossFade ("damage", 0.1f);
//		} 

			enemyAnimator.SetTrigger ("isDamaged");
		
	}
		
	void Death(){
		Debug.Log ("Enemy is dying");
		isDead = true;
		enemyAnimator.Play ("dead");
		enemyAudio.Play ();
//
//		if (currentHealth <= 0) {	
//			if (enemyAnimation != null) {
//				enemyAnimation.Stop (); 
//				enemyAnimation.Play ("dead");
//			} else {
//				
//			}
////			this.gameObject.SetActive (false);
//			//				print ("The enemy is now dead!");
//		}//end enemy health==0	

//		enemyAudio.clip = deathClip;
//		enemyAudio.Play ();

		Destroy (gameObject, 3f);
	}

}