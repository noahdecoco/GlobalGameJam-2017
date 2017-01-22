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
    private float timeToShake;

	private float timeToRise;

    private float dropTimer;

    private bool isSteady = true;

    private bool performedStartGameAnimation = false;

    private MeshRenderer meshRenderer;

    // Unity callbacks.
	void Start ()
    {
		meshRenderer = GetComponent<MeshRenderer>();

		ResetTimers();
	}

	void Update ()
    {
		UpdateState();
	}

    // Public methods.
    public void PerformStartGameAnimation()
    {
        float delay = Random.Range(1.0f, 1.5f);

        iTween.MoveFrom(gameObject, iTween.Hash("y", -10, "time", 3, "delay", delay));
        iTween.FadeFrom(gameObject, iTween.Hash("a", 0, "time", 2, "delay", delay, "includechildren", false));

        performedStartGameAnimation = true;
    }

    // Private methods.
	private void ResetTimers()
    {
		timeToShake = Random.Range(10.0f, 40.0f);

		dropTimer = timeToDrop;

		timeToRise = Random.Range(3.0f, 6.0f);
	}

    private void UpdateState()
    {
        if (!performedStartGameAnimation)
        {
            return;
        }

		if (isSteady)
        {
			timeToShake -= Time.deltaTime;

			if (timeToShake < 0)
            {
				isSteady = false;

				IsShaking = true;	
			}

			return;
		}

		if (IsShaking)
        {
			transform.position = new Vector3(0, Random.Range(-0.5f, 0.0f), 0);

			dropTimer -= Time.deltaTime;

			if (dropTimer < 0)
            {
				IsShaking = false;

				IsDropped = true;

				iTween.MoveTo(gameObject, new Vector3(0, -dropDistance, 0), 2);

				iTween.FadeTo(gameObject, 0, 0.5f);
			}

			return;
		}

		if (IsDropped)
        {
			timeToRise -= Time.deltaTime;

			if (timeToRise < 0)
            {
				IsDropped = false;

				isSteady = true;

				ResetTimers();

				iTween.MoveTo(gameObject, new Vector3(0,0,0), 2);
				iTween.FadeTo(gameObject, 1, 0.8f);
			}

			return;
		}
	}
}