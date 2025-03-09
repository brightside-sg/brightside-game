using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;
    public Transform playerSprite;  // Reference to the sprite (child object)

    void Start()
    {
        // Get Rigidbody from THIS object (Player)
        rb = GetComponent<Rigidbody2D>();

        // Get Animator from the child object (PlayerSprite)
        animator = GetComponentInChildren<Animator>();

        // Find the PlayerSprite (Child)
        playerSprite = transform.Find("PlayerSprite"); 

        // Check if Animator and Sprite are found (Prevent errors)
        if (animator == null)
        {
            Debug.LogError("Animator not found! Make sure the Animator is on PlayerSprite.");
        }

        if (playerSprite == null)
        {
            Debug.LogError("PlayerSprite not found! Make sure it's named correctly in the Hierarchy.");
        }
    }

void Update()
{
    float moveInput = Input.GetAxisRaw("Horizontal");
    
    // Keep the same velocity while flipping to prevent briefly entering Idle
    if (moveInput != 0)
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    // Set Speed parameter in Animator (Only if Animator exists)
    if (animator != null)
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));  // Use velocity instead of input to avoid flicker
    }

    // Flip sprite based on movement direction (Only flip if actually moving)
    if (moveInput > 0 && !facingRight) Flip();
    else if (moveInput < 0 && facingRight) Flip();
}

    void Flip()
    {
        facingRight = !facingRight;

        // Flip only the PlayerSprite, NOT the whole Player object
        if (playerSprite != null)
        {
            Vector3 newScale = playerSprite.localScale;
            newScale.x = facingRight ? 1 : -1;
            playerSprite.localScale = newScale;
        }
    }
}
