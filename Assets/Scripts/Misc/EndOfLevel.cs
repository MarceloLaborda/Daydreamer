using UnityEngine;
using System.Collections;

public class EndOfLevel : MonoBehaviour {

    private LevelRatingSystem levelRatingSystem;
    public string nextLevel;
    [Range(0.0f, 10.0f)] public float waitTime;
    private SoundManager soundManager;


    void Start(){
        levelRatingSystem = GameObject.Find("LevelRatingSystem").GetComponent<LevelRatingSystem>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            if (levelRatingSystem != null)
            {
                target.gameObject.SetActive(false);
                levelRatingSystem.GetRating();
            }
            Invoke("LoadNextLevel", waitTime);
        }
    }

    void LoadNextLevel() {
        soundManager.StopAll();
        if (nextLevel == "TitleScreen") {
            Destroy(soundManager.gameObject);
        }
        Application.LoadLevel(nextLevel);
    }
}
