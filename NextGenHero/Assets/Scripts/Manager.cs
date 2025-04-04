using System.Globalization;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;  // Required for UI Toolkit

public class Manager : MonoBehaviour
{
    public static Manager Instance; // Singleton instance of the Manager class
    int planeCount = 0; // Number of planes in the scene
    public int numberOfEggs = 0; // Number of eggs fired
    int numberOfPlanesDestroyed = 0; // Number of enemies destroyed
    int planeLimit = 10; // Maximum number of planes allowed in the scene
    public bool isMouseControl = true; // Flag to check if mouse control is enabled
    public bool isWaypointMovement = true; // Flag to check if waypoint movement is enabled
    bool isWaypointShown = true; // Flag to check if waypoints are shown
    public GameObject[] waypoints = new GameObject[6]; // Array of waypoints for the planes to follow
    public GameObject planePrefab; // Prefab for the plane object
    public Camera mainCamera; // Reference to the main camera
    public UIDocument uiDoc; // Reference to the UI document
    string statsText; // Text to display in the UI
    Label myLabel; // Label to display the stats text
    void Awake()
    {
        myLabel = new Label(""); // Create a new label with the text to display
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the number of planes is less than the limit
        if (planeCount < planeLimit)
        {
            // Call the SpawnPlane method to spawn a new plane
            SpawnPlane();
            // Increment the plane count
            planeCount++;
        }
        statsText = "Hero Mode: " + isMouseControl + "     Number of Eggs: " + numberOfEggs + "     Enemy Count: " + planeCount + "     Enemies Destroyed: " + numberOfPlanesDestroyed + "     Sequential Mode: " + isWaypointMovement + "     Waypoints Shown: " + isWaypointShown; // Text to display in the UI
        myLabel.text = statsText; // Update the label text with the current stats
        uiDoc.rootVisualElement.Add(myLabel); // Add the label to the UI document
        if (Input.GetKeyDown(KeyCode.J))
        {
            isWaypointMovement = !isWaypointMovement; // Toggle waypoint movement
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleWaypoints(); // Toggle waypoint visibility
        }
    }

    void SpawnPlane()
    {
        // Check if the number of planes is less than the limit
        if (planeCount < planeLimit)
        {
            // Get the camera's screen boundaries in world space
            Vector3 min = mainCamera.ViewportToWorldPoint(new Vector3(0.1f, 0.1f, mainCamera.nearClipPlane));
            Vector3 max = mainCamera.ViewportToWorldPoint(new Vector3(0.9f, 0.9f, mainCamera.nearClipPlane));

            // Random position within the screen bounds
            float randomX = Random.Range(min.x, max.x);
            float randomY = Random.Range(min.y, max.y);

            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            // Spawn object
            Instantiate(planePrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void PlaneDestroyed()
    {
        // Decrement the plane count when a plane is destroyed
        planeCount--;
        numberOfPlanesDestroyed++; // Increment the number of enemies destroyed
    }

    void ToggleWaypoints()
    {
        isWaypointShown = !isWaypointShown; // Toggle the waypoint visibility flag
        // Hide the waypoint by setting its opacity to 0
        foreach (GameObject waypoint in waypoints)
        {
            waypoint.GetComponent<SpriteRenderer>().enabled = !waypoint.GetComponent<SpriteRenderer>().enabled; // Toggle the visibility of the waypoint
            waypoint.GetComponent<Collider2D>().enabled = !waypoint.GetComponent<Collider2D>().enabled; // Toggle the collider of the waypoint
        }
    }
}
