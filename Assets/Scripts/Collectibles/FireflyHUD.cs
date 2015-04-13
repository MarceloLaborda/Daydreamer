using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireflyHUD : MonoBehaviour {


	public Text firefliesCounter; //reference for text that displays the number of fireflies
	public Text firefliesTotal; //reference for text that displays the number of fireflies

	// Use this for initialization
	void Start () {
		firefliesCounter.text = "0";
	}

	public void UpdateCounter (int currentFireflies) {
		firefliesCounter.text = currentFireflies.ToString();
	}

	public void SetTotalFireflies (int totalFireflies){
		firefliesTotal.text = totalFireflies.ToString();
	}
}
