using UnityEngine;
using System.Collections;

public class AreaSwitch : MonoBehaviour {


	public Door door;



	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Player") {
			if (door != null)
				door.Toggle (false);
		}
	}



	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		if (door != null)
			Gizmos.DrawLine (transform.position, door.transform.position);
	}
}
