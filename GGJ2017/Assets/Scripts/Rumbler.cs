using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#

public class Rumbler : MonoBehaviour
{
    private PlayerController playerController;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void SetRumble(float lowRumble, float highRumble)
    {
        GamePad.SetVibration(playerController.GamepadIndex, lowRumble, highRumble);
    }

    public void SetFadingRumbleOverTime(float lowRumble, float highRumble, float duration)
    {
        StopAllCoroutines();

        if (gameObject.activeSelf)
        {
            StartCoroutine(FadeRumble(lowRumble, highRumble, duration));
        }
    }

    private IEnumerator FadeRumble(float lowRumble, float highRumble, float duration)
    {
        float percentage = 0f;

        float rateOfChange = 1f / duration;

        while (percentage < 1f)
        {
            GamePad.SetVibration(playerController.GamepadIndex, lowRumble * percentage, highRumble * percentage);

            percentage += Time.deltaTime * rateOfChange;

            yield return 0;
        }

        StopRumble();
    }

    public void StopRumble()
    {
        GamePad.SetVibration(playerController.GamepadIndex, 0f, 0f);
    }
}
