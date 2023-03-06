using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


public class MLPaddleObservationScript : Agent
{
    // serialized feild just makes private variables show in the inspector
    [SerializeField] private GameObject ball;
    [SerializeField] private Rigidbody2D ballRigidBody;


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

        Vector3 movement = new Vector3(actions.DiscreteActions[0] * -1.0f + actions.DiscreteActions[1], 0, 0);
        this.gameObject.transform.localPosition += movement * 8.0f * Time.deltaTime;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if the paddle hits the ball, it gets 1 point
        if (collision.gameObject.name == "Ball")
        {
            SetReward(+1f);
        }
    }


}

