using UnityEngine;
using System.Collections;

public class FireflySystem : MonoBehaviour {


	private int totalFireflies;
	private int currentFireflies;
	private FireflyHUD fireflyHUD; //reference to firefly counter
    private SoundManager soundManager;

	// Use this for initialization
	void Start () {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
		fireflyHUD = gameObject.GetComponent<FireflyHUD> ();
		totalFireflies = GameObject.FindGameObjectsWithTag("Firefly").Length;
		fireflyHUD.SetTotalFireflies (totalFireflies);
		currentFireflies = 0;
	}
	

	public void CollectFirefly(){
        soundManager.sfx["Firefly_SoundEffect"].Play();
		currentFireflies++;
		if(fireflyHUD!=null)
			fireflyHUD.UpdateCounter (currentFireflies);
	}


	public int GetCollectedFireflies(){
		return currentFireflies;
	}

    public int GetTotalFireflies(){
        return totalFireflies;
    }
}
