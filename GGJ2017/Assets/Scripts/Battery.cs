using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    // Events.
    public delegate void ChargeValueChangedHandler(object sender, float chargeValue);
    public event ChargeValueChangedHandler ChargeValueChangedEvent;

    // Public vars.
    public float ChargeValue { get { return chargeValue; } }

    // Private vars.
    private float chargeValue = 1f;

    public void Charge(float percentage)
    {
        chargeValue += percentage;

        // Don't go over 100%.
        chargeValue = Mathf.Min(chargeValue, 1f);

        ChargeValueChangedEvent(this, chargeValue);
    }

    public void Drain(float percentage)
    {
        chargeValue -= percentage;

        // Don't go under 0%.
        chargeValue = Mathf.Max(chargeValue, 0f);

        ChargeValueChangedEvent(this, chargeValue);
    }
}
