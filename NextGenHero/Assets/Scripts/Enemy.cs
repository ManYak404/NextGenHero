using UnityEngine;

public class Enemy : MonoBehaviour
{
    float health = 100f; // Health of the enemy
    static GameObject[] waypoints = new GameObject [6]; // Array of waypoints for the enemy to follow
    int currentWaypoint; // Current waypoint the enemy is following
    bool isWaypointMovement = true; // Flag to check if waypoint movement is enabled
    float speed = 4f; // Speed of the enemy

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find and assign the waypoint GameObjects
        waypoints[0] = GameObject.Find("Waypoint_A"); 
        waypoints[1] = GameObject.Find("Waypoint_B");
        waypoints[2] = GameObject.Find("Waypoint_C");
        waypoints[3] = GameObject.Find("Waypoint_D");
        waypoints[4] = GameObject.Find("Waypoint_E");
        waypoints[5] = GameObject.Find("Waypoint_F");
        currentWaypoint = Random.Range(0, waypoints.Length); // Randomly select a waypoint to start from
    }

    // Update is called once per frame
    void Update()
    {
        isWaypointMovement = Manager.Instance.isWaypointMovement; // Get the waypoint movement flag from the game manager
        if(isWaypointMovement)
            WaypointMovement(); // Call the waypoint movement function
        else
            RandomWaypointMovement(); // Call the random waypoint movement function
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Egg"))
        {
            Debug.Log("Egg hit enemy!"); // Log message for egg collision
            health -= 25f; // Decrease health on egg collision
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit enemy!"); // Log message for egg collision
            health -= 100f; // Decrease health on player collision
        }
        if (health <= 0f)
        {
            Manager.Instance.PlaneDestroyed(); // Notify the game manager that the enemy is destroyed
            Destroy(gameObject); // Destroy the enemy if health is zero or less
        }
    }

    void WaypointMovement()
    {
        // Move towards the current waypoint
        transform.up = waypoints[currentWaypoint].transform.position - transform.position; // Set the enemy's direction towards the next waypoint
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, speed*Time.deltaTime); // Move the enemy towards the waypoint at a constant speed
        // Check if the enemy has reached the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) < 0.05f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length; // Move to the next waypoint in a loop
        }
    }

    void RandomWaypointMovement()
    {
        // Move towards the current waypoint
        transform.up = waypoints[currentWaypoint].transform.position - transform.position; // Set the enemy's direction towards the next waypoint
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, speed*Time.deltaTime); // Move the enemy towards the waypoint at a constant speed
        // Check if the enemy has reached the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) < 0.05f)
        {
            currentWaypoint = Random.Range(0, waypoints.Length); // Randomly select a waypoint to start from
        }
    }
}
