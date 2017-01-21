using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    // Public vars.
    public List<GameObject> Platforms = new List<GameObject>();

	// Unity callbacks.
	void Start ()
    {
        // Populate list.
        GameManager gameManager = GetComponent<GameManager>();

        GameObject levelObject = gameManager.LevelObject;

        Platforms.AddRange(FindAllPlatformsRecursively(levelObject.transform));

        Debug.Log("Found platforms: " + Platforms.Count);
	}

    // Private methods.
    private List<GameObject> FindAllPlatformsRecursively(Transform t)
    {
        List<GameObject> platformsFound = new List<GameObject>();

        foreach (Transform child in t)
        {
            if (child.tag == "MovingPlatform")
            {
                platformsFound.Add(child.gameObject);
            }

            if (child.childCount > 0)
            {
                platformsFound.AddRange(FindAllPlatformsRecursively(child));
            }
        }

        return platformsFound;
    }
}
