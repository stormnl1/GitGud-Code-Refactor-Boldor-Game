using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private float horizontalCameraSpeed = 200;
    private float verticalCameraSpeed = 100;
    public GameObject player;

    private float yaw;
    private float pitch;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }
    
    // Update is called once per frame
    void Update()
    {
        CameraControl();
    }

    private void CameraControl()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            yaw -= horizontalCameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            yaw += horizontalCameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            pitch -= verticalCameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            pitch += verticalCameraSpeed * Time.deltaTime;
        }

        // Prevent camera flipping too far up/down
        pitch = Mathf.Clamp(pitch, -80f, 80f);

        // Rebuild rotation with zero roll
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);

        transform.position = player.transform.position; // Move focal point with player

    }
}
