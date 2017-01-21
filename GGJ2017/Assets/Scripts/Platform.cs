using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	private float _timeToDrop;
	private float _timeToRise;
	private bool _isDropped = false;
	public float _minSecs = 2.0f;
	public float _maxSecs = 20.0f;

	void Start () {

		_timeToDrop = getRandomSecs(5.0f, 30.0f);
		_timeToRise = getRandomSecs(3.0f, 6.0f);

	}

	void Update () {

		//CalcDrop();

	}

	float getRandomSecs(float min, float max) {

		return Random.Range(min, max);

	}

	void removePlatform() {

		GetComponent<MeshRenderer>().enabled = false;
		GetComponent<MeshCollider>().enabled = false;

	}

	void addPlatform() {

		GetComponent<MeshRenderer>().enabled = true;
		GetComponent<MeshCollider>().enabled = true;

	}

	void CalcDrop () {

		if(!_isDropped){
			_timeToDrop -= Time.deltaTime;
		} else {
			_timeToRise -= Time.deltaTime;
		}

		if(_timeToDrop < 0){
			if(!_isDropped){
				removePlatform();
				_timeToDrop = getRandomSecs(5.0f, 30.0f);
			}
			_isDropped = true;
		}

		if(_timeToRise < 0){
			if(_isDropped){
				addPlatform();
				_timeToRise = getRandomSecs(3.0f, 6.0f);
			}
			_isDropped = false;
		}
	}
}
