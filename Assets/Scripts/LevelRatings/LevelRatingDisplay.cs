using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelRatingDisplay : MonoBehaviour {


    private const int MAX_STARS = 5;

    public List<GameObject> stars;
    public GameObject perfectTitle;

    void Start(){
        perfectTitle.SetActive(false);
        gameObject.SetActive(false);
    }

    public void UpdateLevelRatingDisplay(int rating) {
        gameObject.SetActive(true);
        StartCoroutine(ShowRating(rating));
    }

    IEnumerator ShowRating(int rating) {
        int i;
        yield return new WaitForSeconds(0.1f);
        for (i = 0; i < rating; i++) {
            stars[i].GetComponent<Animator>().SetInteger("AnimState", 2);
            yield return new WaitForSeconds(0.1f);
            stars[i].GetComponentInChildren<ParticleSystem>().Emit(30);
            yield return new WaitForSeconds(0.3f);
            
        }
        while (i < stars.Count) {
            stars[i].GetComponent<Animator>().SetInteger("AnimState", 1);
            i++;
            yield return new WaitForSeconds(0.4f);
        }
        if (rating == 5) {
            perfectTitle.SetActive(true);
        }
        yield return null;
    }
}
