using UnityEngine;
using System.Collections;

public class LevelRatingSystem : MonoBehaviour {

    

    private int rating;
    private FireflySystem fireflySystem;
    private LevelRatingDisplay levelRatingDisplay;

	// Use this for initialization
	void Start () {
        fireflySystem = GameObject.Find("FireflySystem").GetComponent<FireflySystem>();
        levelRatingDisplay = GameObject.Find("LevelRatingDisplay").GetComponent<LevelRatingDisplay>();
        if (fireflySystem == null){
            Debug.Log("Firefly System not found.");
        }
        if (levelRatingDisplay == null)
        {
            Debug.Log("Level Rating Display not found.");
        }
	}

    public void GetRating(){
        float percentage = ((float)fireflySystem.GetCollectedFireflies() / (float)fireflySystem.GetTotalFireflies()) * 100.0f;

        if (percentage < 10.0){
            rating = 1;
        }else if (percentage < 20.0){
            rating = 2;
        }else if (percentage < 30.0){
            rating = 3;
        }else if (percentage < 100.0){
            rating = 4;
        }else{
            rating = 5;
        }
        levelRatingDisplay.UpdateLevelRatingDisplay(rating);
    }
	
}
