using UnityEngine;
using System.Collections;

public class Firefly : MonoBehaviour {

	private FireflySystem fireflySystem;

	void Start(){
		fireflySystem = GameObject.Find ("FireflySystem").GetComponent<FireflySystem>();
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Player" ) {
			if(fireflySystem!=null){
				fireflySystem.CollectFirefly();
			}
			Destroy( gameObject);
		}
	}
}
