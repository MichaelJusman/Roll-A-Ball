using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1.0f;
    public int pickupCount;
    int totalPickups;
    private bool wonGame = false;
    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text winText;
    public GameObject inGamePanel;
    public GameObject winPanel;
    public Image pickupFill;
    public Image frontPickupFill;
    float pickupChunk;
    float timeElapsed;
    public float lerpDuration = 2;
    float startValue = 0;
    float endValue;
    float valueToLerp;
    public GameObject gameOverScreen;
    GameObject resetPoint;
    bool resetting = false;
    Color originalColour;
    bool grounded = true;

    void Start()
    {
        //Turn off our win panel object
        winPanel.SetActive(false);
        //Turn on our ingame panel
        inGamePanel.SetActive(true);
        //Gets the rigid bidy component attatcged to this game object
        rb = GetComponent<Rigidbody>();
        //Workout how many picups are in the scene and store in variable (pickupCount)
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //Assign the amount of pickups to the total pickups
        totalPickups = pickupCount;
        //Work out the ammount of fill for our pickup fill
        pickupChunk = 1.0f / totalPickups;
        pickupFill.fillAmount = 0;
        endValue = pickupChunk;
        //Display the pickups to the users
        CheckPickups();
        //Reset point
        resetPoint = GameObject.Find("Reset Point");
        originalColour = GetComponent<Renderer>().material.color;

        //Pause function
        Time.timeScale = 1;
    }

    void FixedUpdate()
    {
        //If we have won the game, return from the function
        if (wonGame == true)
            return;

        //Reset Function
        if (resetting)
            return;

        //Boost, Only able to move when grounded is true
        if (grounded)
        {
            //Store the horizontal value in a float
            float moveHorizontal = Input.GetAxis("Horizontal");
            //Store the vertical value in a float
            float moveVertical = Input.GetAxis("Vertical");

            //Create a new vector 3 based on the horizontal and vertical values
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            //Add force to our rigidbody from our movement vector times our speed
            rb.AddForce(movement * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If we collide with a pickup, destroy the pickup
        if (other.gameObject.CompareTag("Pickup"))
        {
            //Decrement the pickupCount when we collide with a pickup
            pickupCount -= 1;
            //increase the fill amount of our pickup fill image
            pickupFill.fillAmount = pickupFill.fillAmount + pickupChunk;
            //Display the pickups to the users
            endValue = pickupFill.fillAmount + pickupChunk;
            StartCoroutine(Lerp());
            CheckPickups();

            Destroy(other.gameObject);
        }
    }
    IEnumerator Lerp()
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        valueToLerp = endValue;
        frontPickupFill.fillAmount = valueToLerp;
    }

    void CheckPickups()
    {
        //Display the new pickup count to the player
        scoreText.text = "Pickups Left " + pickupCount.ToString() + "/" + totalPickups.ToString();
        //Check if the pickupCount == 0
        if (pickupCount == 0)
        {
            //If pickupCount == 0, display win message
            winPanel.SetActive(true);
            //Turn off our ingame panel
            inGamePanel.SetActive(false);
            //Remove controls from player
            wonGame = true;
            //Set the velocity of the rigidbody to zero
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    //Temporary reset functionality
    public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    //Check if the pickupCount == 0
    //If pickupCount == 0, display win message, remove controls from player
    //Create a win condition that happens when pickupCount == 0

    //Reset Collision function
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }
    }

    public IEnumerator ResetPlayer()
    {
        resetting = true;
        GetComponent<Renderer>().material.color = Color.black;
        rb.velocity = Vector3.zero;
        Vector3 startPos = transform.position;
        float resetSpeed = 2f;
        var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, resetPoint.transform.position, i);
            yield return null;
        }
        GetComponent<Renderer>().material.color = originalColour;
        resetting = false;
    }

    //If exitting ground collider, disable ball movement
    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.CompareTag("Ground"))
            grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = false;
    }
}
