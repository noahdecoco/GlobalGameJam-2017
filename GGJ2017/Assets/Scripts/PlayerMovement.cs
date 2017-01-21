using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

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

		//if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
		//	transform.Translate(Vector3.forward.normalized * moveSpeed * Time.deltaTime, Space.World);
        
		//if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
		//	transform.Translate(-Vector3.forward.normalized * moveSpeed * Time.deltaTime, Space.World);
        
		//if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		//	transform.Translate(Vector3.right.normalized * -moveSpeed * Time.deltaTime, Space.World);
        
		//if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		//	transform.Translate(Vector3.right.normalized * moveSpeed * Time.deltaTime, Space.World);

		//if(Input.GetKey(KeyCode.Space) && _isGrounded) {
		//	print("Jump");
		//	// transform.Translate(Vector3.up * 12 * Time.deltaTime, Space.World);
		//	// _rigidBody.AddForce(Vector3.up * 5000);
		//}
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

        var direction = new Vector3(state.ThumbSticks.Left.X, 0f, state.ThumbSticks.Left.Y);

        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        // Make the current object turn
        //transform.localRotation *= Quaternion.Euler(0.0f, state.ThumbSticks.Left.X * 25.0f * Time.deltaTime, 0.0f);
    }
}	
