using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 10.0f;
	public float axisX = 0.0f;
	public float axisY = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {



		axisX = Input.GetAxis ("Horizontal");
		axisY = Input.GetAxis ("Vertical");

		transform.position += new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0.0f) * speed * Time.deltaTime;

		if (axisX < 0 && (Input.GetKey("left") || Input.GetKey("a"))) {
			transform.localScale = new Vector3(-1,1,1);
		} else if (axisX > 0 && (Input.GetKey("right") || Input.GetKey("d"))) {
			transform.localScale = new Vector3(1,1,1);
		}
	}
}
