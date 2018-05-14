using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC2 : MonoBehaviour {

	public Transform target;

	[System.Serializable]
	public class PositionSettings{
		
		public Vector3 targetPosOffset = new Vector3(0, 5f, -10);
		public float lookSmooth = 100f;
		public float distanceFromTarget = -8;
		public float zoomSmooth = 20;
		public float maxZoom = -2;
		public float minZoom = -15;
	}

	[System.Serializable]
	public class OrbitSettings{
		public float xRotation = 12;
		public float yRotation = -180;
		public float maxXRotation = 25;
		public float minXRotation = -85;
		public float vOrbitSmooth = 150;
		public float hOrbitSmooth = 150;
	}

	[System.Serializable]
	public class InputSettings{
		public string ORBIT_HORIZONTAL_SNAP = "OrbitHorizontalSnap";
		public string ORBIT_HORIZONTAL= "OrbitHorizontal";
		public string ORBIT_VERTICAL = "OrbitVertical";
		public string ZOOM = "Mouse ScrollWheel";
	}

	public PositionSettings position = new PositionSettings ();
	public OrbitSettings orbit = new OrbitSettings();
	public InputSettings input = new InputSettings();

	Vector3 targetPos = Vector3.zero;
	Vector3 destination = Vector3.zero;
	CharacterController charController;
	float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput;

	// Use this for initialization
	void Start () {

		target = GameObject.FindGameObjectWithTag("Player").transform;

		targetPos = target.position + position.targetPosOffset;
		destination = Quaternion.Euler (orbit.xRotation, orbit.yRotation, 0) * -Vector3.forward;
		destination += targetPos;
		transform.position = destination;
		
	}

	void MoveToTarget(){
		targetPos = target.position + position.targetPosOffset;
		destination = Quaternion.Euler (orbit.xRotation, orbit.yRotation, 0) * -Vector3.forward;
		destination += targetPos;
		transform.position = destination;

	}
	
	// Update is called once per frame
	void Update () {
		MoveToTarget ();
		GetInput ();
		OrbitTarget ();
		ZoomInOnTarget ();
		LookAtTarget ();
	}

	void GetInput(){
		vOrbitInput = Input.GetAxisRaw (input.ORBIT_VERTICAL);
		hOrbitInput = Input.GetAxisRaw (input.ORBIT_HORIZONTAL);
		hOrbitSnapInput = Input.GetAxisRaw (input.ORBIT_HORIZONTAL_SNAP);
		zoomInput = Input.GetAxisRaw (input.ZOOM);
	}

	void LookAtTarget(){
		Quaternion targetRotation = Quaternion.LookRotation (targetPos - transform.position);
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, position.lookSmooth * Time.deltaTime);
	}

	void OrbitTarget(){
		if (Input.GetButtonDown(input.ORBIT_HORIZONTAL_SNAP)) {
//			orbit.yRotation = -100;
			LookAtTarget();
			MoveToTarget ();
		}

		orbit.xRotation += -vOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;
		orbit.yRotation += -hOrbitInput * orbit.hOrbitSmooth * Time.deltaTime;

		if (orbit.xRotation > orbit.maxXRotation) {
			orbit.xRotation = orbit.maxXRotation;
		}
		if (orbit.xRotation < orbit.minXRotation) {
			orbit.xRotation = orbit.minXRotation;
		}
	

	}

	void ZoomInOnTarget(){
		position.distanceFromTarget += zoomInput * position.zoomSmooth;
	
		if (position.distanceFromTarget > position.maxZoom) {
			position.distanceFromTarget = position.maxZoom;
		}
		if (position.distanceFromTarget < position.minZoom) {
			position.distanceFromTarget = position.minZoom;
		}
	}
}
