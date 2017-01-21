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
        // pick platform
        int index = (int)(Random.value * platformManager.Platforms.Count);

        GameObject platform = platformManager.Platforms[index];

        // pick position offset
        float angle = 0f;

        float distance = 10f;

        // instantiate
        GameObject newGem = GameObject.Instantiate(GemPrefab);

        newGem.transform.position = new Vector3(0f, 0f, 0f);
    }
}
