using UnityEngine;
using System.Collections;

public class PlayerGrounding : MonoBehaviour
{
    private Rumbler rumbler;

    void Awake()
    {
        rumbler = GetComponent<Rumbler>();
    }

    private void OnCollisionEnter(Collision other)
    {
		if (other.gameObject.tag == "MovingPlatform")
        {
			transform.parent = other.transform;

            Platform platform = other.gameObject.GetComponent<Platform>();

            if (platform.IsShaking)
            {
                rumbler.SetRumble(platform.DropTimerPercentageElapsed, platform.DropTimerPercentageElapsed);
            }
            else
            {
                rumbler.StopRumble();
            }
		}
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            Platform platform = other.gameObject.GetComponent<Platform>();

            if (platform.IsDropped)
            {
                rumbler.StopRumble();
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
		if (other.gameObject.tag == "MovingPlatform")
        {
			if (transform.parent == other.transform)
            {
				transform.parent = null;

                rumbler.StopRumble();
            }
		}
 	}
}