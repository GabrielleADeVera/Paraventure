using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Events;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	//Components
	Rigidbody rb;
//	protected Animator animator;
	public GameObject target;
	[HideInInspector]
	public Vector3 targetDashDirection;
	public Camera sceneCamera;
	public bool useNavMesh = false;
	private UnityEngine.AI.NavMeshAgent agent;
	private float navMeshSpeed;
	public Transform goal;

	//Jumping Variables
	public float gravity = -9.8f;
	[HideInInspector]
	public bool canJump;
	bool isJumping = false;
	[HideInInspector]
	public bool isGrounded;
	public float jumpSpeed = 12;
	public float doublejumpSpeed = 12;
	bool doublejumping = true;
	[HideInInspector]
	public bool canDoubleJump = false;
	[HideInInspector]
	public bool isDoubleJumping = false;
	bool doublejumped = false;
	bool isFalling;
	bool startFall;
//	float fallingVeloci ty = -1f;


	//isStrafing/action variables
	[HideInInspector]
	public bool canAction = true;
	[HideInInspector]
	public bool isStrafing = false;
	[HideInInspector]
	public bool isDead = false;
	public float knockbackMultiplier = 1f;
	bool isKnockback;


	// Used for continuing momentum while in air
	public float inAirSpeed = 8f;
//	float maxVelocity = 2f;
//	float minVelocity = -2f;

	//rolling variables
	public float rollSpeed = 8;
	bool isRolling = false;
	public float rollduration;

	//movement variables
	[HideInInspector]
	public bool canMove = true;
	public float walkSpeed = 1.35f;
	float moveSpeed;
	public float runSpeed = 6f;
//	float rotationSpeed = 40f;
//	Vector3 inputVec;
	Vector3 newVelocity;

	//inputs variables
	float inputHorizontal = 0f;
	float inputVertical = 0f;
	float inputDashVertical = 0f;
	float inputDashHorizontal = 0f;
//	float inputBlock = 0f;
//	bool inputLightHit;
//	bool inputDeath;
//	bool inputAttackR;
//	bool inputAttackL;
//	bool inputCastL;
//	bool inputCastR;
	bool inputJump;

	void Start(){
		//set the animator component
//		animator = GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody>();
//		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
//		agent.enabled = false;
	}

	void Inputs(){
		inputDashHorizontal = Input.GetAxisRaw("DashHorizontal");
		inputDashVertical = Input.GetAxisRaw("DashVertical");
		inputHorizontal = Input.GetAxisRaw("Horizontal");
		inputVertical = Input.GetAxisRaw("Vertical");
//		inputLightHit = Input.GetButtonDown("LightHit");
//		inputDeath = Input.GetButtonDown("Death");
//		inputAttackL = Input.GetButtonDown("AttackL");
//		inputAttackR = Input.GetButtonDown("AttackR");
//		inputCastL = Input.GetButtonDown("CastL");
//		inputCastR = Input.GetButtonDown("CastR");
//		inputBlock = Input.GetAxisRaw("TargetBlock");
		inputJump = Input.GetButtonDown("Jump");

	}

	void Update(){
		//make sure there is animator on character
//		if(animator){
			Inputs();
			if(canMove && !isDead && !useNavMesh){
				CameraRelativeMovement();
//			} 
//			Rolling();
			Jumping();
//			if(inputLightHit && canAction && isGrounded){
//				GetHit();
//			}
//			if(inputDeath && canAction && isGrounded){
//				if(!isDead){
//					StartCoroutine(_Death());
//				}
//				else{
//					StartCoroutine(_Revive());
//				}
//			}
//			if(inputAttackL && canAction && isGrounded){
//				Attack(1);
//			}
//			if(inputAttackR && canAction && isGrounded){
//				Attack(2);
//			}
//			if(inputCastL && canAction && isGrounded && !isStrafing){
//				AttackKick(1);
//			}
//			if(inputCastR && canAction && isGrounded && !isStrafing){
//				AttackKick(2);
//			}
//			//if strafing
//			if((Input.GetKey(KeyCode.LeftShift) || inputBlock > 0.1f) && canAction){  
//				isStrafing = true;
//				animator.SetBool("Strafing", true);
//				if(inputCastL && canAction && isGrounded){
//					CastAttack(1);
//				}
//				if(inputCastR && canAction && isGrounded){
//					CastAttack(2);
//				}
//			}
//			else{
//				isStrafing = false;
//				animator.SetBool("Strafing", false);
//			}
//			//Navmesh
//			if(Input.GetMouseButtonDown(0))
//			{
//				if(useNavMesh)
//				{
//					RaycastHit hit;
//					if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
//						agent.destination = hit.point;
//					}
//				}
//			}
//		}
//		else{
//			Debug.Log("ERROR: There is no animator for character.");
//		}
//		if(useNavMesh){
//			agent.enabled = true;
//			navMeshSpeed = agent.velocity.magnitude;
//		}
//		else{
//			agent.enabled = false;
//		}
//		//Slow time
//		if(Input.GetKeyDown(KeyCode.T)){
//			if(Time.timeScale != 1){
//				Time.timeScale = 1;
//			}
//			else{
//				Time.timeScale = 0.15f;
//			}
//		}
//		//Pause
//		if(Input.GetKeyDown(KeyCode.P)){
//			if(Time.timeScale != 1){
//				Time.timeScale = 1;
//			}
//			else{
//				Time.timeScale = 0f;
//			}
		}
	}

	void CameraRelativeMovement(){
		//converts control input vectors into camera facing vectors
		Transform cameraTransform = sceneCamera.transform;
		//Forward vector relative to the camera along the x-z plane   
		Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;
		//Right vector relative to the camera always orthogonal to the forward vector
		Vector3 right = new Vector3(forward.z, 0, -forward.x);
		if(!isRolling){
			targetDashDirection = inputDashHorizontal * right + inputDashVertical * -forward;
		}
//		inputVec = inputHorizontal * right + inputVertical * forward;
	}

