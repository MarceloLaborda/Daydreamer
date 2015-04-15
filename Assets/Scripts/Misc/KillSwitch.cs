using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KillSwitch : MonoBehaviour {

    public GameObject laser;

    public int type = 0; //1 Laser
                         //2 Enemies

    private Animator animator;

    public bool isActive;

    public List<FlyingEnemySpawner> enemySpawners;


    // Use this for initialization
    void Start() {
        laser.SetActive(false);
        isActive = true;
        animator = GetComponent<Animator>();
    }



    void OnTriggerEnter2D(Collider2D target) {
        if (isActive) {
            if (type == 1) {
                if (target.gameObject.tag == "Player") {
                    // if (laser != null)
                    //laser.Toggle(true);
                    animator.SetInteger("AnimState", 1);
                    isActive = false;
                    laser.SetActive(true);
                }
                Invoke("ResetLaser", 1.0f);
            } else if (type == 2) {
                if (target.gameObject.tag == "Player") {
                    animator.SetInteger("AnimState", 1);
                    foreach (FlyingEnemySpawner spawner in enemySpawners) {
                        spawner.SpawnEnemy();
                        isActive = false;
                    }
                }
            }
        }
    }

    void ResetLaser() {
        laser.SetActive(false);
        //isActive = false;
        //animator.SetInteger("AnimState", 0);
    }

    public void ResetSwitch() {
        isActive = true;
        animator.SetInteger("AnimState", 0);
    }

    void OnDrawGizmos() {
        //Gizmos.color = Color.red;
        //if (door != null)
         //   Gizmos.DrawLine(transform.position, door.transform.position);
    }
}
