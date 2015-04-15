using UnityEngine;
using System.Collections;

public class ZoomingArea : MonoBehaviour {

	public Camera targetCamera;
	public float zoomTargetValue = 2.0f;

	public bool inverseDirection = false;

	private float zoomValue = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//print(zoomValue);
	}
	
	void OnTriggerStay2D(Collider2D target) {
		if (target.tag == "Player") {
			if(target.transform.position.x <= collider2D.bounds.max.x && target.transform.position.x >= collider2D.bounds.min.x){
				zoomValue = Mathf.Abs (target.transform.position.x - (inverseDirection ? collider2D.bounds.min.x : collider2D.bounds.max.x));
				targetCamera.SendMessage ("Zoom", (zoomValue * zoomTargetValue)/ collider2D.bounds.size.x);
			}
		}
	}

	void OnTriggerExit2D(Collider2D target) {
		zoomValue = Mathf.Round (zoomValue);
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		var boxCollider2D = GetComponent<BoxCollider2D> ();
		var boxCollider2Dpos = boxCollider2D.transform.position;
		var gizmoPos = new Vector2 (boxCollider2Dpos.x + boxCollider2D.center.x, boxCollider2Dpos.y + boxCollider2D.center.y);
		Gizmos.DrawWireCube(gizmoPos, new Vector2(boxCollider2D.size.x, boxCollider2D.size.y));
	}
}
