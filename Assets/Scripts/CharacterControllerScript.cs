using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {
	[Header("Movement")]
	public float moveSpeed;

	[Header("Mouse")]
	[Range(0f, 360f)]
	public float mouseX;
	[Range(-360f, 0f)]
	public float mouseY;
	public float mouseXSens;
	public float mouseYSens;

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
		mouseX = 0;
		mouseY = 0;
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

		mouseX = camera.rotation.eulerAngles.y;
		mouseY = -camera.rotation.eulerAngles.x;
	}

	void mouseMovement() {
		float mouseXDel = mouseXSens * mouseXInput * Time.deltaTime;
		float mouseYDel = mouseYSens * mouseYInput * Time.deltaTime;

		mouseX += mouseXDel;
		mouseY += mouseYDel;

		if(mouseX < 0) {
			mouseX += 360;
		} else if(mouseY > 360f) {
			mouseX -= 360;
		}
		if(mouseY < -35 && mouseY > -265) {
			mouseY = -35;
		}

		Vector3 finalRotation = new Vector3(-mouseY, mouseX, 0);

		camera.rotation = Quaternion.Euler(finalRotation);
	}
}
