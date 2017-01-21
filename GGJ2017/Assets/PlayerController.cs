using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject respawnPoint;

	private void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag == "Death") {
			print("Player dead, respawn");
			gameObject.transform.position = respawnPoint.transform.position;
		}
 	}

 }