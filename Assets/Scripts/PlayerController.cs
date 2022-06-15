using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1.0f;
    public int pickupCount;
    private bool wonGame = false;

    void Start()
    {
        //Gets the rigid bidy component attatcged to this game object
        rb = GetComponent<Rigidbody>();
        //Workout how many picups are in the scene and store in variable (pickupCount)
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //Display the pickups to the users
        CheckPickups();
    }

    void FixedUpdate()
    {
        //If we have won the game, return from the function
        if (wonGame == true)
            return;

        //Store the horizontal value in a float
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Store the vertical value in a float
        float moveVertical = Input.GetAxis("Vertical");

        //Create a new vector 3 based on the horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //Add force to our rigidbody from our movement vector times our speed
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        //If we collide with a pickup, destroy the pickup
        if (other.gameObject.CompareTag("Pickup"))
        {
            //Decrement the pickupCount when we collide with a pickup
            pickupCount -= 1;
            //Display the pickups to the users
            CheckPickups();

            Destroy(other.gameObject);
        }
    }

    void CheckPickups()
    {
        //Display the new pickup count to the player
        Debug.Log("Pickup Count" + pickupCount);

        //Check if the pickupCount == 0
        if (pickupCount == 0)
        {
            //If pickupCount == 0, display win message
            Debug.Log("You Win");
            //Remove controls from player
            wonGame = true;
            //Set the velocity of the rigidbody to zero
            rb.velocity = Vector3.zero;
        }
    }
    
    //Check if the pickupCount == 0
    //If pickupCount == 0, display win message, remove controls from player
    //Create a win condition that happens when pickupCount == 0
}
