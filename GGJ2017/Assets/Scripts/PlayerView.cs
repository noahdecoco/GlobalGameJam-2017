using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    // Public vars.
    public GameObject[] WizardPrefabs = new GameObject[4];

    // Private vars.
    private PlayerInfo playerInfo;

    // Unity callbacks.
    void Awake()
    {
        playerInfo = GetComponent<PlayerInfo>();

        playerInfo.PlayerIndexChangedEvent += OnPlayerIndexChanged;
    }

    // Private methods.
    private void OnPlayerIndexChanged(object sender, int playerIndex)
    {
        SetCharacterModel(playerIndex);
    }

    private void SetCharacterModel(int playerIndex)
    {
        GameObject characterObject = GameObject.Instantiate(WizardPrefabs[playerIndex]);

        characterObject.transform.parent = transform;

        Debug.Log(gameObject.name + " is " + WizardPrefabs[playerIndex].name);
    }
}
