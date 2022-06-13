using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {
	[Header("Movement")]
	public bool isGrounded;
	public float sprintSpeed;
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

	[Header("Analogue Inputs")]
	[Range(-1f, 1f)]
	public float moveHorInput;
	[Range(-1f, 1f)]
	public float moveVertInput;
	[Range(-1f, 1f)]
	public float mouseHorInput;
	[Range(-1f, 1f)]
	public float mouseVertInput;

	[Header("Digital Inputs")]
	public bool sprint;
	public bool walk;
	public bool jump;
	public bool crouch;
	public bool action;

	[Header("Misc")]
	public Rigidbody rb;
	public CharacterController cc;
	public Transform cam;
	public Transform head;


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
		setMoveSpeed();
		mouseMovement();
		playerMovement();
		miscControlFunctions();
		cc.Move(targetVelocity);
	}


	void getInputVals() {
		moveHorInput = Input.GetAxis("Horizontal");
		moveVertInput = Input.GetAxis("Vertical");
		mouseHorInput = Input.GetAxis("Mouse X");
		mouseVertInput = Input.GetAxis("Mouse Y");

		sprint = Input.GetButton("Sprint");
		walk = Input.GetButton("Walk");
		jump = Input.GetButton("Jump");
		crouch = Input.GetButton("Crouch");
		action = Input.GetButton("Action");
	}

	void setMoveSpeed() {
		if(sprint) {
			moveSpeed = sprintSpeed;
		} else if(walk) {
			moveSpeed = walkSpeed;
		} else {
			moveSpeed = runSpeed;
		}
	}
	void playerMovement() {
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

		if(action) {
			RaycastHit hit;
			Ray ray = new Ray(cam.position + 0.2f * cam.forward, cam.forward);
			Physics.Raycast(ray, out hit, 1f);

			if(hit.collider != null) {
				Debug.Log(hit.transform.gameObject);
				hit.transform.gameObject.SendMessage("playerInteraction", null, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	void mouseMovement() {
		mouseX += mouseHorInput * sens;
		mouseY += mouseVertInput * sens;

		clampMouseValues();
		Vector3 camRotation;
		Vector3 playerRotation;
		convertMopuseToRotation(out camRotation, out playerRotation);
		cam.localRotation = Quaternion.Euler(camRotation);
		transform.rotation = Quaternion.Euler(playerRotation);
	}
	void convertMopuseToRotation(out Vector3 camRotation, out Vector3 playerRotation) {
		camRotation = new Vector3();
		playerRotation = new Vector3();
		playerRotation.y = mouseX;
		if(mouseY <= 0) {
			camRotation.x = -mouseY;
		} else {
			camRotation.x = 360 - mouseY;
		}
	}
	void clampMouseValues() {
		if(mouseX > 360) {
			mouseX -= 360;
		} else if(mouseX < 0) {
			mouseX += 360;
		}

		if(mouseY > 90) {
			mouseY = 90;
		} else if(mouseY < -40) {
			mouseY = -40;
		}
	}
}
