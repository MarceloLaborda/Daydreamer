using UnityEngine;
using System.Collections;

public class RockSpawner : MonoBehaviour {


    public GameObject rock;
    private GameObject instantiatedRock;
    public bool rockIsBroken = true;

    public void SpawnRock() {
        instantiatedRock = Instantiate(rock, transform.position, transform.rotation) as GameObject;
        rockIsBroken = false;
    }


    void Update() {
        if(instantiatedRock==null){
            rockIsBroken = true;
        }
    }
	

}
