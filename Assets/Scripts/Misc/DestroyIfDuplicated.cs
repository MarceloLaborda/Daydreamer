using UnityEngine;
using System.Collections;

public class DestroyIfDuplicated : MonoBehaviour {

	// Use this for initialization
    void Start() {
        if (GameObject.Find("SoundManager")!=null) {
            Destroy(gameObject);
        }
    }

}
