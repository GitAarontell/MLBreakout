using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.SceneManagement;


public class PaddleMLAgent : Agent
{
    // serialized feild just makes private variables show in the inspector
    [SerializeField] private GameObject ball;
    [SerializeField] private Rigidbody2D ballRigidBody;
    public GameObject bricksPrefab;
    public GameObject parent;


    // paddle position
    [SerializeField] private scoringMLAgent scoringMLAgentScript;
    private GameObject bricksBox;


    // this is basically the start() function for ml agents
    public override void Initialize()
    {
        bricksBox = GameObject.Find("Bricks");
    }

    public override void OnEpisodeBegin()
    {
        // used so the first time the scene loads it doesn't call this, sinc episode begin starts at every episode.
        // If we don't have this if check it will destroy the bricks already loaded and reload new ones. Doing this stops it
        // adding a little efficiency at the start.
        if (scoringMLAgentScript.getLives() < 1 || scoringMLAgentScript.getBrickCount() < 1)
        {
            // call this quickly so ball goes away
            scoringMLAgentScript.spawnBallAway();
            // destroy all the bricks
            Destroy(bricksBox);

            // recreate bricks
            bricksBox = Instantiate(bricksPrefab, parent.transform, false);
            // rename to Bricks
            bricksBox.name = "Bricks";
            // transform to this local position, not world position
            bricksBox.transform.localPosition = new Vector3(7.3f, 2.7f, 10f);
            // reposition paddle
            this.gameObject.transform.localPosition = new Vector3(0f, -10f, 10f);
            // respawn ball
            scoringMLAgentScript.respawn();

        }

    }
    // A good rule of thumb for deciding what information to collect is to consider what you would need to calculate an analytical solution to the problem.

    public override void CollectObservations(VectorSensor sensor)
    {
        // space size needs to be the same size as the data we are passing in. Vector3's count as 3 data points so on vector 3 will need size of 3
        // in vector observation space size
        // if the lives goes to zero end episode and set reward

        // giving the position of the paddle to the AI
        sensor.AddObservation(transform.localPosition); //vector 3
        // giving the position of the ball to the AI
        sensor.AddObservation(ball.transform.localPosition); // vector 3

        sensor.AddObservation(Vector2.SignedAngle(Vector2.up, ballRigidBody.velocity)); // float

        sensor.AddObservation(ballRigidBody.velocity); // vector 2

        sensor.AddObservation(Vector3.Distance(this.transform.localPosition, ball.transform.localPosition)); // float


    }

    // these are actions the ML will take represented as numbers
    public override void OnActionReceived(ActionBuffers actions)
    {

        if (scoringMLAgentScript.getLives() < 1 || scoringMLAgentScript.getBrickCount() < 1)
        {
            EndEpisode();
        }
        Vector3 movement = new Vector3(actions.DiscreteActions[0] * -1.0f + actions.DiscreteActions[1], 0, 0);
        this.gameObject.transform.localPosition += movement * 8.0f * Time.deltaTime;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if the paddle hits the ball, it gets 1 point
        if (collision.gameObject.name == "Ball"){
            SetReward(+1f);
        }
    }

    // used for when the ball hits a brick then give AI a reward
    // this is called in the ball movement script
    // only for training AI so don't call when AI has completed training
    public void brickCollisionReward()
    {
        SetReward(2f);
    }

    public void punishment()
    {
        SetReward(-10f);
    }

}
