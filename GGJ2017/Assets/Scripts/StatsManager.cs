using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour {

	public GameObject playerBlue;
	public GameObject playerGreen;
	public GameObject playerRed;
	public GameObject playerYellow;

	private GameManager gameManager;

	public void Initialise()
	{
		gameManager = GetComponent<GameManager>();
		for (int i = 0; i < gameManager.ActivePlayersGameObjects.Length; i++) {
//			print(gameManager.ActivePlayersGameObjects[i].PlayerObject.GetComponent<Inventory>().Count("Gem"));
			print(gameManager.ActivePlayersGameObjects[i].CrystalObject.GetComponent<Battery>().ChargeValue);
		}
	}



}