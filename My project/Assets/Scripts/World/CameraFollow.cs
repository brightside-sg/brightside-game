using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public float smoothSpeed = 0.125f;  // Smooth camera movement
    public float fixedYPosition = 0f;  // Lock Y position (Adjust in Inspector)
    public float offsetX = 1f;  // Horizontal offset (Optional)

    void LateUpdate()
    {
        if (player == null) return;

        // Only update the X-axis, keep Y fixed
        Vector3 desiredPosition = new Vector3(player.position.x + offsetX, fixedYPosition, -10);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
