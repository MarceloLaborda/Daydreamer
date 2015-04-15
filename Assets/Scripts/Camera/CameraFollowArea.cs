using UnityEngine;
using System.Collections;

public class CameraFollowArea : MonoBehaviour {
	
	public Transform target;
	
	//Mathf.SmoothDamp(transform.position.y, target.position.y, ref yVelocity, smoothTime);
	
	public float smoothTimeX = 0.6f;
	public float smoothTimeY = 0.1f;
	public float smoothTimeZ = 0.4f;
	public float maxSpeed = 100.0f;
	
	public Vector3 cameraOffset = new Vector3(1.0f, 1.5f, -10.0f);
	
	public Vector3 initialPosition = new Vector3(1.0f, 1.5f, -5.0f);
	
	private Vector3 currentPosition = Vector3.zero;
	private Vector3 targetPosition = Vector3.zero;
	
	private Vector3 currentVelocity = Vector3.zero;
	
	
	// Use this for initialization
	private void Start()
	{
		transform.position = initialPosition;//RELATIVEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
		currentPosition = transform.position;
	}
	
	// Update is called once per frame
	private void LateUpdate()
	{
		if (target != null) {
			//Mathf.Sign(player    determining which way the player is currently facing.
			targetPosition = new Vector3 (target.transform.position.x + cameraOffset.x * Mathf.Sign(target.transform.localScale.x), target.transform.position.y + cameraOffset.y, cameraOffset.z);
		}
		currentPosition.x = Mathf.SmoothDamp(currentPosition.x, targetPosition.x, ref currentVelocity.x, smoothTimeX, maxSpeed, Time.deltaTime);
		currentPosition.y = Mathf.SmoothDamp(currentPosition.y, targetPosition.y, ref currentVelocity.y, smoothTimeY, maxSpeed, Time.deltaTime);
		currentPosition.z = Mathf.SmoothDamp(currentPosition.z, targetPosition.z, ref currentVelocity.z, smoothTimeZ, maxSpeed, Time.deltaTime);
		transform.position = currentPosition;
		
	}
}