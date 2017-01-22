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

    public void StopRumble()
    {
        GamePad.SetVibration(playerController.GamepadIndex, 0f, 0f);
    }
}
