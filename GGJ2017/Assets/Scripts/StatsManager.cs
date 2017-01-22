using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {

	public GameObject[] playerGui = new GameObject[4];

	private GameManager gameManager;



	public void Initialise()
	{
		gameManager = GetComponent<GameManager>();
		for (int i = 0; i < gameManager.ActivePlayersGameObjects.Length; i++) {

			Battery battery = gameManager.ActivePlayersGameObjects[i].CrystalObject.GetComponent<Battery>();
			battery.ChargeValueChangedEvent += OnChargeValueChanged;

		}

	}

	private void OnChargeValueChanged(object sender, float chargeValue, int playerIndex)
	{
		UpdateCrystalHealth(playerIndex, chargeValue);
	}


	public void UpdateCrystalHealth(int playerIndex, float crystalHealth)
	{
		Text healthText = playerGui[playerIndex].transform.Find("Health").GetComponent<Text>();
		healthText.text = crystalHealth.ToString();
	}

	public void UpdateGemCount(int playerIndex, int gemCount)
	{

	}


}