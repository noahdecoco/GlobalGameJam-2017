using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    
	public float moveSpeed = 10f;
	private bool _isGrounded = true;
	private Rigidbody _rigidBody;

	void Start() {

		_rigidBody = GetComponent<Rigidbody>();

	}

	void OnTriggerEnter(Collider other) {
		
		if(other.tag == "MovingPlatform"){
			_isGrounded = true;
		}

	}

	void OnTriggerExit(Collider other) {
		
		if(other.tag == "MovingPlatform"){
			_isGrounded = false;
		}

	}
	
	// Update is called once per frame
	void Update () {

		HandleInput();
	}

	void HandleInput()
	{
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);

        // Detect if a button was pressed this frame
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        }
        // Detect if a button was released this frame
        if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
        {
            GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        // Make the current object turn
        transform.localRotation *= Quaternion.Euler(0.0f, state.ThumbSticks.Left.X * 25.0f * Time.deltaTime, 0.0f);
    }
}	
