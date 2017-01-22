using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    // Public vars.
    public GameObject[] WizardPrefabs = new GameObject[4];

    public GameObject WizardModel;

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
        WizardModel = GameObject.Instantiate(WizardPrefabs[playerIndex]);

        WizardModel.transform.parent = transform;

        Debug.Log(gameObject.name + " is " + WizardPrefabs[playerIndex].name);
    }
}
