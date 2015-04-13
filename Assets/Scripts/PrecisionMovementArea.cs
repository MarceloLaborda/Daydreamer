using UnityEngine;
using System.Collections;

public class PrecisionMovementArea : MonoBehaviour {
	
	public CharacterEnvelope player;
	public Color areaStrokeColor = Color.red;
	public Color areaFillColor = new Color(1.0f, 0.0f, 0.0f, 0.2f);
	public float areaWidth = 0.0f;
	public float areaHeight = 0.0f;
	private Rect area;

	//public float screenMarginY = 1.0f; // Distance in the y axis the player can move before the camera follows.
	
	// Use this for initialization
	void Start () {
		area.center = new Vector2 (transform.position.x, transform.position.y);
		area.width = areaWidth;
		area.height = areaHeight;
	}


	
	// Update is called once per frame
	void FixedUpdate () {
		area.center = new Vector2 (transform.position.x, transform.position.y);

		//if (area.Contains (player.envelope.center)) {
			if(player.envelope.xMax > area.xMax){
				transform.position += new Vector3(player.envelope.xMax - area.xMax, 0.0f, 0.0f);
				transform.localScale = new Vector3(1,1,1);
			}else if(player.envelope.xMin < area.xMin){
				transform.position -= new Vector3(area.xMin - player.envelope.xMin, 0.0f, 0.0f);
				transform.localScale = new Vector3(-1,1,1);
			}
			if(player.envelope.yMax > area.yMax){
				transform.position += new Vector3(0.0f, player.envelope.yMax - area.yMax, 0.0f);
			}else if(player.envelope.yMin < area.yMin){
				transform.position -= new Vector3(0.0f, area.yMin - player.envelope.yMin, 0.0f);
			}
		//}

	}

	public void Snap(){
		//print (Mathf.Abs(area.yMin - player.envelope.yMin));
		transform.position += new Vector3( 0.0f, Mathf.Abs(area.yMin - player.envelope.yMin), 0.0f);
	}

	void OnDrawGizmos() {
		area.center = new Vector2 (transform.position.x, transform.position.y);
		area.width = areaWidth;
		area.height = areaHeight;
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(new Vector3(area.xMin,area.yMax, 0.0f), new Vector3(area.xMin,area.yMin,0.0f));
		Gizmos.DrawLine(new Vector3(area.xMin,area.yMin,0.0f), new Vector3(area.xMax,area.yMin,0.0f));
		Gizmos.DrawLine(new Vector3(area.xMax,area.yMin,0.0f), new Vector3(area.xMax,area.yMax,0.0f));
		Gizmos.DrawLine(new Vector3(area.xMax,area.yMax,0.0f), new Vector3(area.xMin,area.yMax,0.0f));
	}
	

}

