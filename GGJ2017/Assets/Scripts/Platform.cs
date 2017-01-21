using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	private float _timeToShake;
	private float _timeToDrop;
	private float _timeToRise;

	private bool _isSteady = true;
	private bool _isShaking = false;
	private bool _isDropped = false;

	private MeshRenderer _meshRend;
	private Color _color;

	void Start () {
		
		_meshRend = GetComponent<MeshRenderer>();
		_color = _meshRend.material.color;

		float delay = Random.Range(1.0f,3.0f);
		iTween.MoveFrom(gameObject,iTween.Hash("y",-10,"time",3,"delay",delay));
		iTween.FadeFrom(gameObject,iTween.Hash("a",0,"time",2,"delay",delay));
		resetTimers();

	}

	void Update () {

		updateState();

	}

	void resetTimers(){

		_timeToShake = Random.Range(10.0f, 40.0f);
		_timeToDrop = 2.0f;
		_timeToRise = Random.Range(3.0f, 6.0f);

	}

	void updateState(){

		if(_isSteady) {
			_timeToShake -= Time.deltaTime;
			if(_timeToShake < 0) {
				_isSteady = false;
				_isShaking = true;	
			}
			return;
		}

		if(_isShaking) {
			transform.position = new Vector3(0, Random.Range(-0.2f, 0.0f), 0);
			_timeToDrop -= Time.deltaTime;
			if(_timeToDrop < 0) {
				_isShaking = false;
				_isDropped = true;
				iTween.MoveTo(gameObject, new Vector3(0,-10,0), 2);
				iTween.FadeTo(gameObject, 0, 0.5f);
			}
			return;
		}

		if(_isDropped) {
			_timeToRise -= Time.deltaTime;
			if(_timeToRise < 0) {
				_isDropped = false;
				_isSteady = true;
				resetTimers();
				iTween.MoveTo(gameObject, new Vector3(0,0,0), 2);
				iTween.FadeTo(gameObject, 1, 0.8);
			}
			return;
		}

	}
}