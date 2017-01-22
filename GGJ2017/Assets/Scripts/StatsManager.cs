using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {

	public GameObject[] playerGui = new GameObject[4];

    public GameObject startGUI;

	private GameManager gameManager;

    public void Initialize()
    {
        startGUI.SetActive(true);
    }
    
	public void StartGame()
    {
        startGUI.SetActive(false);

        gameManager = GetComponent<GameManager>();

		for (int i = 0; i < gameManager.ActivePlayersGameObjects.Length; i++)
        {
			Battery battery = gameManager.ActivePlayersGameObjects[i].CrystalObject.GetComponent<Battery>();
			battery.ChargeValueChangedEvent += OnChargeValueChanged;

			Inventory inventory = gameManager.ActivePlayersGameObjects[i].PlayerObject.GetComponent<Inventory>();
			inventory.GemValueChangedEvent += OnGemValueChanged;
		}
	}

	private void OnChargeValueChanged(object sender, float chargeValue, int playerIndex)
	{
		UpdateCrystalHealth(playerIndex, chargeValue);
	}

	private void OnGemValueChanged(object sender, int gemValue, int playerIndex)
	{
		UpdateGemCount(playerIndex, gemValue);
	}

	public void UpdateCrystalHealth(int playerIndex, float crystalHealth)
	{
//		Text healthText = playerGui[playerIndex].transform.Find("Health").GetComponent<Text>();
//		healthText.text = crystalHealth.ToString();

		Image healthBar = playerGui[playerIndex].transform.Find("HealthBar").GetComponent<Image>();
		healthBar.fillAmount = crystalHealth;
	}

	public void UpdateGemCount(int playerIndex, int gemValue)
	{
		Text gemText = playerGui[playerIndex].transform.Find("Gems").GetComponent<Text>();
		gemText.text = gemValue.ToString();

	}
}