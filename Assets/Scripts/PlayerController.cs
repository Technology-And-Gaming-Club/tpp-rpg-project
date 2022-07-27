using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float w_speed;
    public bool Running;
    public Transform playerTrans;
    public CharacterController charCon;
    private Vector3 moveInput;
 



    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {
        //if (Input.GetKey(KeyCode.W))
        //{
        //    playerRigid.velocity = transform.forward * w_speed * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    playerRigid.velocity = -transform.forward * wb_speed * Time.deltaTime;
        //}

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 horizontalMove = transform.right * Input.GetAxis("Horizontal");
        Vector3 verticalMove = transform.forward * Input.GetAxis("Vertical");

        moveInput = horizontalMove + verticalMove;
        moveInput.Normalize();
        moveInput = moveInput * w_speed;

        charCon.Move(moveInput * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("Run");
            playerAnim.ResetTrigger("Idle");
            Running = true;
            
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("Run");
            playerAnim.SetTrigger("Idle");
            Running = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("Run Back");
            playerAnim.ResetTrigger("Idle");
            Running = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("Run Back");
            playerAnim.SetTrigger("Idle");
            Running = false;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerAnim.SetTrigger("Left Run");
            playerAnim.ResetTrigger("Idle");
            Running = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            playerAnim.ResetTrigger("Left Run");
            playerAnim.SetTrigger("Idle");
            Running = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            playerAnim.SetTrigger("Right Run");
            playerAnim.ResetTrigger("Idle");
            Running = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            playerAnim.ResetTrigger("Right Run");
            playerAnim.SetTrigger("Idle");
            Running = false;
        }
    }
}
