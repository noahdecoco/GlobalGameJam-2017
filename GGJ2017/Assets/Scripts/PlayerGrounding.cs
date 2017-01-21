using UnityEngine;
using System.Collections;

public class PlayerGrounding : MonoBehaviour {

	private void OnCollisionEnter(Collision other) {

		if(other.gameObject.tag == "MovingPlatform") {
			print("Enter");
//			transform.parent = other.transform;
		}
 	}

	private void OnCollisionExit(Collision other) {
		if(other.gameObject.tag == "MovingPlatform") {
			print("Leave");
			if(transform.parent == other.transform){
//				transform.parent = null;
			}
		}
 	}

}