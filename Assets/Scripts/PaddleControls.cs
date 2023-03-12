using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControls : MonoBehaviour
{
    public BoxCollider2D myCollider;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
        // gets either 1 or -1 depending on whether "A"(-1) was pushed or "D"(1) was pushed. Also works for arrows. This is Unity's default input controls
        float x = Input.GetAxis("Horizontal");
        // I create a vector3, although a vector2 would probably also work, and I just place this value from user input into movement vector
        Vector3 movement = new Vector3(x, 0, 0);
        // tranform accesses the position of the object this script is attached to
        // since vectors view things in world direction, I used Translate to use relative direction, so moving to the left is moving to the paddles left not the world coordinates left vice versa
        this.gameObject.transform.Translate(movement * 8.0f * Time.deltaTime);
        
        
    }
    // OnCollisionEnter info https://docs.unity3d.com/ScriptReference/Collider.OnCollisionEnter.html
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball"){
            // Collider info https://docs.unity3d.com/ScriptReference/Collider2D.html
            // othercollider refers to the object that collided with the ball
            Collider2D Paddle = collision.otherCollider;
            Rigidbody2D ball = collision.gameObject.GetComponent<Rigidbody2D>();
            // GetContact(0) gives the contact object I think not sure can't find too much info
            // gets x value from contact point 
            // contact point info: https://docs.unity3d.com/ScriptReference/ContactPoint2D.html
            float xPositionContactPoint = collision.GetContact(0).point.x;
            // x position of the paddle is the center x position of the paddle
            float xPositionPaddle = Paddle.transform.position.x;

            // gets difference in X positions so we know which side the ball hit. If positive then ball was on the left side and if negative then ball hit the right side.
            float diffInXPos = xPositionPaddle - xPositionContactPoint;

            // size of paddle where x(width), y(height), and z(depth) of the paddle collider in Unity measurements 
            float widthOfPaddle = Paddle.bounds.size.x / 2;

            // if we get percent closer to zero then it is closer to center, wheras if we get a value further from 0 with the max being 1(100%), then we are towards the edge. 
            // Again, left side of paddle represents a positive number, and right side represents a negative number. This is because xPositionPaddle is the center position of paddle.
            float positionPercent = diffInXPos / widthOfPaddle;


            // Vector2 methods: https://docs.unity3d.com/ScriptReference/Vector2.html
            // signed angle gets angle in float from first parameter to second parameter. Vector2.up = (0, 1) which is a straight line on the y axis. Signed angle gets closest angle as well,
            // which, avoids angles under 180 or -180 from that vector2.up.
            float curAngle = Vector2.SignedAngle(Vector2.up, ball.velocity);
                       
            // reflectionAngle is just opposite of current angle
            float reflectionAngle = curAngle * -1;

            // so we take the current angle, then we add onto that some position where it hit the paddle as a percentage multiplied by 30 degree.
            // Ex: if the ball is coming at the paddle at 60 degrees and hits the center of the left side of the paddle, then the pecent will be close to 0 negative number, so we will have the same
            // reflection angle made by the engine basically. If however, it bounced on the left edge, the percent value would be closer to -1 and we would add -30 degrees to the 60 degree, so it would
            // bounce more backwards possibly.
            float newAngle = curAngle + positionPercent * 30;

            // the angle axis allows for rotation along a specific axis. Vector3.foreward(0, 0, 1) is the z-axis which causes rotation of our objects like if we were to be looking at a clock hand
            // moving counterclockwise or clockwise. the new angle parameter is an angle in degree of float type.
            Quaternion newBallVectorAngle = Quaternion.AngleAxis(newAngle, Vector3.forward);

            // the balls velocity is the x and y vector combined. This method is used because rotating the actual ball doesn't change the vector angle since the physics engine just
            // changes the vector after we rotate. So, we need to actually change the vector of the ball itself.
            // by multiplying our quaternion(newBallVectorAngle) by a vector we can rotate that vector. Then by multiplying that vector by the current magnitude of the ball we maintain its speed(Magnitude).
            ball.velocity = newBallVectorAngle * Vector2.up * ball.velocity.magnitude;
            
        }



        // Other Notes:

        // so GetContacts returns the number of contacts that were put into the contactArray, and it requires a ContactPoint2D array to be passed in that must be big enough to store the number
        // of contacts. It also reuses this array. There is only 1 contact when the ball hits the paddle, but maybe if the ball was shaped like a spike there would be more contact points, idk.

        // ContactPoint2D[] contactArray = new ContactPoint2D[10];
        // int contactNum = collision.GetContacts(contactArray);

        // so these two things return the same value. GetContacts returns the length of the array and then puts all the ContactPoint2D's in the array passed in(contactArray).
        // then from there you can use .point with the index value to get the point. So the first point of contact will always be at index 0. Obviously, using GetContact(0) is quicker
        // for this case.

        // print(contactArray[0].point);
        // print(collision.GetContact(0).point);

        /*// Quaternions are used for rotation and avoid gimbal lock, best to use them when dealing with any rotation instead of Euler angles
        Quaternion ballRotation = collision.gameObject.transform.rotation;
        // print(ballRotation);
        // converting euler to quaternion: https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
        // Quaternion rotation = Quaternion.Euler(0, 0, 130 * positionPercent);
        // this.gameObject.transform.rotation = rotation;
        // collision.gameObject.transform.rotation = rotation;
        Quaternion rotation = Quaternion.Euler(0, 0, 130 * positionPercent);
        ball.MoveRotation(rotation);*/


        // Notes on physics of rigidbody

        // r = rigidbody

        // speed = r.velocity.magnitude;
        // angularspeed = r.angularVelocity.magnitude;



        // relative velocity prints the velocity of the ball's rigid body before it redirects, so the x and y velocity right before the bounce
        /*print(ball.velocity.magnitude);
        print("Starting:");
        print(collision.relativeVelocity.x);
        print(collision.relativeVelocity.x * positionPercent);*/
        // Experiment with adding forces, the problem is the forces are a bit hard to translate to velocity so maybe using rotation would be better
        // Vector2 newForce = new Vector2(collision.relativeVelocity.x * 100 * positionPercent % 8, collision.relativeVelocity.y * -1);
        /*print(ball.velocity.magnitude);
        print(newForce);
        // ball.velocity = Vector2.ClampMagnitude(newForce, 8);
        ball.AddForce(newForce);
        print(ball.velocity.magnitude);*/
    }
}
