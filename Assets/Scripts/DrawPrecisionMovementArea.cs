using UnityEngine;
using System.Collections;

public class DrawPrecisionMovementArea : MonoBehaviour {

	private PrecisionMovementArea area;

	void OnDrawGizmos() {
		area = transform.parent.GetComponent<PrecisionMovementArea> ();
		Gizmos.color = area.areaStrokeColor;
		Gizmos.DrawWireCube(transform.position, new Vector3(area.areaWidth, area.areaHeight, 0.0f));
		Gizmos.color = area.areaFillColor;
		Gizmos.DrawCube(transform.position, new Vector3(area.areaWidth, area.areaHeight, 1.0f));
	}
}
