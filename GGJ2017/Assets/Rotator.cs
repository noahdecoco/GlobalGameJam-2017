using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public float rotateSpeed = 20.0f;

	void Update () {
		transform.Rotate(Vector3.up.normalized, rotateSpeed * Time.deltaTime);
	}
}
