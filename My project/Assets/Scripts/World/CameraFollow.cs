using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public float smoothSpeed = 0.125f;  // Smooth camera movement
    public float fixedYPosition = 0f;  // Lock Y position
    public float screenPercentageLeft = 0.25f;  // Player's position (25% from left)

    private float cameraHalfWidth;  // To calculate correct offset

    void LateUpdate()
    {
        if (player == null) return;

        // Ensure Camera.main is always valid
        Camera cam = Camera.main;
        if (cam == null) return;

        // Calculate half-width every frame (Fixes potential issues with resizing)
        cameraHalfWidth = cam.orthographicSize * cam.aspect;

        // Correct offset calculation for 25% left alignment
        float targetX = player.position.x - (cameraHalfWidth * (1 - 2 * screenPercentageLeft));

        // Keep Y fixed and move only on X
        Vector3 desiredPosition = new Vector3(targetX, fixedYPosition, -10);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
