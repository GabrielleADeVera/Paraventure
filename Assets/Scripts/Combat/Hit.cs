using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour {


	//Array of type Collider
	Collider[] hits; 
	public Transform collisionPoint;
	public float collisionRadius = 0.5f;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		hits = Physics.OverlapSphere (collisionPoint.position, collisionRadius);

		foreach (Collider Hit in hits) {
			if (Hit.transform.root != transform) {
			//prevent hitting self
				Debug.Log(Hit.name);
			}
		}
	}
}