//	void Rolling(){
//		if(!isRolling && isGrounded){
//			if(Input.GetAxis("DashVertical") > .5 || Input.GetAxis("DashVertical") < -.5 || Input.GetAxis("DashHorizontal") > .5 || Input.GetAxis("DashHorizontal") < -.5){
//				StartCoroutine(_DirectionalRoll(Input.GetAxis("DashVertical"), Input.GetAxis("DashHorizontal")));
//			}
//		}
//	}
//
	void Jumping(){
		if(isGrounded){
			if(canJump && inputJump){
				StartCoroutine(_Jump());
			}
		}
		else{    
			canDoubleJump = true;
			canJump = false;
			if(isFalling){
				//set the animation back to falling
//				animator.SetInteger("Jumping", 2);
				//prevent from going into land animation while in air
				if(!startFall){
//					animator.SetTrigger("JumpTrigger");
					startFall = true;
				}
			}
			if(canDoubleJump && doublejumping && inputJump && !doublejumped && isFalling){
				// Apply the current movement to launch velocity
				rb.velocity += doublejumpSpeed * Vector3.up;
//				animator.SetInteger("Jumping", 3);
				doublejumped = true;
			}
		}
	}


	public IEnumerator _Jump(){
		isJumping = true;
//		animator.SetInteger("Jumping", 1);
//		animator.SetTrigger("JumpTrigger");
		// Apply the current movement to launch velocity
		rb.velocity += jumpSpeed * Vector3.up;
		canJump = false;
		yield return new WaitForSeconds(.5f);
		isJumping = false;
	}

	public IEnumerator _DirectionalRoll(float x, float v){
		//check which way the dash is pressed relative to the character facing
		float angle = Vector3.Angle(targetDashDirection, -transform.forward);
		float sign = Mathf.Sign(Vector3.Dot(transform.up, Vector3.Cross(targetDashDirection, transform.forward)));
		// angle in [-179,180]
		float signed_angle = angle * sign;
		//angle in 0-360
		float angle360 = (signed_angle + 180) % 360;
		//deternime the animation to play based on the angle
		if(angle360 > 315 || angle360 < 45){
			StartCoroutine(_Roll(1));
		}
		if(angle360 > 45 && angle360 < 135){
			StartCoroutine(_Roll(2));
		}
		if(angle360 > 135 && angle360 < 225){
			StartCoroutine(_Roll(3));
		}
		if(angle360 > 225 && angle360 < 315){
			StartCoroutine(_Roll(4));
		}
		yield return null;
	}

	public IEnumerator _Roll(int rollNumber){
		if(rollNumber == 1){
//			animator.SetTrigger("RollForwardTrigger");
		}
		if(rollNumber == 2){
//			animator.SetTrigger("RollRightTrigger");
		}
		if(rollNumber == 3){
//			animator.SetTrigger("RollBackwardTrigger");
		}
		if(rollNumber == 4){
//			animator.SetTrigger("RollLeftTrigger");
		}
		isRolling = true;
		yield return new WaitForSeconds(rollduration);
		isRolling = false;
	}

}