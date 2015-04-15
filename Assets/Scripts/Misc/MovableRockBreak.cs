using UnityEngine;
using System.Collections;

public class MovableRockBreak : MonoBehaviour {

	public MovableRockPart rockPart;
	public int totalParts = 5;
    public int forceIntensity = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Enemy" || target.gameObject.tag == "Ground"){
			Break();
		}
        if (target.gameObject.name == "FirstBoss") {
            target.gameObject.GetComponent<FirstBoss>().Damage();
        }
	}

	void Break(){
		Destroy (gameObject);

		var t = transform;

		for (int i = 0; i < totalParts; i++) {
			t.TransformPoint (0,-20,0);
            MovableRockPart clone = Instantiate(rockPart, t.position, Quaternion.identity) as MovableRockPart;
			clone.rigidbody2D.AddForce(Vector3.right * Random.Range (-50,50) * forceIntensity);
			clone.rigidbody2D.AddForce (Vector3.up * Random.Range (100,400) * forceIntensity);
		}
	}
}
