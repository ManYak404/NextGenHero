using UnityEngine;

public class Waypoint : MonoBehaviour
{
    float health = 1f; // Health of the waypoint
    float waypointRange = 1f; // Range for the waypoints
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetOpacity(health); // Set the opacity based on health
    }

    void SetOpacity(float opacity)
    {
        // Set the opacity of the waypoint
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = opacity; // Set the alpha value to the specified opacity
        GetComponent<SpriteRenderer>().color = color; // Apply the new color to the sprite renderer
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the tag "Egg"
        if (collision.gameObject.CompareTag("Egg"))
        {
            Debug.Log("Egg hit waypoint A!"); // Log message for egg collision
            health -= 0.25f; // Decrease health on egg collision
        }
        if(health<=0f)
        {
            // Random position within the screen bounds
            float randomX = Random.Range(-waypointRange, waypointRange);
            float randomY = Random.Range(-waypointRange, waypointRange);

            transform.position += new Vector3(randomX, randomY, 0);
            health = 1f;
        }
    }
}
