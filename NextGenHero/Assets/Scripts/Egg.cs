using UnityEngine;

public class Egg : MonoBehaviour
{
    float speed = 10f; // Speed of the egg
    Camera mainCamera; // Reference to the main camera
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Manager.Instance.numberOfEggs++; // Increment the number of eggs in the game manager
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime; // Move the egg forward
        
        // Check if the egg is out of the camera's view
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            Manager.Instance.numberOfEggs--; // Decrement the number of eggs in the game manager
            Destroy(gameObject); // Destroy the egg if it's out of view
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Waypoint"))
        {
            Manager.Instance.numberOfEggs--; // Decrement the number of eggs in the game manager
            Destroy(gameObject); // Destroy the egg on collision
        }
    }
}
