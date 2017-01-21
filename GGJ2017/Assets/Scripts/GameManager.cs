using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerGameObjects
{
    public GameObject PlayerObject;

    public GameObject CrystalObject;
}

public class GameManager : MonoBehaviour
{
    // Public vars.
    public GameObject CrystalPrefab;

    public GameObject PlayerPrefab;

    public GameObject LevelObject;

    // Private vars.
    private SpawnPoints spawnPoints;

    private PlayerGameObjects[] activePlayersGameObjects = new PlayerGameObjects[4];

    // Unity callbacks.
    void Start ()
    {
        spawnPoints = LevelObject.GetComponent<SpawnPoints>();

        // TODO: Decide how many players are playing.
        StartGame(4);
    }

    // Public methods.
    public void StartGame(int numberOfPlayers)
    {
        GameObject[] spawnPointsToUse =
            numberOfPlayers == 2 ? spawnPoints.twoPlayers :
            numberOfPlayers == 3 ? spawnPoints.threePlayers :
            numberOfPlayers == 4 ? spawnPoints.fourPlayers :
            null;

        for (int i = 0; i < numberOfPlayers; ++i)
        {
            // Spawn.
            GameObject newCrystal = GameObject.Instantiate(CrystalPrefab);

            GameObject newPlayer = GameObject.Instantiate(PlayerPrefab);

            // Position and parent.
            newCrystal.transform.position = spawnPointsToUse[i].transform.position;

            newCrystal.transform.parent = spawnPointsToUse[i].transform;

            // Set indices.
            newCrystal.GetComponent<CrystalInfo>().PlayerIndex = i;

            PlayerInfo playerInfo = newPlayer.GetComponent<PlayerInfo>();

            playerInfo.PlayerIndex = i;

            // Set spawn point and spawn player.
            PlayerRespawn playerRespawn = newPlayer.GetComponent<PlayerRespawn>();

            playerRespawn.RespawnPoint = spawnPointsToUse[i];

            playerRespawn.Respawn();

            // Listen for battery charge drained event (loss condition).
            Battery newBattery = newCrystal.GetComponent<Battery>();

            newBattery.ChargeDrainedEvent += OnChargeDrained;

            // Keep track of active players.
            activePlayersGameObjects[i].PlayerObject = newPlayer;
            activePlayersGameObjects[i].CrystalObject = newCrystal;
        }
    }

    public void EndGame()
    {
        // dont know yet
    }

    // Private methods.
    private void OnChargeDrained(object sender, CrystalInfo crystalInfo)
    {
        PermaKill(crystalInfo.PlayerIndex);
    }

    private void PermaKill(int playerIndex)
    {
        activePlayersGameObjects[playerIndex].PlayerObject.SetActive(false);
        activePlayersGameObjects[playerIndex].CrystalObject.SetActive(false);
    }
}
