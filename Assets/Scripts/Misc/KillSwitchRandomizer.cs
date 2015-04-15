using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KillSwitchRandomizer : MonoBehaviour {

    public List<KillSwitch> switches;

	// Use this for initialization
	void Start () {
        Randomize();
	}
	

	public void Randomize () {
        int whichIsLaser = Random.Range(0, 3);
        int counter = 0;
        foreach (KillSwitch switchObj in switches) {
            if (counter == whichIsLaser) {
                switchObj.type = 1;
            } else {
                switchObj.type = 2;
            }
            counter++;
        }
	}

    void Update() {
        bool allInactive = true;
        foreach (KillSwitch switchObj in switches) {
            if (switchObj.isActive) {
                allInactive = false;
            }
        }
        if (allInactive) {
            foreach (KillSwitch switchObj in switches) {
                switchObj.ResetSwitch();
            }
            Randomize();
        }
    }
}
