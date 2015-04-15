using UnityEngine;
using System.Collections;

public class PlatformRoot : MonoBehaviour {

	public GameObject RootedTo;

	void Update () {
		if (RootedTo != null) {
			this.transform.position = RootedTo.transform.position;	
		}
	}
}
