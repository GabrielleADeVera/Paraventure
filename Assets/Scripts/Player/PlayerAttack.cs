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
			Debug.Log ("Kachow");
			}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
