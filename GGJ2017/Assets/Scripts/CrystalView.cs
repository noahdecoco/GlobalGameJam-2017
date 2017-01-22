using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalView : MonoBehaviour
{
    // Static vars.
    private static Color[] crystalColors = new Color[]
    {
        new Color(0f, 0f, 1f, 1f), // Blue
        new Color(0f, 1f, 0f, 1f), // Green
        new Color(1f, 0f, 0f, 1f), // Red
        new Color(1f, 1f, 0f, 1f)  // Yellow
    };

    private static float crystalLightBaseIntensity = 0.5f;

    private static float crystalLightMaxExtraIntensity = 1.5f;

    private static float crystalLightBaseRange = 5f;

    private static float crystalLightMaxExtraRange = 15f;

    private static float crystalBaseScale = 1.908349f;

    private static float crystalExtraScale = 0.5f;

    // Private vars.
    private Battery battery;

    private Material crystalMaterial;

    private Light crystalLight;

    private ParticleSystem crystalSplosion;

    private ParticleSystem crystalFlares;

    private Transform offsets;

    public GameObject explosionPrefab;
    private GameObject explosionInst;
    private bool isDead = false;

    // Unity callbacks.
    void Start ()
    {
        // Listen for battery events.
        battery = GetComponent<Battery>();
        
        battery.ChargeValueChangedEvent += OnChargeValueChanged;
        battery.ChargeDrainedEvent += OnChargeDrained;

        // Get access to the offsets transform and the crystal material.
        offsets = transform.Find("Offsets");

        Transform crystalTransform = offsets.Find("chrystal");

        Debug.Assert(crystalTransform != null);

        crystalMaterial = crystalTransform.GetComponent<Renderer>().materials[1];

        Debug.Assert(crystalMaterial != null);

        // Set color based on color index.
        CrystalInfo crystalInfo = GetComponent<CrystalInfo>();

        crystalMaterial.color = crystalColors[crystalInfo.PlayerIndex];

        crystalLight = transform.GetComponentInChildren<Light>();
        
        crystalSplosion = transform.FindChild("splosion").GetComponent<ParticleSystem>();

        crystalFlares = transform.FindChild("flares").GetComponent<ParticleSystem>();

        crystalMaterial.color = new Color(
             crystalMaterial.color.r,
             crystalMaterial.color.g,
             crystalMaterial.color.b,
             crystalMaterial.color.a);

        crystalLight.color = new Color(
            crystalMaterial.color.r,
            crystalMaterial.color.g,
            crystalMaterial.color.b,
            crystalMaterial.color.a);

        crystalSplosion.startColor = new Color(
            crystalMaterial.color.r,
            crystalMaterial.color.g,
            crystalMaterial.color.b,
            crystalMaterial.color.a);

        crystalFlares.startColor = new Color(
            crystalMaterial.color.r,
            crystalMaterial.color.g,
            crystalMaterial.color.b,
            crystalMaterial.color.a);

        // Set light properties.
        crystalLight.intensity = crystalLightBaseIntensity + (crystalLightMaxExtraIntensity * 1f);

        crystalLight.range = crystalLightBaseRange + (crystalLightMaxExtraRange * 1f);

        // Set model scale.
        var scale = crystalBaseScale + (crystalExtraScale * 1f);

        offsets.localScale = new Vector3(scale, scale, scale);
    }

    // Private methods.
    private void OnChargeValueChanged(object sender, float chargeValue)
    {
        // TODO: Insert more code affecting how crystal looks here.

        // Temporary effect until artists change this - sets material alpha equal to charge value.
        crystalMaterial.color = new Color(
            crystalMaterial.color.r, 
            crystalMaterial.color.g, 
            crystalMaterial.color.b, 
            chargeValue);
        
        crystalFlares.startColor = new Color(
            crystalMaterial.color.r,
            crystalMaterial.color.g,
            crystalMaterial.color.b,
            chargeValue);

        crystalSplosion.startColor = new Color(
            crystalMaterial.color.r,
            crystalMaterial.color.g,
            crystalMaterial.color.b,
            chargeValue);

        // Set light properties.
        crystalLight.intensity = crystalLightBaseIntensity + (crystalLightMaxExtraIntensity * chargeValue);

        crystalLight.range = crystalLightBaseRange + (crystalLightMaxExtraRange * chargeValue);

        // Set model scale.
        var scale = crystalBaseScale + (crystalExtraScale * chargeValue);

        offsets.localScale = new Vector3(scale, scale, scale);
    }

    private void OnChargeDrained(object sender, CrystalInfo crystalInfo)
    {
        // TODO: Some kind of spectacular explosion.

        if (isDead == true) return;

        isDead = true;
        explosionInst = (GameObject)Instantiate(explosionPrefab, transform.position + new Vector3(0,2,0), Quaternion.identity);
        Invoke("DestoyExplosion", 3f);
    }

    void DestoyExplosion()
    {
        Destroy(explosionInst);
    }
}
