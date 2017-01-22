using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Public vars.
    public bool IsShaking = false;

    public bool IsDropped = false;

    public float DropTimerPercentageElapsed
    {
        get
        {
            return dropTimer / timeToDrop;
        }
    }

    // Static vars.
    private static float timeToDrop = 2f;

    private static float dropDistance = 30f;

    // Private vars.
    private float _timeToShake;

	private float _timeToRise;

    private float dropTimer;

    private bool _isSteady = true;

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
		dropTimer = timeToDrop;
		_timeToRise = Random.Range(3.0f, 6.0f);

	}

	void updateState(){

		if(_isSteady) {
			_timeToShake -= Time.deltaTime;
			if(_timeToShake < 0) {
				_isSteady = false;
				IsShaking = true;	
			}
			return;
		}

		if(IsShaking) {
			transform.position = new Vector3(0, Random.Range(-0.5f, 0.0f), 0);
			dropTimer -= Time.deltaTime;
			if(dropTimer < 0) {
				IsShaking = false;
				IsDropped = true;
				iTween.MoveTo(gameObject, new Vector3(0, -dropDistance, 0), 2);
				iTween.FadeTo(gameObject, 0, 0.5f);
			}
			return;
		}

		if(IsDropped) {
			_timeToRise -= Time.deltaTime;
			if(_timeToRise < 0) {
				IsDropped = false;
				_isSteady = true;
				resetTimers();
				iTween.MoveTo(gameObject, new Vector3(0,0,0), 2);
				iTween.FadeTo(gameObject, 1, 0.8f);
			}
			return;
		}

	}
}