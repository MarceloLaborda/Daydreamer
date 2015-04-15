using UnityEngine;
using System.Collections;

public class DummyMovement : MonoBehaviour {


    public float speed = 0.1f;
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.SetBool("Grounded", true);
        animator.SetFloat("HorizontalSpeed", 20.0f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.right * speed;
	}
}
