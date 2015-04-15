using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockSpawnerSwitch : MonoBehaviour {


    public List<RockSpawner> rockSpawners;

	private Animator animator;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Player") {
			animator.SetInteger ("AnimState", 1);
            bool allRocksBroken = true;
            foreach (RockSpawner spawner in rockSpawners) {
                if (spawner.rockIsBroken == false) {
                    allRocksBroken = false;
                }
            }
            if (allRocksBroken == true) {
                foreach (RockSpawner spawner in rockSpawners) {
                    spawner.SpawnRock();
                }
            }
		}
	}

	void OnTriggerExit2D(Collider2D target){
		if (target.gameObject.tag == "Player") {
			animator.SetInteger ("AnimState", 0);
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
        foreach (RockSpawner spawner in rockSpawners) {
            Gizmos.DrawLine(transform.position, spawner.gameObject.transform.position);
        }
		
	}
}
