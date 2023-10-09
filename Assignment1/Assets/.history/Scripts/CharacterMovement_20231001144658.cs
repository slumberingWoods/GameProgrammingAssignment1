using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public Vector3 gravity;
    public Vector3 playerVelocity;
    public bool groundedPlayer;
    public float mouseSensitivy = 5.0f;
    private float jumpHeight = 1.2f;
    private float gravityValue = -9.81f;
    private CharacterController controller;
    private float walkSpeed = 5;
    private float runSpeed = 8;
    private bool doubleJump = false;
    private Animator animator;
    public GameObject gameManager;
    public GameObject followTarget;
 
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
       UpdateRotation();
       ProcessMovement();
    }
    void UpdateRotation()
    {
                #region Player Based Rotation
        
        //Move the player based on the X input on the controller
        //transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        #endregion

        #region Follow Transform Rotation

        //Rotate the Follow Target transform based on the input
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        #endregion

        #region Vertical Rotation
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if(angle < 180 && angle > 40)
        {
            angles.x = 40;
        }


        followTransform.transform.localEulerAngles = angles;
        #endregion

        
        nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);

        if (_move.x == 0 && _move.y == 0) 
        {   
            nextPosition = transform.position;

            if (aimValue == 1)
            {
                //Set the player rotation based on the look transform
                transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
                //reset the y rotation of the look transform
                followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
            }

            return; 
        }
        float moveSpeed = speed / 100f;
        Vector3 position = (transform.forward * _move.y * moveSpeed) + (transform.right * _move.x * moveSpeed);
        nextPosition = transform.position + position;        
        

        //Set the player rotation based on the look transform
        transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
        //reset the y rotation of the look transform
        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
 
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
            doubleJump = false;
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
            if(GameManager.doubleJump == true && Input.GetButtonDown("Jump"))
            {
                animator.SetBool("isDoubleJump", true);
                gravity.y += Mathf.Sqrt(jumpHeight * -5.0f * gravityValue);
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
            animator.SetFloat("Speed", 0.5f);
            return walkSpeed;
        }
    }
}
