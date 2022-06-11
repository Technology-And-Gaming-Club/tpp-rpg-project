using System.Collections;
using UnityEngine;

public class AltChatacterMovementScript : MonoBehaviour {
	[Header("Movement")]
	public float sprintSpeed;
	public float walkSpeed;
	public float runSpeed;
	[Range(0f, 0.5f)]
	public float moveSpeed;
	public Vector3 targetVelocity;
	[Range(0, 3)]
	public float jumpHeight;

	[Header("Mouse")]
	[Range(0, 360)]
	public float mouseX;
	[Range(-40, 90)]
	public float mouseY;
	[Range(0, 5)]
	public float sens;

	[Header("Digital Inputs")]
	public bool walk;
	public bool sprint;
	public bool jump;

	[Header("Analogue Inputs")]
	[Range(-1, 1)]
	public float moveHorInput;
	[Range(-1, 1)]
	public float moveVertInput;
	[Range(-1, 1)]
	public float mouseHorInput;
	[Range(-1, 1)]
	public float mouseVertInput;


	[Header("Misc")]
	public Rigidbody rb;
	public CharacterController cc;
	public Transform cam;
	public Transform body;

	void Start() {
		rb = GetComponent<Rigidbody>();
		cc = GetComponent<CharacterController>();
		targetVelocity = new Vector3();
		mouseX = mouseY = 0;
	}

	void Update() {
		getInputs();
		setMoveSpeed();
		playerTranslation();
		otherPlayerMovement();
		mouseMovement();

		/*cc.Move(targetVelocity * 0.01f);*/
		rb.velocity = targetVelocity;
	}

	void getInputs() {
		walk = Input.GetButton("Walk");
		sprint = Input.GetButton("Sprint");
		jump = Input.GetButton("Jump");

		moveHorInput = Input.GetAxis("Horizontal");
		moveVertInput = Input.GetAxis("Vertical");
		mouseHorInput = Input.GetAxis("Mouse X");
		mouseVertInput = Input.GetAxis("Mouse Y");
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
	void playerTranslation() {
		Vector3 dir = moveHorInput * transform.right + moveVertInput * transform.forward;
		targetVelocity.x = (dir * moveSpeed).x;
		targetVelocity.z = (dir * moveSpeed).z;


		if(cc.isGrounded) {
			targetVelocity.y = 0;
		} else {
			targetVelocity.x /= 1.5f;
			targetVelocity.z /= 1.5f;
			targetVelocity.y -= 9.8f * Time.deltaTime;
		}
	}
	void otherPlayerMovement() {
		// Jump functionality
		if(cc.isGrounded && jump) {
			targetVelocity.y += Mathf.Sqrt(-2 * Physics.gravity.y * jumpHeight);
		}

		// Crouch functionality

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
			camRotation.x = 360 - mouseX;
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