using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    // Events.
    public delegate void PlayerIndexChangedHandler(object sender, int playerIndex);
    public event PlayerIndexChangedHandler PlayerIndexChangedEvent;

    // Public vars.
    public int PlayerIndex
    {
        get
        {
            return playerIndex;
        }

        set
        {
            playerIndex = value;

            PlayerIndexChangedEvent(this, playerIndex);
        }
    }

    private int playerIndex = -1;
}
