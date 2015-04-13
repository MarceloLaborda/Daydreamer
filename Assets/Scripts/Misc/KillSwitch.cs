using UnityEngine;
using System.Collections;

public class KillSwitch : MonoBehaviour {

    public GameObject laser;

    private Animator animator;

    private bool isActive;


    // Use this for initialization
    void Start() {
        laser.SetActive(false);
        isActive = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D target) {
        if (!isActive) {
            if (target.gameObject.tag == "Player") {
                // if (laser != null)
                //laser.Toggle(true);
                animator.SetInteger("AnimState", 1);
                isActive = true;
                laser.SetActive(true);
            }
            Invoke("ResetSwitch", 1.0f);
        }
    }



    void ResetSwitch() {
        laser.SetActive(false);
        isActive = false;
        animator.SetInteger("AnimState", 0);
    }

    void OnDrawGizmos() {
        //Gizmos.color = Color.red;
        //if (door != null)
         //   Gizmos.DrawLine(transform.position, door.transform.position);
    }
}
