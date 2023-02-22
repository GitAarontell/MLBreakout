using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixBallBounceOnSideWalls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // only ball not paddle
        if (collision.gameObject.name == "Ball")
        {
            Rigidbody2D ball = collision.gameObject.GetComponent<Rigidbody2D>();
            // if its the left wall
            if (collision.otherCollider.gameObject.name == "WallLeft")
            {
                // get angle to vector going right
                float curAngle = Vector2.SignedAngle(Vector2.right, ball.velocity);
                // so any angle under 5 degrees needs to be altered
                if (curAngle < 5 && curAngle > -5)
                {
                    setNewVelocity(curAngle, Vector2.right, ball);
                }
                
            } else if (collision.otherCollider.gameObject.name == "WallRight")
            {
                // get angle to vector going left
                float curAngle = Vector2.SignedAngle(Vector2.left, ball.velocity);
                if (curAngle < 5 && curAngle > -5)
                {
                    setNewVelocity(curAngle, Vector2.left, ball);
                }
            }
            
        }
    }

    private void setNewVelocity(float curAngle, Vector2 axis, Rigidbody2D ball)
    {
        float newAngle = curAngle + 5 * -1;
        Quaternion newBallVectorAngle = Quaternion.AngleAxis(newAngle, Vector3.forward);
        ball.velocity = newBallVectorAngle * axis * ball.velocity.magnitude;
    }
}
