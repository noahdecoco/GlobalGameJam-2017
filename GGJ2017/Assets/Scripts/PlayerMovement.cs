using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

public class PlayerMovement : MonoBehaviour
{
    // Public vars.
    public float moveSpeed = 10f;

    public bool useKeyboardControls = false;

    // Static (global) vars. Don't use globals, except for the GLOBAL GAME JAM!!!
    public static bool[] __global_ClaimedGamepadIndices = new bool[] { false, false, false, false };

    // Input vars.
    private bool gamepadIndexSet = false;

    private PlayerIndex gamepadIndex;

    private GamePadState state;

    private GamePadState prevState;

    // Component reference vars.
    private Interactor interactor;

    // Unity callbacks.
    void Start()
    {
        interactor = GetComponent<Interactor>();
    }
	
	void Update ()
    {
		HandleInput();
	}

    // Private methods.
    private void HandleInput()
    {
        if (useKeyboardControls)
        {
            HandleKeyboardInput();
        }
        // Gamepad controls.
        else
        {
            HandleGamepadInput();
        }
    }

    private void HandleKeyboardInput()
    {
        // Drop the gamepad index.
        if (gamepadIndexSet)
        {
            Debug.Log(string.Format("{0} dropping gamepad {1} due to enabling keyboard controls", gameObject.name, gamepadIndex));

            __global_ClaimedGamepadIndices[(int)gamepadIndex] = false;

            gamepadIndexSet = false;
        }

        // Move with WASD.
        var direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        PerformMovement(direction);

        // Interact with space bar.
        if (Input.GetKey(KeyCode.Space))
        {
            PerformInteraction();
        }
    }

    private void HandleGamepadInput()
    {
        // If gamepad disconnects, drop the index.
        if (gamepadIndexSet && !prevState.IsConnected)
        {
            Debug.Log(string.Format("{0} dropping gamepad {1} due to disconnection", gameObject.name, gamepadIndex));

            __global_ClaimedGamepadIndices[(int)gamepadIndex] = false;

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
                    && __global_ClaimedGamepadIndices[i] == false)
                {
                    Debug.Log(string.Format("{0} using gamepad {1}", gameObject.name, testPlayerIndex));

                    gamepadIndex = testPlayerIndex;

                    gamepadIndexSet = true;

                    __global_ClaimedGamepadIndices[i] = true;

                    break;
                }
            }
        }

        // If index is set, use gamepad!
        if (gamepadIndexSet)
        {
            // Keep track of previous and current gamepad state.
            prevState = state;

            state = GamePad.GetState(gamepadIndex);

            // Move with left thumbstick.
            var direction = new Vector3(state.ThumbSticks.Left.X, 0f, state.ThumbSticks.Left.Y);

            PerformMovement(direction);

            // Interact with A button.
            if (state.Buttons.A == ButtonState.Pressed)
            {
                PerformInteraction();
            }
        }
    }

    private void PerformMovement(Vector3 direction)
    {
        direction.Normalize();

        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    // Drain/Charge interaction
    private void PerformInteraction()
    {
        interactor.Interact();
    }
}	
