using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public AudioSource source;
    private ScoreCard scoreScript;
    private SceneSwap scene_swap;

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
        scoreScript = FindObjectOfType<ScoreCard>();
        // find the SceneSwap Object.
        scene_swap = FindObjectOfType<SceneSwap>();
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
            // Find scorecard component, kind of high time complexity but not sure of another way to find it
            // call the increase score script function and pass in the brick's sprite renderer to use color of brick inside that function
            scoreScript.increaseScore(collision.gameObject.GetComponent<SpriteRenderer>());
            // play this sound
            this.source.Play();
            // End game if no bricks are left
            if (GameObject.FindGameObjectsWithTag("Brick").Length == 1)
            {
                Debug.Log("Final Brick Destroyed");
                // SceneManager.LoadScene("Level1");
                scene_swap.IncrementLevel();
                scene_swap.PlayNormalBreakout();
            }
        }
        else if (collision.gameObject.name == "WallBottom")
        {
            // calls the decrease lives function from the scorecard script
            scoreScript.decreaseLives();
        }
        else
            this.source.Play(); // if it hits the paddle


    }
}
