using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public float smoothSpeed = 0.125f;  // Smooth camera movement
    public float fixedYPosition = 0f;  // Lock Y position
    public float screenPercentageLeft = 0.25f;  // Player's position on the screen (25% from the left)

    private float cameraHalfWidth;  // To calculate correct offset

    void Start()
    {
        // Calculate how far the camera should be offset based on screen percentage
        Camera cam = Camera.main;
        if (cam != null)
        {
            cameraHalfWidth = cam.orthographicSize * cam.aspect;
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Calculate the target X position (Player is 25% from the left)
        float targetX = player.position.x - (cameraHalfWidth * (0.5f - screenPercentageLeft));

        // Keep Y and Z fixed while moving only on X
        Vector3 desiredPosition = new Vector3(targetX, fixedYPosition, -10);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
