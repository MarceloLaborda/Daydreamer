using UnityEngine;
using System.Collections;

public class FirstBoss : MonoBehaviour {

	public float speed;

	private CharacterController2D PlayerScript;

	private bool EnemyAwake = false;
	private bool EnemyDead = false;

	//Distance who far player has to be to this enemy to wakeup
	public float AwakeDistance = 10f;
	
	//Here are reference slots for AnimationController and Player Sprite Object.
	public Animator AnimatorController;
	public GameObject MySpriteOBJ; 
	private Vector3 MySpriteOriginalScale;

    public int health = 2;

    public Door door;


	void Start () {
		PlayerScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterController2D> ();
		//Start the distance checks. (When player gets close enough, Wake up. When he gets far enough, Go back to sleep.
		InvokeRepeating ("CheckPlayerDistance", 0.5f, 0.5f);

		MySpriteOriginalScale = MySpriteOBJ.transform.localScale;
		MySpriteOBJ.transform.localScale = new Vector3(-MySpriteOriginalScale.x,MySpriteOBJ.transform.localScale.y,1f);

	}

    public void Damage() {
        health--;
        if (health <= 0) {
            EnemyDead = true;
            Invoke("iDied", 0.15f);
        }
        StartCoroutine(HitAnimation());
    }

    IEnumerator HitAnimation() {
        if (MySpriteOBJ != null)
            MySpriteOBJ.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
        yield return new WaitForSeconds(0.05f);
        if (MySpriteOBJ != null)
            MySpriteOBJ.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        yield return new WaitForSeconds(0.05f);
        if (MySpriteOBJ != null)
            MySpriteOBJ.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
        yield return new WaitForSeconds(0.05f);
        if (MySpriteOBJ != null)
            MySpriteOBJ.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    }
	
	void FixedUpdate () {
		this.rigidbody2D.velocity = Vector2.zero;
		//If you are awake. Move towards the player
		if (EnemyAwake == true && EnemyDead == false) {

            rigidbody2D.AddForce(new Vector2(speed, 0.0f));

            if (MySpriteOBJ.transform.localScale.x > 0 && transform.position.x < PlayerScript.transform.position.x && Mathf.Abs(transform.position.x - PlayerScript.transform.position.x) > 2.0f) {
				MySpriteOBJ.transform.localScale = new Vector3(-MySpriteOriginalScale.x,MySpriteOBJ.transform.localScale.y,1f);
                speed = Mathf.Abs(speed);
			}

            if (MySpriteOBJ.transform.localScale.x < 0 && transform.position.x > PlayerScript.transform.position.x && Mathf.Abs(transform.position.x - PlayerScript.transform.position.x) > 2.0f) {
				MySpriteOBJ.transform.localScale = new Vector3(MySpriteOriginalScale.x,MySpriteOBJ.transform.localScale.y,1f);
                speed *= -1.0f;
			}
		}


		//AnimatorController.SetBool ("Awake", EnemyAwake);
		//AnimatorController.SetBool ("Dead", EnemyDead);
	}


	void OnCollisionEnter2D(Collision2D coll) {
	
		if (coll.gameObject.tag == "Player" && EnemyDead == false) {
            /*
			//Check who killed who. If contact happend from the top player killed the enemy. Else player died.
			if(	coll.contacts[0].normal.x > -1f && coll.contacts[0].normal.x < 1f && coll.contacts[0].normal.y < -0.35f){

				coll.rigidbody.AddForce( new Vector2(0f,1500f));
				this.rigidbody2D.AddForce( new Vector2(0f,-200f));
				EnemyDead = true;
				Debug.Log ("Monster died");

				Invoke("iDied",0.15f);
			}else{
             * */
				PlayerScript.CharacterDies();
			//}

		}
	}

	void iDied(){
		//PlayerScript.CharacterKilledEnemy();
		Destroy (this.gameObject);
        if (door != null) {
            door.Toggle(true);
        }
	}


	void CheckPlayerDistance(){

		if (Vector3.Distance (this.transform.position, PlayerScript.transform.position) <= AwakeDistance && EnemyAwake == false) {
//			Debug.Log("Close enough to wake up");
			EnemyAwake = true;

		}

		if (Vector3.Distance (this.transform.position, PlayerScript.transform.position) > AwakeDistance && EnemyAwake == true) {
//			Debug.Log("Far enough to fall back sleep");
			EnemyAwake = false;

		}

	}


}
