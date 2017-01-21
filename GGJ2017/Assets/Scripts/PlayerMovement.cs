using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

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

		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
			transform.Translate(Vector3.forward.normalized * moveSpeed * Time.deltaTime, Space.World);
        
		if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
			transform.Translate(-Vector3.forward.normalized * moveSpeed * Time.deltaTime, Space.World);
        
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
			transform.Translate(Vector3.right.normalized * -moveSpeed * Time.deltaTime, Space.World);
        
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
			transform.Translate(Vector3.right.normalized * moveSpeed * Time.deltaTime, Space.World);

		if(Input.GetKey(KeyCode.Space) && _isGrounded) {
			print("Jump");
			// transform.Translate(Vector3.up * 12 * Time.deltaTime, Space.World);
			// _rigidBody.AddForce(Vector3.up * 5000);
		}

	}
}	