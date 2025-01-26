using UnityEngine;
using System.Collections;
public class Enemy : MonoBehaviour
{
    public EnemyData enemyData; // Assign the Scriptable Object in Inspector

    private Rigidbody2D rb;
    public GameObject popEffect;       // Particle effect for when the bubble pops
    public AudioClip[] popSounds;         // Sound effect for popping
    private AudioSource audioSource;
    private ScoreManager scoreManager;
    void Start()
    {
        // Initialize the enemy's properties based on the Scriptable Object
        transform.localScale = Vector3.one * enemyData.size;

        audioSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    void OnMouseDown()
    {
        // If the enemy can be popped
        if (enemyData.canBePopped)
        {
            scoreManager.AddScore(enemyData.pointsOnPop);
            DestroyEnemy();
        }
        else
        {
            Debug.Log($"{enemyData.enemyName} cannot be popped! Avoid it.");
            // Optional: Add penalty logic or effects
        }
    }

    private IEnumerator PlaySoundAndDestroy()
    {
        var randomSound = popSounds[Random.Range(0, popSounds.Length)];
        audioSource.PlayOneShot(randomSound);
        var collider = GetComponent<BoxCollider2D>();
        collider.enabled = false;

        DisableRenderers(gameObject);
        yield return new WaitForSeconds(randomSound.length); // Wait for the sound to finish
        Destroy(gameObject);
    }
    public void DestroyEnemy()
    {
        Debug.Log($"Popped {enemyData.enemyName}! Gained {enemyData.pointsOnPop} points.");
        // Add score logic here

        // Instantiate pop effect if available
        if (popEffect != null)
        {
            Instantiate(popEffect, transform.position, Quaternion.identity);
        }

        // Play pop sound and destroy after sound finishes
        if (popSounds != null && audioSource != null)
        {
            StartCoroutine(PlaySoundAndDestroy());
        }
        else
        {
            Destroy(gameObject); // Destroy immediately if no sound
        }
    }

    private void DisableRenderers(GameObject obj)
    {
        // Disable the renderer of the current object
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        // Loop through all child objects and disable their renderers
        foreach (Transform child in obj.transform)
        {
            DisableRenderers(child.gameObject); // Recursively disable renderers
        }
    }
    void Update()
    {

    }
}
