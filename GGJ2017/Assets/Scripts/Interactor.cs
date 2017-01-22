using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public float ChargeDistance = 5f;

    public float ChargePercentagePerSecond = 0.25f;

    public float DrainDistance = 5f;

    public float DrainPercentagePerSecond = 0.25f;

    private PlayerInfo playerInfo;

    private Rumbler rumbler;

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();

        rumbler = GetComponent<Rumbler>();
    }

    public void Interact()
    {
        if (!TryCharging())
        {
            if (!TryDraining())
            {
                rumbler.StopRumble();
            }
        }
    }

    public void StopInteract()
    {
        rumbler.StopRumble();
    }

    // If in range of a battery and it's friendly, charge it.
    private bool TryCharging()
    {
        Battery nearestBattery = Battery.FindNearestBatteryWithinDistance(transform.position, ChargeDistance);

        if (nearestBattery 
            && nearestBattery.IsFriendlyWith(playerInfo))
        {
            nearestBattery.Charge(ChargePercentagePerSecond * Time.deltaTime);

            rumbler.SetRumble(0.25f, 0.15f);

            return true;
        }
        
        return false;
    }

    // If in range of a battery and it's not friendly, drain it.
    private bool TryDraining()
    {
        Battery nearestBattery = Battery.FindNearestBatteryWithinDistance(transform.position, ChargeDistance);

        if (nearestBattery 
            && !nearestBattery.IsFriendlyWith(playerInfo))
        {
            nearestBattery.Drain(DrainPercentagePerSecond * Time.deltaTime);

            rumbler.SetRumble(0.05f, 0.45f);

            return true;
        }

        return false;
    }
}
