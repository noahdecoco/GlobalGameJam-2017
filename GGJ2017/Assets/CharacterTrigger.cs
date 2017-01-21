using UnityEngine;
using System.Collections;

public class CharacterTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		print(other);
	}

}
