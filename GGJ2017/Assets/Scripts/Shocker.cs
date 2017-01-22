using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shocker : MonoBehaviour
{
    // Public vars.
    public GameObject Shockwave;

    public int ShockwaveCostInGems = 5;

    // Private vars.
    private float rechargeTime = 3.0f;

    // Unity callbacks.
    void Start ()
    {
		
	}
	
	void Update ()
    {
        if (rechargeTime < 0)
        {
            rechargeTime = 0;
        }
        else
        {
            rechargeTime -= Time.deltaTime;
        }
    }

    // Public methods.
    public bool CanShock(Inventory inventory)
    {
        return rechargeTime <= 0f
            && inventory.Count("Gem") >= ShockwaveCostInGems;
    }

    public void Shock(Inventory inventory)
    {
        inventory.Remove("Gem", ShockwaveCostInGems);

        var shockwave = Instantiate(Shockwave, gameObject.transform.position, Quaternion.identity);

        shockwave.GetComponent<Shockwave>().SetCaster(gameObject);
        shockwave.GetComponent<Shockwave>().Blast();

        rechargeTime = 3.0f;
    }
}
