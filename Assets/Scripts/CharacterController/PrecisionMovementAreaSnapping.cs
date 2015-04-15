using UnityEngine;
using System.Collections;

public class PrecisionMovementAreaSnapping : MonoBehaviour {

	public PrecisionMovementArea precisionMovementArea;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D target){
		if(target.tag == "Player"){
			PlatformerCharacter2D controller = GameObject.FindWithTag("Player").GetComponent<PlatformerCharacter2D>();
			if(controller.grounded){
				precisionMovementArea.SendMessage ("Snap");
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		var boxCollider2D = GetComponent<BoxCollider2D> ();
		if (boxCollider2D != null) {
			var boxCollider2Dpos = boxCollider2D.transform.position;
			var gizmoPos = new Vector2 (boxCollider2Dpos.x + boxCollider2D.center.x, boxCollider2Dpos.y + boxCollider2D.center.y);
			Gizmos.DrawWireCube (gizmoPos, new Vector2 (boxCollider2D.size.x, boxCollider2D.size.y));
		}
	}
}
