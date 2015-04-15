using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public const int OPEN = 1;
	public const int CLOSE = 0;

	private Animator animator;

	public bool ignoreTrigger;

    public bool startsOpen = false;

    private SoundManager soundManager;


	// Use this for initialization
	void Start () {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
		animator = GetComponent<Animator> ();
        if (startsOpen) {
            animator.SetInteger("AnimState", 1);
            collider2D.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target){
		animator.SetInteger ("AnimState", 1);
		//collider2D.enabled = false;
	}
	
	void OnTriggerExit2D(Collider2D target){
		animator.SetInteger ("AnimState", 0);
		//collider2D.enabled = true;
	}

	public void Toggle(bool value){
		if (value) {
			animator.SetInteger ("AnimState", 1);
            soundManager.sfx["Door_SoundEffect"].Play();
			collider2D.enabled = false;
		} else {
			animator.SetInteger ("AnimState", 0);
            soundManager.sfx["Door_SoundEffect"].Play();
			collider2D.enabled = true;
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.green;

		var boxCollider2D = GetComponent<BoxCollider2D> ();
		var boxCollider2Dpos = boxCollider2D.transform.position;
		var gizmoPos = new Vector2 (boxCollider2Dpos.x + boxCollider2D.center.x, boxCollider2Dpos.y + boxCollider2D.center.y);
		Gizmos.DrawWireCube(gizmoPos, new Vector2(boxCollider2D.size.x, boxCollider2D.size.y));
	}
}
