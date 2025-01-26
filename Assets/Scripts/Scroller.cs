using UnityEngine;

public class Scroller : MonoBehaviour
{  public float scrollSpeed = 2f;

    private float backgroundWidth; // The width of the background

    void Start()
    {
        // Get the width of the background sprite
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Move the background to the left
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // Reset position when it goes off-screen
        if (transform.position.x <= -backgroundWidth*1.2)
        {
            Vector3 newPos = transform.position;
            newPos.x += backgroundWidth * 2; // Reset to the start position
            transform.position = newPos;
        }
    }
}
