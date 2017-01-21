using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject respawnPoint;
	private float respawnTime = 5.0f;
	private bool _isDead = false;


	private void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag == "Death") {
			print("Player dead, respawn");
			_isDead = true;
		}
 	}

 	private void Update() {
 		if(_isDead){
			respawnTime -= Time.deltaTime;
 		}
		if(respawnTime < 0){
			_isDead = false;
			respawnTime = 5.0f;
			gameObject.transform.position = respawnPoint.transform.position;
		}
 	}

 }