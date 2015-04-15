using UnityEngine;
using System.Collections;

public class MovableRockPart : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private Color alphaStart;
	private Color alphaEnd;
	private float t = 0.0f;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		alphaStart = spriteRenderer.color;
		alphaEnd = new Color (alphaStart.r, alphaStart.g, alphaStart.b, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;

		renderer.material.color = Color.Lerp (alphaStart, alphaEnd, t / 2);

		if (renderer.material.color.a <= 0.0)
			Destroy (gameObject);
	}
}
