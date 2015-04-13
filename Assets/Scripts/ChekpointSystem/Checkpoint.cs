using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {


    CheckpointSystem checkpointSystem;

	// Use this for initialization
	void Start () {
	    checkpointSystem = GameObject.Find("CheckpointSystem").GetComponent<CheckpointSystem>();
	}

    //If player walks past the checkpoint, make this as the active checkpoint.
    void OnTriggerEnter2D(Collider2D target) {
        if (target.gameObject.tag == "Player"){
            if (checkpointSystem != null){
                checkpointSystem.ActivateCheckpoint(gameObject);
            }
        }
    }


}
