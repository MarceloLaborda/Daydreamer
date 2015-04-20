using UnityEngine;
using System.Collections;

public class AreaSwitch : MonoBehaviour {


	public Door door;

    public bool closes = true;



	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Player") {
			if (door != null){
                if (closes) {
                    door.Toggle(false);
                } else {
                    door.Toggle(true);
                }
            }
		}
	}



	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		if (door != null)
			Gizmos.DrawLine (transform.position, door.transform.position);
	}
}
