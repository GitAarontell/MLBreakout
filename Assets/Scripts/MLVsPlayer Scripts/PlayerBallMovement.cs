using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBallMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public AudioSource source;
    public GameObject gameWon;
    public Transform respawnPoint;
    private PlayerScoreCard scoreScript;


    private void Awake()
    {
        // required if we do not drag in rigid body into the reference in the ballmovement script in UI
        // so this grabs the rigidbody of this object without the dragging required
        // this.rigidBody = GetComponent<Rigidbody2D>();
        this.source = GetComponent<AudioSource>();
    }

    void Start()
    {

        // find the scorecard script object. This function is really slow, so we just want to use it once and
        // store it in a variable since we will use it everytime we decrement lives.
        scoreScript = FindObjectOfType<PlayerScoreCard>();

        // Invoke takes in string of function name and an integer representing seconds to delay
        // so this delays ball movement start by 1 seconds
        Invoke("startingForce", 1);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void startingForce()
    {
        // random direction for x with vector y always going down
        float randX = Random.Range(-1.0f, 1.0f);
        Vector2 startForce = new Vector2(randX, -1);

        this.rigidBody.AddForce(startForce.normalized * 400f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Brick")
        {
            // destroy brick object
            Destroy(collision.gameObject);

            // call the increase score script function and pass in the brick's sprite renderer to use color of brick inside that function
            scoreScript.increaseScore(collision.gameObject.GetComponent<SpriteRenderer>());
            // play this sound
            this.source.Play();

        }
        else if (collision.gameObject.name == "WallBottom")
        {
            // calls the decrease lives function from the scorecard script
            scoreScript.decreaseLives();
            this.killBall();
        }
        else
        {
            this.source.Play(); // if it hits the paddle
        }

    }

    public void killBall()
    {
        this.gameObject.transform.position = respawnPoint.position;
        // Need to zero out only the velocity since there should be no force (at least I think Unity shouldnt think there is based on oncollision being called on frame of "impact")
        Vector2 zeroVelocity = new Vector2(0, 0);
        this.gameObject.GetComponent<Rigidbody2D>().velocity = zeroVelocity;
        this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        //Did this to add the delay
        if (scoreScript.getLives() > 0)
            Invoke("restartMovement", 2.0f);
        else
        {
            Vector3 offscreenPos = new Vector3(2000.0f, 2000.0f);
            this.gameObject.transform.localPosition = offscreenPos;
        }
    }

    //Restarts the balls movement
    public void restartMovement()
    {
        //random direction for x with vector y always going down. COPIED FROM BallMovement Script
        float randX = Random.Range(-1.0f, 1.0f);
        Vector2 startForce = new Vector2(randX, -1);
        //Player.GetComponent<Rigidbody2D>().velocity = startForce;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(startForce.normalized * 400f);
    }
}
