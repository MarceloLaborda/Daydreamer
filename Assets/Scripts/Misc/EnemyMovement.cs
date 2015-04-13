using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public float speed = 8.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//rigidbody2D.velocity = new Vector2(transform.localScale.x, 0) * -speed;
        rigidbody2D.AddForce(new Vector2(transform.localScale.x, 0) * -speed);
	}
}
