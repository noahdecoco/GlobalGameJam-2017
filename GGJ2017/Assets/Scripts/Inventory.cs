using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Private vars.
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    // Unity callbacks.
    void Start()
    {
        inventory["Gem"] = 0;
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
        }
        else
        {
            inventory[name] = amount;
        }
    }

    public void Remove(string name, int amount)
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
