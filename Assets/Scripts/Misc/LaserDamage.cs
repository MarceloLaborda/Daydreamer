using UnityEngine;
using System.Collections;

public class LaserDamage : MonoBehaviour {

    private CharacterController2D PlayerScript;

    void Start() {
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
    }

    void OnCollisionStay2D(Collision2D target) {
        if (target.gameObject.tag == "Enemy") {
            if (target.gameObject.GetComponent<FirstBoss>() != null) {
                target.gameObject.GetComponent<FirstBoss>().Damage();
            }
            gameObject.SetActive(false);
        }
        if (target.gameObject.tag == "Player") {
            PlayerScript.CharacterDies();
        }
    }
}
