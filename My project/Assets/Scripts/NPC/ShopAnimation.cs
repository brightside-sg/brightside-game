using UnityEngine;

public class ShopAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Get Animator from the child object (ShopSprite)
        animator = GetComponentInChildren<Animator>();

        // Check if Animator is found (Prevent errors)
        if (animator == null)
        {
            Debug.LogError("Animator not found! Make sure the Animator is on ShopSprite.");
        }
        else
        {
            // Set animation speed to half (0.5x)
            animator.speed = 0.5f;
        }
    }
}
