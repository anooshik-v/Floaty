using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Bubble Growth Settings")]
    [SerializeField] float growthRate = 1f;       // How quickly the bubble grows
    public float maxSize = 2f;         // Maximum size of the bubble
    public float minSize = 0.2f;       // Minimum size (bubble shrinks back when idle)
    public float shrinkRate = 0.5f;    // Base shrink rate
    public float shrinkMultiplier = 1.5f; // Multiplier for faster shrinking when bubble is larger
    public AudioClip growSound;         // Sound effect for popping

    [Header("Physics Settings")]
    public float liftForce = 5f;       // Upward force applied to the bubble
    public float gravityScale = 1f;    // Custom gravity to control descent

    [Header("Pop Settings")]
    public AudioClip popSound;         // Sound effect for popping
    public AudioSource audioSource;    // AudioSource for playing sounds

    [Header("Particle Effects")]
    public ParticleSystem growEffect;  // Particle effect when bubble grows
    public ParticleSystem popEffect;       // Particle effect for when the bubble pops

    private Rigidbody2D rb;
    private Vector3 originalScale;
    private bool isGrowing;
    public float upperBoundary = 5f; // The highest point the player can reach
    public float lowerBoundary = -5f; // The lowest point the player can go
    public Vector3 respawnPosition = new Vector3(0, 0, 0); // Original spawn position
    private HealthManager healthManager;
    [Header("Immunity Settings")]
    public float immunityDuration = 2f;
    private bool isImmune = false;
    [Header("Visual Settings")]
    public Color immuneColor = Color.yellow; // Color during immunity
    public Color normalColor = Color.white; // Normal bubble color
    private SpriteRenderer spriteRenderer;
    private DifficultyManager difficultyManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthManager = FindFirstObjectByType<HealthManager>();  // Get reference to the HealthManager

        originalScale = transform.localScale;

        // Adjust gravity for smooth descent
        rb.gravityScale = 0;
        rb.linearDamping = 0.5f;
        respawnPosition = transform.position;
        audioSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();
        difficultyManager = FindFirstObjectByType<DifficultyManager>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

    }


    void Update()
    {
        HandleInput();
        // Check if the player is outside the boundaries
        if (transform.position.y > upperBoundary || transform.position.y < lowerBoundary)
        {
            if (difficultyManager.isPlaying)
                PopBubble();
        }

    }

    void FixedUpdate()
    {
        if (difficultyManager.isPlaying)
            ApplyPhysics();
    }

    private void HandleInput()
    {
        // Check if the spacebar is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            isGrowing = true;
            GrowBubble();

            // Check if the bubble size is too large
            if (transform.localScale.x >= maxSize || transform.localScale.y >= maxSize)
            {
                PopBubble();
            }
        }
        else
        {
            isGrowing = false;
            ShrinkBubble();

            // Check if the bubble size is too small
            if (transform.localScale.x <= minSize || transform.localScale.y <= minSize)
            {
                PopBubble();
            }
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


        // Stop the grow effect
        if (growEffect.isPlaying)
        {
            growEffect.Stop();
        }
    }

    private void ApplyPhysics()
    {
        rb.gravityScale = gravityScale;
        
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

        TakeDamage();

        // Play pop particle effect
        if (popEffect != null)
        {
            Instantiate(popEffect, transform.position, Quaternion.identity);
        }

        // Play pop sound
        if (popSound != null && audioSource != null)
        {
            var tempVolume = audioSource.volume;
            audioSource.volume = 1;
            audioSource.PlayOneShot(popSound);
            audioSource.volume = tempVolume;
        }

        // Destroy the bubble
        Debug.Log("Bubble Popped!");
        ResetBubble();


    }

    private void TakeDamage()
    {
        if (difficultyManager.isPlaying)
            healthManager.ReduceHealth();

        StartCoroutine(StartImmunity());


    }
    private System.Collections.IEnumerator StartImmunity()
    {
        isImmune = true;

        // Change visuals for immunity
        float elapsedTime = 0f;
        bool toggle = false;

        while (elapsedTime < immunityDuration)
        {
            toggle = !toggle;
            spriteRenderer.color = toggle ? immuneColor : normalColor; // Flashing effect

            elapsedTime += 0.2f; // Time between flashes
            yield return new WaitForSeconds(0.2f);
        }

        // End immunity
        isImmune = false;
        spriteRenderer.color = normalColor; // Reset to normal color
    }


    private void ResetBubble()
    {
        // Reset the bubble to its initial state (or respawn logic)
        transform.localScale = Vector3.one; // Reset to original size
        transform.position = respawnPosition; // Reset to initial position (if needed)
       
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Player has collided with the enemy!");

        var enemy = collider.gameObject.GetComponent<Enemy>();
        // Check if the collision is with an enemy
        if (enemy != null)
        {
            enemy.DestroyEnemy();
            Debug.Log("Player has collided with the enemy!");

            TakeDamage();
            transform.localScale = Vector3.one;
        }
    }


    public void EndTutorial()
    {
        Start();
        difficultyManager.isPlaying = true;
        rb.gravityScale = gravityScale;


    }
}