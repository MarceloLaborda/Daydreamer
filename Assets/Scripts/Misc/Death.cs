using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {


	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Deadly") {
			PlayerDies();
		}
	}

	void PlayerDies(){
		//yield WaitForSeconds(2.0);
		//Destroy (gameObject);
		//playerDying = true;
		animator.Play("Death");
	}

	void DestroyPlayer(){
		Destroy (gameObject);
	}
}
