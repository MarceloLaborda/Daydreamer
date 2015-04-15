using UnityEngine;
using System.Collections;

public class FlyingEnemySpawner : MonoBehaviour {

    public GameObject flyingEnemy;

	// Use this for initialization
	public void SpawnEnemy () {
        Instantiate(flyingEnemy, transform.position, transform.rotation);
	}
	
}
