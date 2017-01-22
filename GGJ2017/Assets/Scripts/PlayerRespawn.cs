using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // Static global vars.
    public static Vector3 __global_PlayerSpawnOffset = new Vector3(2f, 0f, 2f);

    // Events.
    public delegate void PlayerWaitingForRespawnHandler(object sender, GameObject gameObject, float respawnTime);
    public event PlayerWaitingForRespawnHandler PlayerWaitingForRespawnEvent;

    // Public vars.
    public GameObject RespawnPoint;

    public bool IsRespawning = false;
    
    // Private vars.
    private float respawnTime = 5.0f;

    private Rumbler rumbler;

    // Unity callbacks.
    void Awake()
    {
        rumbler = GetComponent<Rumbler>();
    }

    void OnCollisionEnter(Collision other)
    {
		if (other.gameObject.tag == "Death")
        {
            Debug.Log(gameObject.name + " just fell to their death.");

            rumbler.StopRumble();

            gameObject.SetActive(false);

            Invoke("Respawn", respawnTime);

            IsRespawning = true;

            if (PlayerWaitingForRespawnEvent != null)
            {
                PlayerWaitingForRespawnEvent(this, gameObject, respawnTime);
            }
        }
 	}

    // Public methods.
    public void Respawn()
    {
        IsRespawning = false;

        gameObject.SetActive(true);

        // The magic number here is a percentage of the vector between the 
        // origin and the crystal spawn position. This places the player 
        // between the crystal and the origin.
        gameObject.transform.position = RespawnPoint.transform.position * 0.92f;
    }
 }