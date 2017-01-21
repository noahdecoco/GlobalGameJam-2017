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
        Vector3 direction;

        if (useKeyboardControls)
        {
            direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        }
        // Gamepad controls.
        else
        {
            if (playerIndexSet && !prevState.IsConnected)
            {
                ClaimedPlayerIndices[(int)playerIndex] = false;

                playerIndexSet = false;
            }

            if (!playerIndexSet)
            {
                for (int i = 0; i < 4; ++i)
                {
                    PlayerIndex testPlayerIndex = (PlayerIndex)i;

                    GamePadState testState = GamePad.GetState(testPlayerIndex);

                    // Make sure controller is connected and not claimed yet.
                    if (testState.IsConnected && ClaimedPlayerIndices[i] == false)
                    {
                        Debug.Log(ClaimedPlayerIndices[i]);
                        Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));

                        playerIndex = testPlayerIndex;

                        playerIndexSet = true;

                        ClaimedPlayerIndices[i] = true;
                        Debug.Log(ClaimedPlayerIndices[i]);
                    }
                }
            }

            if (playerIndexSet)
            {
                prevState = state;

                state = GamePad.GetState(playerIndex);

                direction = new Vector3(state.ThumbSticks.Left.X, 0f, state.ThumbSticks.Left.Y);
            }
        }

        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    
}	
