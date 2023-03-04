using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoringMLAgent : MonoBehaviour
{
    public GameObject Ball;
    public Transform respawnPoint;
    public Rigidbody2D ballRigidBody;

    [SerializeField] private int lives = 3;
    public int brickCount = 45;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball"){
            //Did this to add the delay
            this.lives -= 1;
            if (this.lives > 0)
            {

                Vector2 zeroVelocity = new Vector2(0, 0);
                ballRigidBody.velocity = zeroVelocity;
                Ball.transform.position = respawnPoint.position;
                Invoke("restartMovement", 2.0f);
            }
            /*else
            {
                this.spawnBallAway();
            }*/
        }
    }

    //Restarts the balls movement
    public void restartMovement()
    {
        //random direction for x with vector y always going down. COPIED FROM BallMovement Script
        float randX = Random.Range(-1.0f, 1.0f);
        Vector2 startForce = new Vector2(randX, -1);
        ballRigidBody.AddForce(startForce.normalized * 400f);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////// for ml agent
    public void respawn()
    {
        // ball was moved to outside of game so set velocity to zero so its not moving
        Vector2 zeroVelocity = new Vector2(0, 0);
        ballRigidBody.velocity = zeroVelocity;

        // then set the ball back to starting position
        Ball.transform.localPosition = new Vector3(-0f, -1.21f, 10f);

        // reset lives and brick count
        this.lives = 3;
        this.brickCount = 45;
        Invoke("restartMovement", 2f);

    }

    public void spawnBallAway()
    {
        Vector2 zeroVelocity = new Vector2(0, 0);
        ballRigidBody.velocity = zeroVelocity;
        Vector3 offscreenPos = new Vector3(2000.0f, 2000.0f);
        Ball.transform.localPosition = offscreenPos;
    }

    public void decreaseBrickCount()
    {
        this.brickCount -= 1;
    }
    public int getLives()
    {
        return this.lives;
    }

    public int getBrickCount()
    {
        return this.brickCount;
    }
    public void setBrickCount(int newCount)
    {
        this.brickCount = newCount;
    }
}
