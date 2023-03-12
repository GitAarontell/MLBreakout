using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixBallBounceOnSideWalls : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // only ball not paddle
        if (collision.gameObject.name == "Ball")
        {
            
            Rigidbody2D ball = collision.gameObject.GetComponent<Rigidbody2D>();
            Collider2D wall = collision.otherCollider;
            float yPositionContactPoint = collision.GetContact(0).point.y;
            float yPositionWall = wall.transform.position.y;
            float diffYPosition = yPositionContactPoint - yPositionWall;
            float heightOfWall = wall.bounds.size.y / 2;
            float positionPercent = diffYPosition / heightOfWall;
            // if its the left wall

            if (collision.otherCollider.gameObject.name == "WallLeft")
            {
                // get angle to vector going right
                float curAngle = Vector2.SignedAngle(Vector2.right, ball.velocity);
                // so any angle under 5 degrees needs to be altered
                if (curAngle < 5 && curAngle > -5)
                {
                    float newAngle = curAngle + 5;
                    setNewVelocity(-newAngle, Vector2.right, ball, positionPercent);
                }

            }
            else if (collision.otherCollider.gameObject.name == "WallRight")
            {
                // get angle to vector going left
                float curAngle = Vector2.Angle(Vector2.left, ball.velocity);
                if (curAngle < 5 && curAngle > -5)
                {
                    float newAngle = curAngle + 5;
                    setNewVelocity(newAngle, Vector2.left, ball, positionPercent);
                }
            }
            else if (collision.otherCollider.gameObject.name == "WallTop") {
                float xPositionContactPoint = collision.GetContact(0).point.x;
                float xPositionWall = wall.transform.position.x;
                float diffXPosition = xPositionContactPoint - xPositionWall;
                float widthOfWall = wall.bounds.size.x / 2;
                positionPercent = diffXPosition / widthOfWall;
                setNewTopVelocity(135, ball, positionPercent);
            }
        }
    }

    private void setNewVelocity(float newAngle, Vector2 axis, Rigidbody2D ball, float positionPercent)
    {
        Quaternion newBallVectorAngle;
        if (positionPercent > 0)
        {
            newBallVectorAngle = Quaternion.AngleAxis(newAngle, Vector3.forward);
        }
        else {
            newBallVectorAngle = Quaternion.AngleAxis(-newAngle, Vector3.forward);
        }
        
        ball.velocity = newBallVectorAngle * axis * ball.velocity.magnitude;
    }

    private void setNewTopVelocity(float newAngle, Rigidbody2D ball, float positionPercent)
    {
        Quaternion newBallVectorAngle;

        if (positionPercent > .9)
        {
            newBallVectorAngle = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.velocity = newBallVectorAngle * Vector2.up * ball.velocity.magnitude;
        }
        else if (positionPercent < -.9)
        {
            newBallVectorAngle = Quaternion.AngleAxis(-newAngle, Vector3.forward);
            ball.velocity = newBallVectorAngle * Vector2.up * ball.velocity.magnitude;
        }

        
    }
}
