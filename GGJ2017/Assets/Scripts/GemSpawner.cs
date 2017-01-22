using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    // Public vars.
    public int MaxGemCount = 100;

    public float AverageTimeBetweenGemWaves = 10f;

    public float VariationInTimeBetweenGemWaves = 1f;

    public GameObject GemPrefab;

    // Private vars.
    private List<GameObject> activeGems = new List<GameObject>();

    private PlatformManager platformManager;

    private float[] ringDistances = new float[6]
    {
        0f, // ring 1 obsolete
        6.609f,
        9.697f,
        13.240f,
        16.945f,
        21.161f
    };

    private float[] ringThicknesses = new float[6]
    {
        0f, // ring 1 obsolete
        2.023f,
        4.154f,
        2.920f,
        4.501f,
        3.942f
    };

    // Unity callbacks.
    void Start()
    {
        platformManager = GetComponent<PlatformManager>();

        SpawnGemWave();
    }

    // Private methods.
    private void SpawnGemWave()
    {
        int newGemCount = MaxGemCount - activeGems.Count;

        for (int i = 0; i < newGemCount; ++i)
        {
            SpawnGem();
        }

        float timeUntilNextGemWave = AverageTimeBetweenGemWaves + Random.Range(-VariationInTimeBetweenGemWaves, VariationInTimeBetweenGemWaves);

        Invoke("SpawnGemWave", timeUntilNextGemWave);
    }

    private void SpawnGem()
    {
        // Pick platform.
        int index = (int)(Random.value * platformManager.Platforms.Count);

        GameObject platform = platformManager.Platforms[index];

        // Calculate position offset.
        GameObject ringObject = platform.transform.parent.gameObject;

        int ring = int.Parse(ringObject.name[ringObject.name.Length - 1].ToString());

        float angleVariation = ring <= 2 ? (360f / 8f) : (360f / 16f);

        float angleVariationPercentage = Random.Range(0.05f, 0.95f);

        float angle = -90f - float.Parse(platform.name) - (angleVariation * angleVariationPercentage);

        float distanceVariation = Random.Range(-0.45f, 0.45f);

        float distance = ringDistances[ring - 1] + (distanceVariation * ringThicknesses[ring - 1]);

        var x = distance * Mathf.Cos(angle * Mathf.Deg2Rad);

        var z = distance * Mathf.Sin(angle * Mathf.Deg2Rad);

        // Spawn gem.
        GameObject newGem = GameObject.Instantiate(GemPrefab);

        newGem.transform.position = new Vector3(x, platform.transform.position.y, z);

        newGem.transform.parent = platform.transform;

        // Listen for picked up event.
        Pickup.PickedUpEvent += OnPickedUp;

        // Keep track.
        activeGems.Add(newGem);
    }

    private void OnPickedUp(object sender, GameObject gameObject, string pickupName, int pickupCount)
    {
        if (pickupName == "Gem"
            && activeGems.Contains(gameObject))
        {
            activeGems.Remove(gameObject);
        }
    }
}
