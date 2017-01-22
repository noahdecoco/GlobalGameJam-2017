using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

public class PlayerController : MonoBehaviour
{
    // Public vars.
    public float moveSpeed = 10f;

    public bool useKeyboardControls = false;

    public float stunTime = 2f;

    public GameObject stunPrefab;

    public PlayerIndex GamepadIndex;

    // Static (global) vars. Don't use globals, except for the GLOBAL GAME JAM!!!
    public static bool[] __global_ClaimedGamepadIndices = new bool[] { false, false, false, false };

    // Input vars.
    private bool gamepadIndexSet = false;

    private GamePadState state;

    private GamePadState prevState;

    // Component reference vars.
    private Interactor interactor;
    
    private Rumbler rumbler;

    private Shocker shocker;

    private Inventory inventory;

    private PlayerView playerView;

    // Stun vars.
    private Animator animator;

    private bool isStunned;

    private float stunCooldown = 0f;

    private GameObject stunInst;

    private GameObject beamOrigin;

    // Unity callbacks.
    void Start()
    {
        interactor = GetComponent<Interactor>();

		animator = transform.GetChild(0).GetComponent<Animator>();

        rumbler = GetComponent<Rumbler>();

        shocker = GetComponent<Shocker>();

        inventory = GetComponent<Inventory>();

        playerView = GetComponent<PlayerView>();

        isStunned = false;

        //beamOrigin = playerView.WizardModel.transform.FindChild("BeamOrigin").gameObject;
    }
	
	void Update ()
    {
		HandleInput();

        if (isStunned == true)
        {
            if (stunCooldown > 0)
            {
                stunCooldown -= Time.deltaTime;

                if (stunCooldown < 0)
                {
                    RemoveStun();
                }
            }
        }
	}

    // Private methods.
    private void HandleInput()
    {
        if (isStunned)
        {
            return;
        }
        
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
            Debug.Log(string.Format("{0} dropping gamepad {1} due to enabling keyboard controls", gameObject.name, GamepadIndex));

            __global_ClaimedGamepadIndices[(int)GamepadIndex] = false;

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

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopInteraction();
        }

        // Interact with space bar.
        if (Input.GetKey(KeyCode.C))
        {
			PerformShockwave();
        }
    }

    private void HandleGamepadInput()
    {
        // If gamepad disconnects, drop the index.
        if (gamepadIndexSet && !prevState.IsConnected)
        {
            Debug.Log(string.Format("{0} dropping gamepad {1} due to disconnection", gameObject.name, GamepadIndex));

            __global_ClaimedGamepadIndices[(int)GamepadIndex] = false;

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

                    GamepadIndex = testPlayerIndex;

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

            state = GamePad.GetState(GamepadIndex);

            // Move with left thumbstick.
            var direction = new Vector3(state.ThumbSticks.Left.X, 0f, state.ThumbSticks.Left.Y);

            PerformMovement(direction);

            // Interact with A button.
            if (state.Buttons.A == ButtonState.Pressed)
            {
                PerformInteraction();
            }

            if (state.Buttons.A == ButtonState.Released
                && prevState.Buttons.A == ButtonState.Pressed)
            {
                StopInteraction();
            }

            if (state.Buttons.B == ButtonState.Pressed)
            {
				PerformShockwave();
            }
        }
    }

    private void PerformMovement(Vector3 direction)
    {
        if (direction.magnitude > 0f)
        {
            direction.Normalize();

            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

            transform.LookAt(transform.position + direction);

            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);

            animator.SetTrigger("walk");
        }
        else
        {
            SetPlayerIdle();
        }
    }

    // Drain/Charge interaction
    private void PerformInteraction()
    {
		bool interactionOccurred = interactor.Interact();

        if (interactionOccurred)
        {
            animator.SetTrigger("magic");
            beamOrigin.SetActive(true);
        }
        
    }

    private void StopInteraction()
    {
        interactor.StopInteract();
        beamOrigin.SetActive(false);
    }

    // ShockWave
    private void PerformShockwave()
    {
        if (shocker.CanShock(inventory))
        {
            shocker.Shock(inventory);

            rumbler.SetFadingRumbleOverTime(1f, 0.25f, 0.25f);
        }
    }
    
	private void SetPlayerIdle()
	{
		animator.SetTrigger("idle");
	}

    public void ActivateStun()
    {
        isStunned = true;
        stunCooldown = stunTime;
        stunInst = (GameObject)Instantiate(stunPrefab, transform.position, Quaternion.identity);
        stunInst.transform.parent = transform;
    }

    public void RemoveStun()
    {
        isStunned = false;
        stunCooldown = 0;
        if (stunInst != null)
        {
            Destroy(stunInst);
        }
    }
}	
