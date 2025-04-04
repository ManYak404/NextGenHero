using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 0f; // Speed of the player
    float forwardSpeedUp = 3f; // Speed increment for forward movement
    float rotateSpeed = 80f; // Speed of rotation
    bool isMouseControl = true; // Flag to check if mouse control is enabled
    float secondsSinceLastFire = 0f; // Time since the last fire action
    float fireRate = 0.2f; // Rate of fire for the egg
    public GameObject eggPrefab; // Prefab for the egg object
    //Vector3 directionFacing = new Vector3(0, 1, 0); // Direction the player is facing
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SwitchControls(); // Check for control switch input
        }
        if (Input.GetKey(KeyCode.Space))  // Keeps detecting while key is held
        {
            FireEgg(); // Fire an egg when space is pressed
        }
        if (isMouseControl)
        {
            FollowMouse(); // Follow mouse position if mouse control is enabled
        }
        else
        {
            FollowKeyboard(); // Follow keyboard input if mouse control is disabled
        }
        if(Input.GetKey(KeyCode.Q))
        {
            Application.Quit(); // Quit the application if Q is pressed
        }
        secondsSinceLastFire += Time.deltaTime; // Increment time since last fire
    }

    void FireEgg()
    {
        if (secondsSinceLastFire >= fireRate) // Check if enough time has passed to fire again
        {
            // Fire an egg from the player
            GameObject egg = Instantiate(eggPrefab, transform.position, Quaternion.identity);
            egg.transform.up = transform.up; // Set the egg's direction to the player's direction
            Debug.Log("Firing egg!");
            secondsSinceLastFire = 0f; // Reset the timer
        }
    }

    void FollowMouse()
    {
        // Follow the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 for 2D
        transform.position = mousePosition;
    }

    void FollowKeyboard()
    {
        if(Input.GetKey(KeyCode.W))
        {
            speed += forwardSpeedUp * Time.deltaTime; // accelerate forward
        }
        if(Input.GetKey(KeyCode.S))
        {
            speed -= forwardSpeedUp * Time.deltaTime; // decelerate backward
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0,0,1) * rotateSpeed * Time.deltaTime); // rotate left
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-new Vector3(0,0,1) * rotateSpeed * Time.deltaTime); // rotate right
        }
        // Move the player forward in the direction it is facing
        transform.position +=  transform.up * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with an object tagged "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player collided with enemy!");
        }
    }

    void SwitchControls()
    {
        // Switch controls between keyboard and mouse
        isMouseControl = !isMouseControl; // Switch to other control
        Manager.Instance.isMouseControl = isMouseControl; // Update the game manager's control state
        Debug.Log("Switched controls!");
    }
}
