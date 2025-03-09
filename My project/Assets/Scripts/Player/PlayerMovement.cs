using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private bool facingRight = true;
    public Transform spriteTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()  // Use FixedUpdate for physics-based movement
    {
        float moveInput = Input.GetAxisRaw("Horizontal");  // Instant response
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Flip Player if Changing Direction
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteTransform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);  // Ensures smooth movement
        }
    }
}
