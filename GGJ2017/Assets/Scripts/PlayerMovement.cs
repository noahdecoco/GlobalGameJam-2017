using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 10f;

	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        
		if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
			transform.Translate(Vector3.right * -moveSpeed * Time.deltaTime);
        
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
			transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

	}
}