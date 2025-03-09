using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public float imageWidth = 1.0f; // Adjust based on your image width
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newPos = Mathf.Repeat(Time.time * scrollSpeed, imageWidth);
        transform.position = startPos + Vector3.left * newPos;
    }
}
