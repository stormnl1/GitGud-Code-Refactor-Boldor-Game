
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool IsInvincible; // When the player is invincible it cannot be destroyed and the player is able to destroy "destructible"

    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private GameObject overlayMenu;
    [SerializeField] private GameObject fireParticleSystem;

    [Space]
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float groundPoundForce = 15f;
    [SerializeField] private float dashForce = 1000;
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float maxDashSpeed = 100f;
    [SerializeField] private float dashCooldown = 0.5f;
    [SerializeField] private float destroySpeed = 20f;
    [SerializeField] private float groundPoundCooldown = 0.5f;

    private bool isGrounded = true;
    private float lastDashTime;
    private float lastGroundPoundTime;
    private float playerSpeed; 

    // Movement buttons
    private bool forwardPressed;
    private bool backwardPressed;
    private bool leftPressed;
    private bool rightPressed;

    // Camera variables
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
        InvincibilityIndicator();
        SpeedCheck();
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
        lookInput = context.ReadValue<Vector2>();
    }

    public void DashBoost(InputAction.CallbackContext context) // Shift
    {
        if (context.performed &&
            Time.time >= lastDashTime + dashCooldown &&
            playerSpeed < maxDashSpeed)
        {
            // Use different dash forces depending on whether grounded
            if (isGrounded)
            {
                rb.AddForce(cameraPivot.forward * dashForce, ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(cameraPivot.forward * dashForce, ForceMode.Impulse);
            }

            lastDashTime = Time.time;
        }
    }

    public void GroundPound(InputAction.CallbackContext context) // Ctrl
    {
        if (context.performed &&
            Time.time >= lastGroundPoundTime + groundPoundCooldown)
        {
            rb.AddForce(Vector3.down * groundPoundForce, ForceMode.Impulse);
            lastGroundPoundTime = Time.time;
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

    // Restore overlay menu functionality using New Input System
    public void OpenOverlayMenu(InputAction.CallbackContext context) // Escape
    {
        if (context.performed && overlayMenu != null)
        {
            overlayMenu.SetActive(!overlayMenu.activeSelf);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;

        if (collision.gameObject.CompareTag("Destructible") && IsInvincible)
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TargetPointTrigger"))
        {
            isGrounded = true;
            Destroy(other.gameObject);
        }
    }

    private void MovePlayer()
    {
        // Get camera directions
        Vector3 forward = cameraPivot.forward;
        Vector3 right = cameraPivot.right;

        // Remove vertical components
        forward.y = 0f;
        right.y = 0f;

        Vector3 movement = Vector3.zero;

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

        rb.AddForce(movement * moveSpeed, ForceMode.Acceleration);
    }

    private void CameraRotation()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        cameraYaw += mouseX;
        cameraPitch -= mouseY;

        cameraPitch = Mathf.Clamp(cameraPitch, cameraMinPitch, cameraMaxPitch);

        cameraPivot.rotation = Quaternion.Euler(cameraPitch, cameraYaw, 0f);
    }

    private void PlayerSpeed()
    {
        playerSpeed = rb.linearVelocity.magnitude;
    }

    private void SpeedCheck()
    {
        if (rb.linearVelocity.magnitude >= destroySpeed)
        {
        IsInvincible = true;
        }   

        else
        {
        IsInvincible = false;
        }
    }

    // Restore fire particle effect when player is invincible
    private void InvincibilityIndicator()
    {
        if (IsInvincible == true)
        {
            fireParticleSystem.SetActive(true);
        }

        else
        {
            fireParticleSystem.SetActive(false);
        }
    }
}

