using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {


	public Door door;

	private Animator animator;

	public bool pressure = false;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Player" || target.gameObject.tag == "Box") {
			if (door != null)
				door.Toggle (true);
			animator.SetInteger ("AnimState", 1);
		}
	}

	void OnTriggerExit2D(Collider2D target){
		if (target.gameObject.tag == "Player" || target.gameObject.tag == "Box") {
			if (pressure) {
				if (door != null)
					door.Toggle (false);
				animator.SetInteger ("AnimState", 0);
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		if (door != null)
			Gizmos.DrawLine (transform.position, door.transform.position);
	}
}
