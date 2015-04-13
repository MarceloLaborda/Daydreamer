using UnityEngine;
using System.Collections;

public class EnemyFacingDirection : MonoBehaviour {

	public Transform sightStart, sightEnd;

	private bool collision = false;
	private bool grounded = true;

	[SerializeField] private LayerMask whatIsGround;

    public bool jumpsOffPlatforms = true;
	public Transform groundCheck;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		collision = Physics2D.Linecast (sightStart.position, sightEnd.position, whatIsGround);


		grounded = Physics2D.OverlapCircle(groundCheck.position, .1f, whatIsGround);

        if (collision || !grounded) {
            this.transform.localScale = new Vector3((transform.localScale.x == 1) ? -1 : 1, 1, 1);
        }
	}

    void OnDrawGizmos() {
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.yellow);
    }
}
