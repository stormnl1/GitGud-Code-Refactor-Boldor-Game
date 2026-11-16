using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform cameraPivot;

    [SerializeField] private float mouseSensitivity = 0.1f;

    private Vector2 lookInput;

    private float cameraPitch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
    }

    public void CameraInput(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();

        if (context.canceled)
        {
            lookInput = Vector2.zero;
        }
    }
    private void CameraRotation()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        // Rotate PLAYER left/right
        transform.Rotate(Vector3.up * mouseX);

        // Rotate CAMERA up/down
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        cameraPivot.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }
}
