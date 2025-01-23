using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ [Header("Bubble Growth Settings")]
    public float growthRate = 1f;       // How quickly the bubble grows
    public float maxSize = 2f;         // Maximum size of the bubble
    public float minSize = 0.5f;       // Minimum size (bubble shrinks back when idle)
    public float shrinkRate = 0.5f;    // Base shrink rate
    public float shrinkMultiplier = 1.5f; // Multiplier for faster shrinking when bubble is larger

    [Header("Physics Settings")]
    public float liftForce = 5f;       // Upward force applied to the bubble
    public float gravityScale = 1f;    // Custom gravity to control descent

    [Header("Pop Settings")]
    public GameObject popEffect;       // Particle effect for when the bubble pops
    public AudioClip popSound;         // Sound effect for popping
    public AudioSource audioSource;    // AudioSource for playing sounds

    [Header("Particle Effects")]
    public ParticleSystem growEffect;  // Particle effect when bubble grows

    private Rigidbody2D rb;
    private Vector3 originalScale;
    private bool isGrowing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;

        rb.gravityScale = gravityScale;
        rb.linearDamping = 0.5f; 
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        ApplyPhysics();
    }

    private void HandleInput()
    {
        // Check if the spacebar is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            isGrowing = true;
            GrowBubble();
        }
        else
        {
            isGrowing = false;
            ShrinkBubble();
        }
    }

    private void GrowBubble()
    {
        // Increase size while holding space
        if (transform.localScale.x < maxSize)
        {
            transform.localScale += Vector3.one * growthRate * Time.deltaTime;

            // Play particle effect
            if (!growEffect.isPlaying)
            {
                growEffect.Play();
            }
        }
        else
        {
            // Pop the bubble if it exceeds max size
            PopBubble();
        }
    }

    private void ShrinkBubble()
    {
        // Gradually shrink the bubble back to its original size
        float currentShrinkRate = shrinkRate;

        // If the bubble is larger than the original size, shrink faster
        if (transform.localScale.x > originalScale.x)
        {
            currentShrinkRate *= shrinkMultiplier;
        }

        transform.localScale -= Vector3.one * currentShrinkRate * Time.deltaTime;

        // Prevent shrinking below original size
        if (transform.localScale.x < originalScale.x)
        {
            transform.localScale = originalScale;
        }

        // Stop the grow effect
        if (growEffect.isPlaying)
        {
            growEffect.Stop();
        }
    }

    private void ApplyPhysics()
    {
 
    if (isGrowing)
    {
        // Apply a continuous upward force while growing
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, liftForce);
    }
    else
    {
        // Allow natural gravity to take over when not growing
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y);
    
    }
    }

    private void PopBubble()
    {
        // Play pop particle effect
        if (popEffect != null)
        {
            Instantiate(popEffect, transform.position, Quaternion.identity);
        }

        // Play pop sound
        if (popSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(popSound);
        }

        // Destroy the bubble
        Debug.Log("Bubble Popped!");
        Destroy(gameObject);

        // Trigger Game Over or Restart
        // (Add your own GameManager logic here if needed)
    }
}

