using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalView : MonoBehaviour
{
    // Private vars.
    private Battery battery;

    private Material crystalMaterial;

	void Start ()
    {
        battery = GetComponent<Battery>();
        
        battery.ChargeValueChangedEvent += OnChargeValueChanged;

        crystalMaterial = transform.FindChild("chrystal").GetComponent<Material>();
    }

    void OnChargeValueChanged(object sender, float chargeValue)
    {
        // TODO: Insert more code affecting how crystal looks here.

        // Temporary effect until artists change this - sets material alpha equal to charge value.
        crystalMaterial.color = new Color(
            crystalMaterial.color.r, 
            crystalMaterial.color.g, 
            crystalMaterial.color.b, 
            chargeValue);
    }
}
