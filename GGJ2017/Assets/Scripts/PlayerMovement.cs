using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

public class PlayerMovement : MonoBehaviour
{
    // Public vars.
    public float moveSpeed = 10f;

    public bool useKeyboardControls = false;

    // Static (global) vars. Don't use globals, except for the GLOBAL GAME JAM!!!
    public static bool[] ClaimedGamepadIndices = new bool[] { false, false, false, false };

    // Input vars.
    private bool gamepadIndexSet = false;

    private PlayerIndex gamepadIndex;

    private GamePadState state;

    private GamePadState prevState;
	
	// Update is called once per frame
	void Update ()
    {
		HandleInput();
	}

	void HandleInput()
    {
        Vector3 direction = Vector3.zero;

        if (useKeyboardControls)
        {
            // Drop the gamepad index.
            if (gamepadIndexSet)
            {
                Debug.Log(string.Format("{0} dropping gamepad {1} due to enabling keyboard controls", gameObject.name, gamepadIndex));

                ClaimedGamepadIndices[(int)gamepadIndex] = false;

                gamepadIndexSet = false;
            }

            direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        }
        // Gamepad controls.
        else
        {
            // If gamepad disconnects, drop the index.
            if (gamepadIndexSet && !prevState.IsConnected)
            {
                Debug.Log(string.Format("{0} dropping gamepad {1} due to disconnection", gameObject.name, gamepadIndex));

                ClaimedGamepadIndices[(int)gamepadIndex] = false;

                gamepadIndexSet = false;
            }

            // If no index is set, claim one.
            if (!gamepadIndexSet)
            {
                for (int i = 0; i < 4; ++i)
                {
                    PlayerIndex testPlayerIndex = (PlayerIndex)i;

                    GamePadState testState = GamePad.GetState(testPlayerIndex);

                    // Make sure controller is connected and not claimed yet.
                    if (testState.IsConnected 
                        && ClaimedGamepadIndices[i] == false)
                    {
                        Debug.Log(string.Format("{0} using gamepad {1}", gameObject.name, testPlayerIndex));

                        gamepadIndex = testPlayerIndex;

                        gamepadIndexSet = true;

                        ClaimedGamepadIndices[i] = true;

                        break;
                    }
                }
            }

            // If index is set, use gamepad!
            if (gamepadIndexSet)
            {
                prevState = state;

                state = GamePad.GetState(gamepadIndex);

                direction = new Vector3(state.ThumbSticks.Left.X, 0f, state.ThumbSticks.Left.Y);
            }
        }

        // Perform movement.
        direction.Normalize();

        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }
}	
