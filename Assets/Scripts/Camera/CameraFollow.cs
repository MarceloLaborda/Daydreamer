using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	//Mathf.SmoothDamp(transform.position.y, target.position.y, ref yVelocity, smoothTime);

	public Rect levelBounds = new Rect(0.0f, 0.0f, 10.0f, 10.0f);

	public float smoothTimeX = 0.8f;
	public float minSmoothTimeX = 0.2f;
	public float maxSmoothTimeX = 0.8f;
	public float smoothTimeY = 0.2f;
	public float minSmoothTimeY = 0.2f;
	public float maxSmoothTimeY = 0.6f;
	public float smoothTimeZ = 0.4f;
	public float maxSpeed = 100.0f;

	
	public float walkOffsetX = 3.0f;
	public float runOffsetX = 6.0f;
	public Vector3 cameraOffset = new Vector3(1.0f, 1.5f, -10.0f);

	private float initialCameraOffsetX = 1.0f;


	public Vector3 initialPosition = new Vector3(1.0f, 1.5f, -5.0f);

	private Vector3 currentPosition = Vector3.zero;
	private Vector3 targetPosition = Vector3.zero;

	private Vector3 currentVelocity = Vector3.zero;

	private float zoom = 0.0f;

	// Use this for initialization
	private void Start()
	{
		transform.position = initialPosition;//RELATIVEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
		currentPosition = transform.position;
		initialCameraOffsetX = cameraOffset.x;//NOT REALTIME

	}

    public void Reset() {
        
    }
	
	// Update is called once per frame
	private void LateUpdate()
	{
		if (target != null) {
			//Mathf.Sign(player    determining which way the player is currently facing.
			targetPosition = new Vector3 (target.transform.position.x + cameraOffset.x * Mathf.Sign(target.transform.localScale.x), target.transform.position.y + cameraOffset.y, target.transform.position.z + cameraOffset.z + zoom);
		}
		targetPosition = ConstraintLevelBounds (targetPosition);

		currentPosition.x = Mathf.SmoothDamp(currentPosition.x, targetPosition.x, ref currentVelocity.x, smoothTimeX, maxSpeed, Time.deltaTime);
		currentPosition.y = Mathf.SmoothDamp(currentPosition.y, targetPosition.y, ref currentVelocity.y, smoothTimeY, maxSpeed, Time.deltaTime);
		currentPosition.z = Mathf.SmoothDamp(currentPosition.z, targetPosition.z, ref currentVelocity.z, smoothTimeZ, maxSpeed, Time.deltaTime);
		transform.position = currentPosition;

		//print (camera.pixelRect);
		//print (camera.ScreenToWorldPoint(new Vector3(camera.pixelRect.xMin,camera.pixelRect.yMin,0.0f)));
		//print (levelBounds.xMin + ";" + levelBounds.xMax);

	}

	private Vector3 ConstraintLevelBounds(Vector3 targetPosition){
		// Level boundaries
		Vector3 viewportSizeBy2 = ViewportSizeBy2 ();
		float posX = Mathf.Clamp(targetPosition.x, levelBounds.xMin + viewportSizeBy2.x, levelBounds.xMax - viewportSizeBy2.x);
		float posY = Mathf.Clamp(targetPosition.y, levelBounds.yMin + viewportSizeBy2.y, levelBounds.yMax - viewportSizeBy2.y);
		float posZ = targetPosition.z;
		
		//create new position - within set boundaries
		return new Vector3(posX, posY, posZ);
	}

	private Vector3 ViewportSizeBy2(){
		return (camera.ViewportToWorldPoint (new Vector3 (1, 1, -camera.transform.position.z)) - camera.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, -camera.transform.position.z)));
	}

	public void ToggleSmoothTimeY(bool max){
		if (max) {
			smoothTimeY = maxSmoothTimeY;
		} else {
			smoothTimeY = minSmoothTimeY;
		}
	
	}

	public void ToggleSmoothTimeX(bool max){
		if (max) {
			smoothTimeX = maxSmoothTimeX;
		} else {
			smoothTimeX = minSmoothTimeX;
		}
		
	}

	public void ToggleWalkOffsetX(bool addOffset){
		if (addOffset) {
			cameraOffset.x = walkOffsetX;
		} else {
			cameraOffset.x = initialCameraOffsetX;
		}
		
	}

	public void ToggleRunOffsetX(bool addOffset){
		if (addOffset) {
			cameraOffset.x = runOffsetX;
		} else {
			cameraOffset.x = initialCameraOffsetX;
		}
		
	}
	
	public void Zoom(float zoomValue){
		zoom = zoomValue;
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(new Vector3(levelBounds.xMin,levelBounds.yMax, 0.0f), new Vector3(levelBounds.xMin,levelBounds.yMin,0.0f));
		Gizmos.DrawLine(new Vector3(levelBounds.xMin,levelBounds.yMin,0.0f), new Vector3(levelBounds.xMax,levelBounds.yMin,0.0f));
		Gizmos.DrawLine(new Vector3(levelBounds.xMax,levelBounds.yMin,0.0f), new Vector3(levelBounds.xMax,levelBounds.yMax,0.0f));
		Gizmos.DrawLine(new Vector3(levelBounds.xMax,levelBounds.yMax,0.0f), new Vector3(levelBounds.xMin,levelBounds.yMax,0.0f));
	}

}
