using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Enemy" ) {
            GetComponent<CharacterController2D>().CharacterDies();
		}
	}
}
