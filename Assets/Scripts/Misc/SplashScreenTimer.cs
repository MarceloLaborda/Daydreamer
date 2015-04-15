using UnityEngine;
using System.Collections;

public class SplashScreenTimer : MonoBehaviour {

    public float secondsToLoad = 2.0f;
    public string nextScene;
    public SpriteRenderer splashScreenRenderer;


	// Use this for initialization
	void Start () {
        StartCoroutine(LoadTitleScreen());

	}

    IEnumerator LoadTitleScreen() {
        yield return new WaitForSeconds(secondsToLoad);
        while (splashScreenRenderer.color.a > 0.0f) {
            splashScreenRenderer.color = new Color(splashScreenRenderer.color.r, splashScreenRenderer.color.g, splashScreenRenderer.color.b, splashScreenRenderer.color.a - 0.1f);
            yield return new WaitForSeconds(0.04f);
        }
        Application.LoadLevel(nextScene);
    }
	
}
