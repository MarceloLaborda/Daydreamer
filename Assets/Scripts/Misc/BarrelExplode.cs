﻿using UnityEngine;
using System.Collections;

public class BarrelExplode : MonoBehaviour {

	public BarrelPart barrelPart;
	public int totalParts = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Player"){
			Explode();
		}
	}

	void Explode(){
		Destroy (gameObject);

		var t = transform;

		for (int i = 0; i < totalParts; i++) {
			t.TransformPoint (0,-20,0);
			BarrelPart clone = Instantiate (barrelPart, t.position, Quaternion.identity) as BarrelPart;
			clone.rigidbody2D.AddForce(Vector3.right * Random.Range (-50,50));
			clone.rigidbody2D.AddForce (Vector3.up * Random.Range (100,400));
		}
	}
}
