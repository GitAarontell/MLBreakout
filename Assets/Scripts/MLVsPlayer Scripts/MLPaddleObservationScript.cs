using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.Barracuda; // required to use NNModel
using UnityEditor;
using Unity.MLAgents.Policies;

public class MLPaddleObservationScript : Agent
{
    // serialized feild just makes private variables show in the inspector
    [SerializeField] private GameObject ball;
    [SerializeField] private Rigidbody2D ballRigidBody;
    [SerializeField] private NNModel model30;
    [SerializeField] private NNModel model40;
    [SerializeField] private NNModel model50;
    [SerializeField] private NNModel model60;
    [SerializeField] private NNModel model120;
    [SerializeField] private NNModel model240;
    // A good rule of thumb for deciding what information to collect is to consider what you would need to calculate an analytical solution to the problem.
    public override void Initialize()
    {
        BehaviorParameters beh = this.gameObject.GetComponent<BehaviorParameters>();

        if (SceneSwap.time == 30) {
            beh.Model = model30;
            //print(beh.Model);
        } else if (SceneSwap.time == 40) 
        {
            beh.Model = model40;
        }
        else if (SceneSwap.time == 50)
        {
            beh.Model = model50;
        }
        else if (SceneSwap.time == 60)
        {
            beh.Model = model60;
        }
        else if (SceneSwap.time == 120)
        {
            beh.Model = model120;
        }
        else if (SceneSwap.time == 240)
        {
            beh.Model = model240;
        }
        print(beh.Model);
    }
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
        // SetModel("HitAllBricks", model); works in runtime but not in Initialize
        Vector3 movement = new Vector3(actions.DiscreteActions[0] * -1.0f + actions.DiscreteActions[1], 0, 0);
        this.gameObject.transform.localPosition += movement * 8.0f * Time.deltaTime;
    }

}

