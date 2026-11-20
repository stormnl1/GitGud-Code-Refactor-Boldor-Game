
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool IsInvincible // property to keep track of wether the player is going fast enough to destroy objects with tag "Destructible"
    {
        get
        {
            return rb.linearVelocity.magnitude >= maxDashSpeed;   
        }
    }

    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform cameraPivot;

    [Space]
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float groundPoundForce = 15f;
    [SerializeField] private float dashForce;
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float maxDashSpeed; // a greater speed than this disables DashBoost
    [SerializeField] private float dashCooldown = 0.5f;
    
    private bool isGrounded = true;
    private float lastDashTime; // keeps track of how much time has passed after the player used DashBoost
    [SerializeField] private float playerSpeed; // keeps track of player's velocity

    // Movement buttons
    private bool forwardPressed;
    private bool backwardPressed;
    private bool leftPressed;
    private bool rightPressed;

    // camera variables
    private Vector2 lookInput;
    private float cameraPitch;
    private float cameraYaw;
    private float cameraMaxPitch = 80.0f;
    private float cameraMinPitch = -80.0f;
    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CameraRotation();
        PlayerSpeed();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Separate movement inputs
    public void MoveForward(InputAction.CallbackContext context) // W
    {
        forwardPressed = context.ReadValueAsButton();
    }

    public void MoveBackward(InputAction.CallbackContext context) // S
    {
        backwardPressed = context.ReadValueAsButton();
    }

    public void MoveLeft(InputAction.CallbackContext context) // A
    {
        leftPressed = context.ReadValueAsButton();
    }

    public void MoveRight(InputAction.CallbackContext context) // D
    {
        rightPressed = context.ReadValueAsButton();
    }

    public void CameraInput(InputAction.CallbackContext context) // Mouse
    {
        lookInput = context.ReadValue<Vector2>(); // store how much the mouse went horizontally and vertically in lookInput
    }

    public void DashBoost(InputAction.CallbackContext context) // Shift
    {
        if (context.performed && Time.time >= lastDashTime + dashCooldown && playerSpeed < maxDashSpeed)
        {
            rb.AddForce(cameraPivot.forward * dashForce, ForceMode.Impulse);
            lastDashTime = Time.time;
        }
    }

    public void GroundPound(InputAction.CallbackContext context) // Ctrl
    {
        if (context.performed)
        {
            rb.AddForce(Vector3.down * groundPoundForce, ForceMode.Impulse);
        }
    }

    public void PlayerJump(InputAction.CallbackContext context) // Space
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        isGrounded = true; // Allow player to jump after a collision

        if (collision.gameObject.CompareTag("Destructible") && IsInvincible == true) // Destroys other object wiht tag "Destructible" when player goes fast enough
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) // Destroy target when player collides with it
    {
        if (other.gameObject.CompareTag("TargetPointTrigger"))
        {   
            isGrounded = true;
            Destroy(other.gameObject);
        }   
    }

    private void MovePlayer()
    {
        // get camera directions
        Vector3 forward = cameraPivot.forward;
        Vector3 right = cameraPivot.right;

        // keep vertical velocity 0 so the player doesn't gain upwards speed when looking up
        forward.y = 0f;
        right.y = 0f;

        Vector3 movement = Vector3.zero; // variable to keep track of movement direction

        if (forwardPressed)
        {
            movement += forward;
        }
        if (backwardPressed)
        {
            movement -= forward;
        }
        if (leftPressed)
        {
            movement -= right;
        }
        if (rightPressed)
        {
            movement += right;
        }

        rb.AddForce(movement * moveSpeed, ForceMode.Acceleration); // Accelerates the player in the movement direction by moveSpeed
    }

    private void CameraRotation()
    {
        // multiply lookInput by mouseSensitivity to increase/decrease the magnitude at which the camera's Yaw and Pitch are afffected
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;
        
        cameraYaw += mouseX;
        cameraPitch -= mouseY;

        cameraPitch = Mathf.Clamp(cameraPitch, cameraMinPitch, cameraMaxPitch); // limtis how far the camera can rotate up and down

        cameraPivot.rotation = Quaternion.Euler(cameraPitch, cameraYaw, 0f); // change camera's rotation to cameraPitch and -Yaw values
    }

    private void PlayerSpeed()
    {
        playerSpeed = rb.linearVelocity.magnitude;
    }
}
