using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Player"){
			target.gameObject.GetComponent<CharacterController2D>().Bounce();
			Destroy (transform.parent.gameObject);
		}
	}
}
