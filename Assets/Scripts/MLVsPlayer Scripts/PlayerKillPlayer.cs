using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerKillPlayer : MonoBehaviour
{
    public GameObject Player;
    public Transform respawnPoint;
    

    // make scoreScript available to all script functions
    public PlayerScoreCard scoreScript;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            killBall();
        }
  
    }

    public void killBall()
    {
       
        //***** I experimented with resetting the scene, but the progress with destorying bricks is erased.
        // Scene currentScene = SceneManager.GetActiveScene();
        // SceneManager.LoadScene(currentScene.name);
        Player.transform.position = respawnPoint.position;
        // Need to zero out only the velocity since there should be no force (at least I think Unity shouldnt think there is based on oncollision being called on frame of "impact")
        Vector2 zeroVelocity = new Vector2(0, 0);
        Player.GetComponent<Rigidbody2D>().velocity = zeroVelocity;
        Player.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        //Did this to add the delay
        if (scoreScript.getLives() > 0)
            Invoke("restartMovement", 2.0f);
        else
        {
            Vector3 offscreenPos = new Vector3(2000.0f, 2000.0f);
            Player.transform.localPosition = offscreenPos;
        }
    }

    //Restarts the balls movement
    public void restartMovement()
    {
        //random direction for x with vector y always going down. COPIED FROM BallMovement Script
        float randX = Random.Range(-1.0f, 1.0f);
        Vector2 startForce = new Vector2(randX, -1);
        //Player.GetComponent<Rigidbody2D>().velocity = startForce;
        Player.GetComponent<Rigidbody2D>().AddForce(startForce.normalized * 400f);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////// for ml agent
    public void respawn()
    {
        Debug.Log("Does this get called");
        Vector2 zeroVelocity = new Vector2(0, 0);
        Player.GetComponent<Rigidbody2D>().velocity = zeroVelocity;
        Player.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        // sets balls local position
        Player.transform.localPosition = new Vector3(-8f, -3.4f, 10f);
        Invoke("restartMovement", 2f);

    }
}
