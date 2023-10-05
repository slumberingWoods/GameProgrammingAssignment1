using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public Vector3 gravity;
    public Vector3 playerVelocity;
    public bool groundedPlayer;
    public float mouseSensitivy = 5.0f;
    private float jumpHeight = 1f;
    private float gravityValue = -9.81f;
    private CharacterController controller;
    private float walkSpeed = 5;
    private float runSpeed = 8;
    private Animator animator;
    public GameObject followTarget;
 
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Update()
    {
       UpdateRotation();
       ProcessMovement();
    }
    void UpdateRotation()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X")* mouseSensitivy, 0, Space.Self);
        followTarget.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * mouseSensitivy, Vector3.right);
        var angles = followTarget.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTarget.transform.localEulerAngles.x;

        if (angle > 160 && angle < 300) {
            angles.x = 300;
        }
        else if(angle < 160 && angle > 40) {
            angles.x = 40;
        }

        followTarget.transform.localEulerAngles = angles;

        transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);

        followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

    void ProcessMovement()
    { 
        // Moving the character forward according to the speed
        float speed = GetMovementSpeed();
        
        // Get the camera's forward and right vectors
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
 
        // Make sure to flatten the vectors so that they don't contain any vertical component
        cameraForward.y = 0;
        cameraRight.y = 0;
 
        // Normalize the vectors to ensure consistent speed in all directions
        cameraForward.Normalize();
        cameraRight.Normalize();
 
        // Calculate the movement direction based on input and camera orientation
        Vector3 moveDirection = (cameraForward * Input.GetAxis("Vertical")) + (cameraRight * Input.GetAxis("Horizontal"));
 
        // Apply the movement direction and speed
        Vector3 movement = moveDirection.normalized * speed * Time.deltaTime;
 
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isGrounded", true);
            animator.SetBool("isDoubleJump", false);
            if (movement != Vector3.zero) {
                if (GetMovementSpeed() == runSpeed) {
                    animator.SetFloat("Speed", 1.0f);
                } else {
                    animator.SetFloat("Speed", 0.49f);
                }
            } else {
                animator.SetFloat("Speed", 0.0f);
            }
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetBool("noJump", true);
                animator.SetBool("isGrounded", false);
                gravity.y += Mathf.Sqrt(jumpHeight * -5.0f * gravityValue);
                animator.SetBool("isJumping", true);
            }
            else
            {
                // Dont apply gravity if grounded and not jumping
                gravity.y = -1.0f;
            }
        }
        else
        {
            animator.SetBool("isGrounded", false);
            if(GameManager.Instance.doubleJump == true && Input.GetButtonDown("Jump"))
            {
                animator.SetBool("isDoubleJump", true);
                gravity.y += Mathf.Sqrt((jumpHeight + 2.0f) * -5.0f * gravityValue );
                GameManager.Instance.ResetJump();
            }
            // Since there is no physics applied on character controller we have this applies to reapply gravity
            gravity.y += gravityValue * Time.deltaTime;
        }
        // Apply gravity and move the character
        playerVelocity = gravity * Time.deltaTime + movement;
        controller.Move(playerVelocity);
    }

    float GetMovementSpeed()
    {
        if (Input.GetButton("Shift"))// Left shift
        {
            animator.SetFloat("Speed", 1.0f);
            return runSpeed;
        }
        else
        {
            animator.SetFloat("Speed", 0.4f);
            return walkSpeed;
        }
    }
}
