using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	private float _timeToDrop;

	// Use this for initialization
	void Start () {
		_timeToDrop = Random.Range(10.0f, 20.0f);
	}
	
	// Update is called once per frame
	void Update () {
		_timeToDrop -= Time.deltaTime;
		if(_timeToDrop < 0){
			gameObject.SetActive(false);
		}
	}
}
