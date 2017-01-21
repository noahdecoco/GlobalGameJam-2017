using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	private float _timeToDrop;
	private float _timeToRise;
	private bool _isDropped = false;
	public float minSecs = 2.0f;
	public float maxSecs = 20.0f;

	private Vector3 _oldPosition;
	private Vector3 _newPosition;

	float lerpTime = 3.0f;
    float currentLerpTime = 0.0f;

	void Start () {

		_timeToDrop = getRandomSecs(15.0f, 60.0f);
		_timeToRise = getRandomSecs(3.0f, 6.0f);
		_oldPosition = new Vector3(0,Random.Range(-40.0f, -80.0f),0);
		_newPosition = new Vector3(0,0,0);
	}

	void Update () {

		CalcDrop();
		updatePosition();

	}

	void updatePosition() {

		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > lerpTime) currentLerpTime = lerpTime;
		float perc = currentLerpTime / lerpTime;
		print("drop" + perc);
		transform.position = Vector3.Lerp(_oldPosition, _newPosition, perc);
	}

	float getRandomSecs(float min, float max) {

		return Random.Range(min, max);

	}

	void CalcDrop () {

		if(!_isDropped){
			_timeToDrop -= Time.deltaTime;
		} else {
			_timeToRise -= Time.deltaTime;
		}

		if(_timeToDrop < 0){
			if(!_isDropped){
				currentLerpTime = 0.0f;
				lerpTime = 5.0f;
				_oldPosition = new Vector3(0,0,0);
				_newPosition = new Vector3(0,-12,0);
				_timeToDrop = getRandomSecs(5.0f, 30.0f);
			}
			_isDropped = true;
		}

		if(_timeToRise < 0){
			if(_isDropped){
				currentLerpTime = 0.0f;
				lerpTime = 3.0f;
				_oldPosition = new Vector3(0,-12,0);
				_newPosition = new Vector3(0,0,0);
				_timeToRise = getRandomSecs(3.0f, 6.0f);
			}
			_isDropped = false;
		}
	}
}
