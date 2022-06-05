using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {
	[Header("Movement")]
	public bool isGrounded;
	public float gravity;
	public float moveSpeed;
	public float jumpHeight;

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
	public bool walk;
	public bool jump;
	public bool crouch;

	[Header("Misc")]
	public Rigidbody rb;
	public Transform camera;
	public Transform body;
	public Transform head;
	public float groundDistance;


	void Start() {
		rb = GetComponent<Rigidbody>();
		mouseX = 0;
		mouseY = 0;
	}

	void Update() {
		groundCheck();
		getInputVals();
		mouseMovement();
		playerMovement();
		miscControlFunctions();
	}


	void groundCheck() {
		RaycastHit hit;
		Physics.Raycast(transform.position, -transform.up, out hit);
		if(Mathf.Approximately(hit.distance, 0.75f) || Mathf.Approximately(hit.distance, 0.5625f)) {
			isGrounded = true;
		} else {
			isGrounded = false;
		}
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
			moveSpeed = 5;
		} else {
			moveSpeed = 10;
		}
		float angle = camera.rotation.eulerAngles.y * Mathf.PI / 180f;
		Vector3 moveDir = (moveHorInput * transform.right + moveVertInput * transform.forward).normalized;
		Vector3 finalVelocity = moveSpeed * moveDir * new Vector3(moveHorInput, rb.velocity.y, moveVertInput).magnitude;

		if(isGrounded) {
			rb.velocity = Vector3.Lerp(finalVelocity, rb.velocity, 0.5f);
		} else {
			rb.velocity -= Vector3.up * gravity * Time.deltaTime;
		}
	}

	void miscControlFunctions() {
		if(jump && isGrounded) {
			StartCoroutine(jumpCoroutine());
		}

		if(crouch) {
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 0.75f, 1f), 0.5f);
			head.localScale = new Vector3(0.4f, 0.4f / transform.localScale.y, 0.4f);
		} else {
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), 0.5f);
			head.localScale = new Vector3(0.4f, 0.4f / transform.localScale.y, 0.4f);
		}
	}


	IEnumerator jumpCoroutine() {
		float jumpVel = Mathf.Sqrt(2 * gravity * jumpHeight);
		rb.velocity += Vector3.up * jumpVel;
		yield return new WaitForSeconds(0.5f);
	}
}
