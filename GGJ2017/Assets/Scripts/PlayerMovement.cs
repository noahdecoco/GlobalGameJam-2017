using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

public class PlayerMovement : MonoBehaviour
{
    // Public vars.
    public float moveSpeed = 10f;

    public bool useKeyboardControls = false;

    // Static (global) vars. Don't use globals, except for the GLOBAL GAME JAM!!!
    public static bool[] ClaimedPlayerIndices = new bool[] { false, false, false, false };

    // Input vars.
    private bool playerIndexSet = false;

    private PlayerIndex playerIndex;

    private GamePadState state;

    private GamePadState prevState;

    // Movement vars.
	private bool _isGrounded = true;

	private Rigidbody _rigidBody;

	void Start()
    {
		_rigidBody = GetComponent<Rigidbody>();
	}

	void OnTriggerEnter(Collider other)
    {
		if (other.tag == "MovingPlatform")
        {
			_isGrounded = true;
		}
	}

	void OnTriggerExit(Collider other)
    {
		if (other.tag == "MovingPlatform")
        {
			_isGrounded = false;
		}
	}
	
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
            if (playerIndexSet)
            {
                Debug.Log(string.Format("{0} dropping gamepad {1}", gameObject.name, playerIndex));

                ClaimedPlayerIndices[(int)playerIndex] = false;

                playerIndexSet = false;
            }

            direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        }
        // Gamepad controls.
        else
        {
            // If gamepad disconnects, drop the index.
            if (playerIndexSet && !prevState.IsConnected)
            {
                Debug.Log(string.Format("{0} dropping gamepad {1} due to disconnection", gameObject.name, playerIndex));

                ClaimedPlayerIndices[(int)playerIndex] = false;

                playerIndexSet = false;
            }

            // If no index is set, claim one.
            if (!playerIndexSet)
            {
                for (int i = 0; i < 4; ++i)
                {
                    PlayerIndex testPlayerIndex = (PlayerIndex)i;

                    GamePadState testState = GamePad.GetState(testPlayerIndex);

                    // Make sure controller is connected and not claimed yet.
                    if (testState.IsConnected 
                        && ClaimedPlayerIndices[i] == false)
                    {
                        Debug.Log(string.Format("{0} using gamepad {1}", gameObject.name, testPlayerIndex));

                        playerIndex = testPlayerIndex;

                        playerIndexSet = true;

                        ClaimedPlayerIndices[i] = true;

                        break;
                    }
                }
            }

            // If index is set, use gamepad!
            if (playerIndexSet)
            {
                prevState = state;

                state = GamePad.GetState(playerIndex);

                direction = new Vector3(state.ThumbSticks.Left.X, 0f, state.ThumbSticks.Left.Y);
            }
        }

        // Perform movement.
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    
}	
