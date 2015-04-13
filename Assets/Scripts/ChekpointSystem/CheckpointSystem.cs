using UnityEngine;
using System.Collections;

public class CheckpointSystem : MonoBehaviour {

    public GameObject activeCheckpoint;

    public void ActivateCheckpoint(GameObject checkpoint){
        activeCheckpoint = checkpoint;
    }
}
