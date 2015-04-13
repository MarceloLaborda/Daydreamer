using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

    private SoundManager soundManager;

    public string nextLevel;

	// Use this for initialization
	void Start () {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!soundManager.music["Loading_Track"].isPlaying) {
            soundManager.StopAll();
            Application.LoadLevel(nextLevel);
        }
	}
}
