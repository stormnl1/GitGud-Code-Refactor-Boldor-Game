
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform cameraPivot;

    [Space]
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float mouseSensitivity = 0.1f;

    private bool isGrounded = true;

    // Movement buttons
    private bool forwardPressed;
    private bool backwardPressed;
    private bool leftPressed;
    private bool rightPressed;

    private Vector2 lookInput;
    private float cameraPitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CameraRotation();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Separate movement inputs
    public void MoveForward(InputAction.CallbackContext context)
    {
        forwardPressed = context.ReadValueAsButton();
    }

    public void MoveBackward(InputAction.CallbackContext context)
    {
        backwardPressed = context.ReadValueAsButton();
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        leftPressed = context.ReadValueAsButton();
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        rightPressed = context.ReadValueAsButton();
    }

    public void CameraInput(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();

        if (context.canceled)
        {
            lookInput = Vector2.zero;
        }
    }

    public void PlayerJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void MovePlayer()
    {
        Vector3 movement = Vector3.zero;

        if (forwardPressed)
            movement += transform.forward;

        if (backwardPressed)
            movement -= transform.forward;

        if (leftPressed)
            movement -= transform.right;

        if (rightPressed)
            movement += transform.right;

        // Prevent faster diagonal movement
        movement = movement.normalized;

        // Apply movement force
        rb.AddForce(movement * moveSpeed, ForceMode.Acceleration);

        // Limit horizontal movement speed
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (horizontalVelocity.magnitude > maxSpeed)
        {
            Vector3 limitedVelocity = horizontalVelocity.normalized * maxSpeed;

            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }

    private void CameraRotation()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        // Rotate player left/right
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera up/down
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        cameraPivot.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }
}
