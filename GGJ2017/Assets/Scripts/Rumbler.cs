using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#

public class Rumbler : MonoBehaviour
{
    public void SetRumble(PlayerIndex playerIndex, float lowRumble, float highRumble)
    {
        GamePad.SetVibration(playerIndex, lowRumble, highRumble);
    }

    public void StopRumble(PlayerIndex playerIndex)
    {
        GamePad.SetVibration(playerIndex, 0f, 0f);
    }
}
