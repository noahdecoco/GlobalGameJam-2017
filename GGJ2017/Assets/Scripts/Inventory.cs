using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    public int Count(string name)
    {
        return inventory[name];
    }

    public void Add(string name, int amount)
    {
        if (inventory.ContainsKey(name))
        {
            inventory[name] += amount;
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
