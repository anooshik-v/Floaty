using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public float speed = 5f; // Speed of the enemy
    public float destroyXPosition = -20f; // X position at which the enemy is destroyed

    private void Update()
    {
        // Move the enemy to the left
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Destroy the enemy if it goes out of range
        if (transform.position.x < destroyXPosition)
        {
            Destroy(gameObject);
        }
    }
}