using UnityEngine;
using System.Collections;

public class CharacterEnvelope : MonoBehaviour {
	
	public Color envelopeFillColor = new Color(0.0f, 0.0f, 1.0f, 0.2f);
	public Rect envelope = new Rect(0.0f, 0.0f, 2.0f, 4.0f);

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void LateUpdate () {
		envelope.center = new Vector2 (transform.position.x, transform.position.y);
	}

	void OnDrawGizmos() {
		envelope.center = new Vector2 (transform.position.x, transform.position.y);
		Gizmos.color = envelopeFillColor;
		Gizmos.DrawCube(envelope.center, envelope.size);
	}
}
