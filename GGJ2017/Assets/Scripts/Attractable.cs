using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractable : MonoBehaviour
{
    // Static (global) vars. Don't use globals, except for the GLOBAL GAME JAM!!!
    public static List<Attractable> __global_Attractables = new List<Attractable>();

    void Start ()
    {
        if (!__global_Attractables.Contains(this))
        {
            __global_Attractables.Add(this);
        }
    }

    void OnDestroy()
    {
        if (__global_Attractables.Contains(this))
        {
            __global_Attractables.Remove(this);
        }
    }
}
