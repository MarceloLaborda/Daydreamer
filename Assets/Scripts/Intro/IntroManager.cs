using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntroManager : MonoBehaviour {

    public List<GameObject> takes;
    public int currentTake;
    private int numTakes;
    private SoundManager soundManager;

    // Use this for initialization
    void Start() {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        currentTake = 0;
        numTakes = takes.Count;
    }

    public void NextTake() {
        if (currentTake + 1 < numTakes) {
            currentTake++;
            StartCoroutine(FadeIn());
        }else{
            soundManager.StopAll();
            Application.LoadLevel("ForestLevelTransition");
        }
    }

    public void SkipIntro() {
        soundManager.StopAll();
        Application.LoadLevel("ForestLevelTransition");
    }

    IEnumerator FadeIn(){
        SpriteRenderer currentTakeRenderer = takes[currentTake].GetComponent<SpriteRenderer>();
        while (currentTakeRenderer.color.a < 1.0f){
            currentTakeRenderer.color = new Color(currentTakeRenderer.color.r, currentTakeRenderer.color.g, currentTakeRenderer.color.b, currentTakeRenderer.color.a + 0.1f);
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }
}