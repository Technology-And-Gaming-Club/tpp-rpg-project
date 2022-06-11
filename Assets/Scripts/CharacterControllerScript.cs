using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {
	[Header("Movement")]
	public bool isGrounded;
	public float runSpeed;
	public float walkSpeed;
	[Range(0f, 0.5f)]
	public float moveSpeed;
	public Vector3 targetVelocity;
	public float jumpHeight;

	[Header("Mouse")]
	public float sens;
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
	public bool walk;
	public bool jump;
	public bool crouch;

	[Header("Misc")]
	public Rigidbody rb;
	public CharacterController cc;
	public Transform camera;
	public Transform body;
	public Transform head;
	public float groundDistance;


	void Start() {
		rb = GetComponent<Rigidbody>();
		cc = GetComponent<CharacterController>();
		targetVelocity = new Vector3();
		mouseX = 0;
		mouseY = 0;
	}

	void Update() {
		isGrounded = cc.isGrounded;
		getInputVals();
		mouseMovement();
		playerMovement();
		miscControlFunctions();
		cc.Move(targetVelocity);
	}


	void getInputVals() {
		moveHorInput = Input.GetAxis("Horizontal");
		moveVertInput = Input.GetAxis("Vertical");
		mouseXInput = Input.GetAxis("Mouse X");
		mouseYInput = Input.GetAxis("Mouse Y");
		walk = Input.GetButton("Walk");
		jump = Input.GetButton("Jump");
		crouch = Input.GetButton("Crouch");

		mouseX = transform.rotation.eulerAngles.y;
		mouseY = -camera.rotation.eulerAngles.x;
	}
	
	void mouseMovement() {
		float mouseXDel = mouseXSens * mouseXInput * Time.deltaTime;
		float mouseYDel = mouseYSens * mouseYInput * Time.deltaTime;

		mouseX += mouseXDel;
		mouseY += mouseYDel;

		if(mouseX < 0) {
			mouseX += 360;
		} else if(mouseX > 360f) {
			mouseX -= 360;
		}
		if(mouseY < -35 && mouseY > -265) {
			mouseY = -35;
		} else if(mouseY > -270 && mouseY < -265) {
			mouseY = -270;
		}

		Vector3 finalPlayerRotation = new Vector3(0, mouseX, 0);
		Vector3 finalCamRotation = new Vector3(-mouseY, 0, 0);

		transform.rotation = Quaternion.Euler(finalPlayerRotation);
		camera.localRotation = Quaternion.Euler(finalCamRotation);
	}
	void playerMovement() {
		if(walk) {
			moveSpeed = walkSpeed;
		} else {
			moveSpeed = runSpeed;
		}
		Vector3 moveDir;
		moveDir = moveHorInput * transform.right + moveVertInput * transform.forward;
		targetVelocity = moveSpeed * moveDir;

		if(!cc.isGrounded) {
			targetVelocity.x /= 0.75f;
			targetVelocity.z /= 0.75f;
			targetVelocity.y += Physics.gravity.y * Time.deltaTime;
		} else {
			targetVelocity.y = 0;
		}
	}

	void miscControlFunctions() {
		if(jump && cc.isGrounded) {
			targetVelocity.y += Mathf.Sqrt(2 * Physics.gravity.magnitude * jumpHeight) * cc.stepOffset;
		}

		if(crouch) {
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 0.75f, 1f), 0.5f);
			head.localScale = new Vector3(0.4f, 0.4f / transform.localScale.y, 0.4f);
		} else {
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), 0.5f);
			head.localScale = new Vector3(0.4f, 0.4f / transform.localScale.y, 0.4f);
		}
	}
}
