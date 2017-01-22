using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    // Static (global) vars. Don't use globals, except for the GLOBAL GAME JAM!!!
    public static List<Battery> __global_Batteries = new List<Battery>();

    // Events.
    public delegate void ChargeValueChangedHandler(object sender, float chargeValue);
    public event ChargeValueChangedHandler ChargeValueChangedEvent;

    public delegate void ChargeDrainedHandler(object sender, CrystalInfo crystalInfo);
    public event ChargeDrainedHandler ChargeDrainedEvent;

    // Public vars.
    public float ChargeValue { get { return chargeValue; } }

    public bool IsFullyCharged
    {
        get
        {
            return chargeValue >= 1f;
        }
    }

    // Private vars.
    private CrystalInfo crystalInfo;

    private float chargeValue = 1f;

    // Unity callbacks.
    void Start()
    {
        if (!__global_Batteries.Contains(this))
        {
            __global_Batteries.Add(this);
        }

        crystalInfo = GetComponent<CrystalInfo>();
    }
    
    void OnDestroy()
    {
        if (__global_Batteries.Contains(this))
        {
            __global_Batteries.Remove(this);
        }
    }

    // Public methods.
    public void Charge(float percentage)
    {
        if (chargeValue >= 1f)
        {
            return;
        }

        chargeValue += percentage;

        // Don't go over 100%.
        chargeValue = Mathf.Min(chargeValue, 1f);

        ChargeValueChangedEvent(this, chargeValue);
    }

    public void Drain(float percentage)
    {
        if (chargeValue <= 0f)
        {
            return;
        }

        chargeValue -= percentage;

        // Don't go under 0%.
        chargeValue = Mathf.Max(chargeValue, 0f);

        ChargeValueChangedEvent(this, chargeValue);

        if (chargeValue <= 0f)
        {
            ChargeDrainedEvent(this, crystalInfo);
        }
    }

    public bool IsFriendlyWith(PlayerInfo somePlayerInfo)
    {
        if (somePlayerInfo
            && somePlayerInfo.PlayerIndex == crystalInfo.PlayerIndex)
        {
            return true;
        }

        return false;
    }

    // Static (global) methods.
    public static Battery FindNearestBatteryWithinDistance(Vector3 position, float distance)
    {
        float shortestDistance = Mathf.Infinity;

        Battery result = null;

        foreach (Battery b in __global_Batteries)
        {
            float d = Vector3.Distance(b.transform.position, position);

            if (b.gameObject.activeSelf
                && d < distance 
                && d < shortestDistance)
            {
                shortestDistance = d;

                result = b;
            }
        }

        return result;
    }
}
