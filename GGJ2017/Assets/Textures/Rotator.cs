using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public float rotateSpeed;

	private float rotateMultiplier;
	private float oldRotateSpeed;
	private float newRotateSpeed;

	private float _timeToChangeDirection;

	static float t = 0.0f;

	float lerpTime = 3.0f;
    float currentLerpTime;

	void Start() {

		_timeToChangeDirection = Random.Range(15.0f, 20.0f);
		oldRotateSpeed = 0.0f;
		newRotateSpeed = Mathf.Round(Random.Range(-3.0f, +3.0f)) * rotateSpeed;
		rotateMultiplier = rotateSpeed;

	}

	void Update () {

		_timeToChangeDirection -= Time.deltaTime;

		if(_timeToChangeDirection < 0) {
			oldRotateSpeed = rotateSpeed;
			newRotateSpeed = Random.Range(-3.0f, +3.0f) * rotateMultiplier;
			_timeToChangeDirection = Random.Range(15.0f, 20.0f);
			currentLerpTime = 0.0f;
		}

		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > lerpTime) {
            currentLerpTime = lerpTime;
        }
		float perc = currentLerpTime / lerpTime;
		rotateSpeed = Mathf.Lerp(oldRotateSpeed, newRotateSpeed, perc);

		rotate();

	}

	void rotate(){

		transform.Rotate(Vector3.up.normalized, rotateSpeed * Time.deltaTime);

	}

}
