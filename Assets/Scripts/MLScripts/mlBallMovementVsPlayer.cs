using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mlBallMovementVsPlayer : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public AudioSource source;
    public scoringMLAgent scoringMLAgentScript;
    public PaddleMLAgent paddleMLAgentScript;
    private MLScoreCard scoreScript;
    


    private void Awake()
    {
        // required if we do not drag in rigid body into the reference in the ballmovement script in UI
        // so this grabs the rigidbody of this object without the dragging required
        // this.rigidBody = GetComponent<Rigidbody2D>();
        this.source = GetComponent<AudioSource>();
    }

    void Start()
    {
        // Invoke takes in string of function name and an integer representing seconds to delay
        // so this delays ball movement start by 1 seconds
        Invoke("startingForce", 1);

        // Find the score card
        scoreScript = FindObjectOfType<MLScoreCard>();
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
        // when ball hits brick
        if (collision.gameObject.name == "Brick")
        {
            // destroy brick object
            Destroy(collision.gameObject);

            // rewards ml agent for hitting brick with 2 points
            paddleMLAgentScript.brickCollisionReward();

            // print(collision.otherRigidbody.velocity.magnitude);

            // decrease brick count
            scoringMLAgentScript.decreaseBrickCount();

            // Increment score
            scoreScript.increaseScore(collision.gameObject.GetComponent<SpriteRenderer>());
            // play this sound
            this.source.Play();
        }
        // when ball hits bottom wall
        else if (collision.gameObject.name == "WallBottom")
        {
            // calls the decrease lives function from the scorecard script
            paddleMLAgentScript.punishment();

            // calls the decrease lives function from the scorecard script
            scoreScript.decreaseLives();
        }
        else
            this.source.Play(); // if it hits the paddle


    }
}

