using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Private vars.
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

	private PlayerInfo playerInfo;
	private int playerIndex;

    // Events.
	public delegate void GemValueChangedHandler(object sender, int gemValue, int playerIndex);
    public event GemValueChangedHandler GemValueChangedEvent;


    void Start() {
		playerInfo = GetComponent<PlayerInfo>();
		playerIndex = playerInfo.PlayerIndex;
    }


    // Public methods.
    public int Count(string name)
    {
        return inventory[name];
    }

    public void Add(string name, int amount)
    {
        if (inventory.ContainsKey(name))
        {
            inventory[name] += amount;

			if(name == "Gem") {
				GemValueChangedEvent(this, inventory[name], playerIndex);
			}
        }
        else
        {
            inventory[name] = amount;
        }
    }

    public void Drop(string name, int amount)
    {
        if (inventory.ContainsKey(name) && inventory[name] >= amount)
        {
            inventory[name] -= amount;
        }
    }

    public void Exchange(string name, int amount, Inventory otherInventory)
    {
        if (inventory.ContainsKey(name) && inventory[name] >= amount)
        {
            inventory[name] -= amount;

            otherInventory.Add(name, amount);
        }
    }
}
