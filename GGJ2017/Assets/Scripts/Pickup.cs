using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Events.
    public delegate void PickedUpHandler(object sender, GameObject gameObject, string pickupName, int pickupCount);
    public static event PickedUpHandler PickedUpEvent;

    // Public vars.
    public string PickupName;

    public int PickupCount;

    public GameObject HoverTextPrefab;
    
    // Unity callbacks.
	void OnTriggerEnter(Collider collider)
    {
        Inventory inventory = collider.gameObject.GetComponent<Inventory>();

        if (inventory != null)
        {
            inventory.Add(PickupName, PickupCount);

            int pickupTotalCount = inventory.Count(PickupName);

            HoverText hoverText = GameObject.Instantiate(HoverTextPrefab).GetComponent<HoverText>();

            hoverText.transform.position = transform.position;
            hoverText.HoverTextDuration = 0.5f;
            hoverText.SetText(pickupTotalCount.ToString());

            GameObject.Destroy(gameObject);

            PickedUpEvent(this, gameObject, PickupName, PickupCount);
        }
	}
}
