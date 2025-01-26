using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 2f;
    [SerializeField] private int size = 1; // Size of the bubble
    private Vector2 screenBounds;
    [SerializeField] private AudioClip[] popSounds;         // Sound effect for popping
    private AudioSource audioSource;    // AudioSource for playing sounds

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        audioSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Make the bubble fall down
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        // Check if the bubble has fallen out of the screen bounds
        if (transform.position.y < -screenBounds.y)
        {
            Destroy(gameObject); // Destroy the bubble if it falls off the screen
        }
    }



    public void MergeBubble(Bubble otherBubble)
    {
        // Merge this bubble with another one to create a bigger bubble
        this.size += otherBubble.size;
        var randomSound = popSounds[Random.Range(0, popSounds.Length)];
        audioSource.PlayOneShot(randomSound);
        Destroy(otherBubble.gameObject); // Destroy the merged bubble
        UpdateSize();
    }

    public void UpdateSize()
    {
        // Update the size of the bubble based on its size
        transform.localScale = new Vector3(size, size, 1);
    }
    private void OnMouseDown()
    {
        // If the bubble is clicked, pop it and destroy it
        PopBubble();
    }

    private void PopBubble()
    {
        // Pop the bubble (you can add an effect or sound here)
        StartCoroutine(PlaySoundAndDestroy());

    }

    public void SetSize(int size){ this.size= size;}

    private IEnumerator PlaySoundAndDestroy()
    {
        var randomSound = popSounds[Random.Range(0, popSounds.Length)];
        audioSource.PlayOneShot(randomSound);
        DisableRenderers(gameObject);
        yield return new WaitForSeconds(randomSound.length); // Wait for the sound to finish
        Destroy(gameObject);
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
}
