using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // Static global vars.
    public static Vector3 __global_PlayerSpawnOffset = new Vector3(2f, 0f, 2f);

    // Public vars.
    public GameObject respawnPoint;
    
    // Private vars.
    private float respawnTime = 5.0f;

	private bool isDead = false;
    
    // Unity callbacks.
	void OnCollisionEnter(Collision other)
    {
		if (other.gameObject.tag == "Death")
        {
            Debug.Log(gameObject.name + " just fell to their death.");

			isDead = true;

            gameObject.SetActive(false);

            Invoke("Respawn", respawnTime);
        }
 	}

    // Private methods.
    private void Respawn()
    {
        gameObject.SetActive(true);

        isDead = false;

        // The magic number here is a percentage of the vector between the 
        // origin and the crystal spawn position. This places the player 
        // between the crystal and the origin.
        gameObject.transform.position = respawnPoint.transform.position * 0.92f;
    }
 }