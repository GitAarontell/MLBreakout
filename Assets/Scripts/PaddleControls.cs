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
        transform.Translate(movement * 8.0f * Time.deltaTime);
    }
    // OnCollisionEnter info https://docs.unity3d.com/ScriptReference/Collider.OnCollisionEnter.html
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collider info https://docs.unity3d.com/ScriptReference/Collider2D.html
        // othercollider refers to the object that collided with the ball
        Collider2D Paddle = collision.otherCollider;

        // GetContact(0) gives the contact object I think not sure can't find too much info
        // gets x value from contact point 
        // contact point info: https://docs.unity3d.com/ScriptReference/ContactPoint2D.html
        float xPositionContactPoint = collision.GetContact(0).point.x;
        // x position of the paddle is the center x position of the paddle
        float xPositionPaddle = Paddle.transform.position.x;

        // gets difference in X positions so we know which side the ball hit
        float diffInXPos = xPositionPaddle - xPositionContactPoint;
        // size of paddle where x(width), y(height), and z(depth) of the paddle collider in Unity measurements 
        // print(Paddle.bounds.size);
        float widthOfPaddle = Paddle.bounds.size.x;

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
    }
}
