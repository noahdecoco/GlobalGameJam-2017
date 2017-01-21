using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public float ChargeDistance = 2f;

    public float ChargePercentagePerSecond = 0.25f;

    public float DrainDistance = 2f;

    public float DrainPercentagePerSecond = 0.25f;

    private PlayerInfo playerInfo;

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
    }

    public void Interact()
    {
        if (!TryCharging())
        {
            TryDraining();
        }
    }

    // If in range of a battery and it's friendly, charge it.
    private bool TryCharging()
    {
        Battery nearestBattery = Battery.FindNearestBatteryWithinDistance(transform.position, ChargeDistance);

        if (nearestBattery 
            && nearestBattery.IsFriendlyWith(playerInfo))
        {
            nearestBattery.Charge(ChargePercentagePerSecond * Time.deltaTime);

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

            return true;
        }

        return false;
    }
}
