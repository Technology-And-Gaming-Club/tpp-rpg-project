using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {
	[Header("Movement")]
	public float moveSpeed;

	[Header("Mouse")]
	public float mouseXSens;
	public float mouseYSens;
	public float mouseYMin;
	public float mouseYMax;

	[Header("Inputs")]
	[Range(-1f, 1f)]
	public float moveHorInput;
	[Range(-1f, 1f)]
	public float moveVertInput;
	[Range(-1f, 1f)]
	public float mouseXInput;
	[Range(-1f, 1f)]
	public float mouseYInput;
	public bool jump;
	public bool crouch;

	[Header("Misc")]
	public Rigidbody rb;
	public Transform camera;


	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void Update() {
		getInputVals();
		mouseMovement();
	}

	void getInputVals() {
		moveHorInput = Input.GetAxis("Horizontal");
		moveVertInput = Input.GetAxis("Vertical");
		mouseXInput = Input.GetAxis("Mouse X");
		mouseYInput = Input.GetAxis("Mouse Y");
		jump = Input.GetButtonDown("Jump");
		crouch = Input.GetButton("Crouch");
	}

	void mouseMovement() {
		float mouseXDel = mouseXSens * mouseXInput * Time.deltaTime;
		float mouseYDel = mouseYSens * mouseYInput * Time.deltaTime;


		Vector3 delta = new Vector3(-mouseYDel, mouseXDel, 0);
		Vector3 FinalRotation = camera.rotation.eulerAngles + delta;

		FinalRotation = clampRotation(FinalRotation, -mouseYMax, -mouseYMin, -180f, 180f);

		camera.rotation = Quaternion.Euler(FinalRotation);
	}

	Vector3 clampRotation(Vector3 rotation, float xMin, float xMax, float yMin, float yMax) {
		float x, y;
		if(rotation.x < xMin) {
			x = xMin;
		} else if(rotation.x > xMax) {
			x = xMax;
		} else {
			x = rotation.x;
		}

		if(rotation.y <= yMin) {
			y = rotation.y + 360;
		} else if(rotation.y >= yMax) {
			y = rotation.y - 360;
		} else {
			y = rotation.y;
		}

		return new Vector3(x, y, 0);
	}
}
